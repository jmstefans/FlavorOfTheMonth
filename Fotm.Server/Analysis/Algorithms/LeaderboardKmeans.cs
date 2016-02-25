using System;
using System.Collections.Generic;
using System.Linq;
using Fotm.DAL;
using Fotm.DAL.Util;
using Fotm.Server.Util;

namespace Fotm.Server.Analysis.Algorithms
{
    /// <summary>
    /// Implementation of the Kmeans algorithm to cluster teams from the WoW leaderboard. 
    /// </summary>
    public class LeaderboardKmeans
    {
        /// <summary>
        /// Clusters the members into teams of provided team size.
        /// </summary>
        /// <param name="members">The members to cluster.</param>
        /// <param name="teamSize">The requested team size.</param>
        /// <param name="maximumIterations">The maximum number of iterations to perform before converging.</param>
        /// <returns>A list of clustered teams.</returns>
        public static List<Team> ClusterTeams(List<TeamMember> members,
                                              int teamSize,
                                              int maximumIterations = 100)
        {
            var numberOfTeams = members.Count / teamSize;
            if (numberOfTeams * teamSize != members.Count)
            {
                LoggingUtil.LogMessage(DateTime.Now,
                                       "The team size requested doesn't divide evenly with the number of team members provided",
                                       LoggingUtil.LogType.Warning);
                return null; // for now, don't cluster uneven number of teams
            }

            var clusteredTeams = InitializeTeams(members, numberOfTeams, teamSize);
            ClusterTeams(clusteredTeams, members, maximumIterations, teamSize);

            return clusteredTeams;
        }

        private static void ClusterTeams(List<Team> clusteredTeams,
                                         List<TeamMember> membersToCluster,
                                         int maximumIterations,
                                         int teamSize)
        {
            // init loop variables
            var currentIteration = 0;
            var maxIterations = membersToCluster.Count * maximumIterations;
            var changed = true;
            var success = true;

            while (changed && success && currentIteration < maxIterations)
            {
                success = UpdateTeamMeans(clusteredTeams);
                changed = UpdateClustering(clusteredTeams, membersToCluster);

                currentIteration++;
            }

            if (clusteredTeams.Any(team => team.TeamMembers.Count > teamSize))
            {
                // teams are uneven, resolve team sizes
                ResolveUnevenTeams(clusteredTeams, teamSize);

                lock (_lock)
                {
                    ++_currentResolveIteration;
                    if (_currentResolveIteration >= 5) // arbitrary max
                    {
                        _currentResolveIteration = 0;
                        return;
                    }
                }
                
                // cluster teams again after resolution
                var members = clusteredTeams.SelectMany(t => t.TeamMembers).ToList();
                ClusterTeams(clusteredTeams, members, maximumIterations, teamSize);
            }
        }

        private static object _lock = new object();
        private static int _currentResolveIteration = 0;

        /// <summary>
        /// Initializes the list of teams by sequentially adding all of the members to a team.
        /// </summary>
        private static List<Team> InitializeTeams(List<TeamMember> members, int numberOfTeams, int teamSize)
        {
            var teams = new List<Team>();

            /* Initializing each cluster */
            for (var i = 0; i < numberOfTeams; i++)
            {
                var team = new Team();
                for (var j = 0; j < teamSize; j++)
                {
                    team.TeamMembers.Add(members[i * teamSize + j]);
                }
                teams.Add(team);
            }

            return teams;
        }

        /// <summary>
        /// Recalculates the mean of each team.
        /// </summary>
        private static bool UpdateTeamMeans(List<Team> teams)
        {
            try
            {
                foreach (var team in teams)
                {
                    var memberCount = team.TeamMembers.Count;
                    if (memberCount <= 0)
                        continue; // avoid divide by zero- todo: originally was returning false here, test if necessary

                    var sumRatingChange = team.TeamMembers.Sum(m => m.RatingChangeValue);
                    team.MeanRatingChange = (double)sumRatingChange / memberCount;

                    var sumRating = team.TeamMembers.Sum(m => m.CurrentRating);
                    team.MeanRating = (double)sumRating / memberCount;
                }
                return true;
            }
            catch (Exception e)
            {
                LoggingUtil.LogMessage(DateTime.Now,
                                        $"Exception thrown attempting to update team means: {e.StackTrace}",
                                        LoggingUtil.LogType.Warning);
                return false;
            }
        }

        /// <summary>
        /// For each member provided, adds it to the closest team.
        /// </summary>
        private static bool UpdateClustering(List<Team> teams, List<TeamMember> members)
        {
            var changed = false;

            foreach (var member in members)
            {
                var team = FindClosestTeam(teams, member);
                var invalid = team == null ||
                              team.TeamMembers.Any(m => m.CharacterID == member.CharacterID);
                if (invalid)
                    continue;

                // Remove from previous team
                RemoveMemberFromTeam(teams, member);
                team.TeamMembers.Add(member); // Note - this won't handle teams with members > cluster size
                changed = true;
            }

            return changed;
        }

        /// <summary>
        /// Finds the team with a mean closest to the member's rating change value.
        /// </summary>
        private static Team FindClosestTeam(List<Team> teams, TeamMember teamMember)
        {
            Team closestTeam = null;
            var closestDistance = double.MaxValue;

            foreach (var team in teams)
            {
                var currentDistance = GetTeamMemberDistanceToTeam(team, teamMember);
                if (currentDistance < closestDistance)
                {
                    closestTeam = team;
                    closestDistance = currentDistance;
                }
            }

            return closestTeam;
        }

        /// <summary>
        /// Removes the member from all teams.
        /// </summary>
        private static void RemoveMemberFromTeam(List<Team> teams, TeamMember teamMemberToRemove)
        {
            foreach (var team in teams)
            {
                for (var i = 0; i < team.TeamMembers.Count; i++)
                {
                    var member = team.TeamMembers[i];
                    if (member.CharacterID == teamMemberToRemove.CharacterID)
                    {
                        team.TeamMembers.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// Resolves teams with sizes not equal to the team size. 
        /// </summary>
        private static void ResolveUnevenTeams(List<Team> teams, int teamSize)
        {
            var unevenTeams = teams.Where(t => t.TeamMembers.Count != teamSize).ToList();
            foreach (var team in unevenTeams)
            {
                if (team.TeamMembers.Count > teamSize)
                {
                    /* only need the furthest members, they will be added to teams 
                       with sizes < expected team size */
                    var furthestMembers = FindAndRemoveFurthestTeamMembers(team, teamSize);
                    AddToClosestTeam(unevenTeams, furthestMembers, teamSize);
                }
            }
        }

        /// <summary>
        /// Finds the team members in a team that are furthest away (compared to other members of the team) 
        /// from the team's rating mean and rating change mean
        /// </summary>
        private static List<TeamMember> FindAndRemoveFurthestTeamMembers(Team unevenTeam, int teamSize)
        {
            var furthestTeamMembers = new List<TeamMember>();
            var teamCount = unevenTeam.TeamMembers.Count;

            var furthestMember = unevenTeam.TeamMembers[0]; // init with first team member
            var furthestDistance = GetTeamMemberDistanceToTeam(unevenTeam, furthestMember);

            // while the uneven team is still uneven
            while (teamCount > teamSize)
            {
                // find the furthest member away from team means
                foreach (var member in unevenTeam.TeamMembers)
                {
                    var currentDistance = GetTeamMemberDistanceToTeam(unevenTeam, member);
                    if (currentDistance > furthestDistance)
                    {
                        furthestMember = member;
                        furthestDistance = currentDistance;
                    }
                }

                // reset furthest distance 
                furthestDistance = double.MinValue;

                // remove from the uneven team and add to furthest members
                unevenTeam.TeamMembers.Remove(furthestMember);
                furthestTeamMembers.Add(furthestMember);
                teamCount--;
            }

            return furthestTeamMembers;
        }

        /// <summary>
        /// Gets the distance between the team and team member. 
        /// This is using the basic distance formula (as if they were points on a graph), 
        /// and should eventually be replaced with the sum of square differences formula. 
        /// </summary>
        private static double GetTeamMemberDistanceToTeam(Team team, TeamMember teamMember)
        {
            var ratingDiff = team.MeanRating - teamMember.CurrentRating;
            var ratingChangeDiff = team.MeanRatingChange - teamMember.RatingChangeValue;
            return Math.Sqrt(Math.Pow(ratingDiff, 2) + Math.Pow(ratingChangeDiff, 2));
        }

        /// <summary>
        /// For each of the team members in the uneven list, finds the closest team 
        /// with a current size less than teamsize and adds it to the list. 
        /// </summary>
        private static void AddToClosestTeam(List<Team> teams, List<TeamMember> teamMembers, int teamSize)
        {
            foreach (var teamMember in teamMembers)
            {
                var closestDistance = double.MaxValue;
                Team closestTeam = null;

                foreach (var team in teams.Where(t => t.TeamMembers.Count < teamSize))
                {
                    var distance = GetTeamMemberDistanceToTeam(team, teamMember);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTeam = team;
                    }
                }

                closestTeam?.TeamMembers.Add(teamMember);
            }
        }
    }
}
