using System;
using System.Collections.Generic;
using System.Linq;

namespace FotmServerApp.Analysis.Algorithms
{
    /// <summary>
    /// Implementation of the Kmeans algorithm to cluster teams from the WoW leaderboard. 
    /// </summary>
    public class LeaderboardKmeans
    {
        public class Member
        {
            /// <summary>
            /// Member character name.
            /// </summary>
            public string Name { get; set; }
            
            /// <summary>
            /// Member realm name.
            /// </summary>
            public string RealmName { get; set; }
            
            /// <summary>
            /// Member rating change between two time points.
            /// </summary>
            public int RatingChangeValue { get; set; }

            /// <summary>
            /// Class specialization (Rogue's assasination/combat/subtlety)
            /// </summary>
            public string Spec { get; set; }
        }

        public class Team
        {
            /// <summary>
            /// The mean of the team's rating change values.
            /// </summary>
            public double Mean { get; set; }

            /// <summary>
            /// List of members belonging to this team.
            /// </summary>
            public List<Member> Members { get; set; }

            /// <summary>
            /// You know what the fuck this does.
            /// </summary>
            public Team()
            {
                Members = new List<Member>();
            }
        }

        /// <summary>
        /// Clusters the members into teams of provided team size.
        /// </summary>
        /// <param name="members">The members to cluster.</param>
        /// <param name="teamSize">The requested team size.</param>
        /// <param name="maximumIterations">The maximum number of iterations to perform before converging.</param>
        /// <returns></returns>
        public static List<Team> ClusterTeams(List<Member> members,
                                              int teamSize,
                                              int maximumIterations = 100)
        {
            var changed = true;
            var success = true;

            // round up the number of teams to avoid too few
            var numberOfTeams = members.Count/ teamSize;
            if (numberOfTeams*teamSize != members.Count)
                return null; //this line used to be    numberOfTeams += 1;    but that caused in ArrayOutOfBoundsEx in InitializeTeams()

            var clusteredTeams = InitializeTeams(members, numberOfTeams, teamSize);
            var currentIteration = 0;
            var maxIterations = members.Count * maximumIterations;

            while (changed && success && currentIteration < maxIterations)
            {
                success = UpdateTeamMeans(clusteredTeams);
                changed = UpdateClustering(clusteredTeams, members);

                currentIteration++;
            }

            return clusteredTeams;
        }

        /// <summary>
        /// Initializes the list of teams by sequentially adding all of the members to a team.
        /// </summary>
        private static List<Team> InitializeTeams(List<Member> members, int numberOfTeams, int teamSize)
        {
            var teams = new List<Team>();

            /* Initializing each cluster */
            for (var i = 0; i < numberOfTeams; i++)
            {
                var team = new Team();
                for (var j = 0; j < teamSize; j++)
                {
                    team.Members.Add(members[i*teamSize + j]);
                }
                teams.Add(team);
            }

            /* Initialize the team's means */
            //for (var i = 0; i < numberOfTeams; i++)
            //{
            //    teams[i].Means = new double[]; // TODO: add this so multiple props can be clustered on
            //}

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
                    var memberCount = team.Members.Count;
                    if (memberCount <= 0)
                        continue; // avoid divide by zero- todo: originally was returning false here, test if necessary
                    var sum = team.Members.Sum(m => m.RatingChangeValue);
                    team.Mean = (double)sum / memberCount;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// For each member provided, adds it to the closest team.
        /// </summary>
        private static bool UpdateClustering(List<Team> teams, List<Member> members)
        {
            var changed = false;

            foreach (var member in members)
            {
                var team = FindClosestTeam(teams, member);
                var invalid = team == null ||
                              team.Members.Any(m => m.Name.Equals(member.Name) && m.RealmName.Equals(member.RealmName));
                if (invalid)
                    continue;
                
                // Remove from previous team
                RemoveMemberFromTeam(teams, member);
                team.Members.Add(member); // Note - this won't handle teams with members > cluster size
                changed = true;
            }

            return changed;
        }

        /// <summary>
        /// Finds the team with a mean closest to the member's rating change value.
        /// </summary>
        private static Team FindClosestTeam(List<Team> teams, Member member)
        {
            Team closestTeam = null;
            var closestDistance = double.MaxValue;

            foreach (var team in teams)
            {
                /* Note - just using basic distance b/w two numbers here
                          this will need an update to account for multiple properties */
                var currentDistance = Math.Abs(team.Mean - member.RatingChangeValue);
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
        private static void RemoveMemberFromTeam(List<Team> teams, Member memberToRemove)
        {
            foreach (var team in teams)
            {
                for (var i = 0; i < team.Members.Count; i++)
                {
                    var member = team.Members[i];
                    if (member.Name.Equals(memberToRemove.Name) &&
                        member.RealmName.Equals(memberToRemove.RealmName))
                    {
                        team.Members.RemoveAt(i);
                    }
                }
            }
        }
    }
}
