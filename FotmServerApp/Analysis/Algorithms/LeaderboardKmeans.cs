using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FotmServerApp.Models;
using Quartz.Impl.AdoJobStore;

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
            /// TODO
            /// </summary>
            public string Name { get; set; }
            
            /// <summary>
            /// TODO
            /// </summary>
            public string RealmName { get; set; }
            
            /// <summary>
            /// TODO
            /// </summary>
            public int RatingChangeValue { get; set; }
        }

        public class Team
        {
            /// <summary>
            /// TODO
            /// </summary>
            public double Mean { get; set; }

            /// <summary>
            /// TODO
            /// </summary>
            public List<Member> Members { get; set; }

            /// <summary>
            /// TODO
            /// </summary>
            public Team()
            {
                Members = new List<Member>();
            }
        }

        public static List<Team> ClusterTeams(List<Member> members,
                                              int teamSize,
                                              int maximumIterations = 100)
        {
            var changed = true;
            var success = true;

            // round up the number of teams to avoid too few
            var numberOfTeams = members.Count/ teamSize;
            if (numberOfTeams*teamSize != members.Count)
                numberOfTeams += 1;
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

        private static List<Team> InitializeTeams(List<Member> members, int numberOfTeams, int teamSize, int seed = 0)
        {
            var random = new Random(seed);
            var teams = new List<Team>();

            /* Initializing each cluster */
            for (var i = 0; i < numberOfTeams; i++)
            {
                var team = new Team();
                team.Members.Add(members[i]);
                teams.Add(team);
            }

            /* Randomly assigning rating changes */
            foreach (var team in teams)
            {
                var randomIndex = random.Next(0, numberOfTeams);
                var member = members[randomIndex];

                if (team.Members.Any(m => m.Name.Equals(member.Name) && m.RealmName.Equals(member.RealmName)))
                    continue; // already added

                
                team.Members.Add(member);
            }


            /* Initialize the team's means */
            //for (var i = 0; i < numberOfTeams; i++)
            //{
            //    teams[i].Means = new double[]; // TODO: add this so multiple props can be clustered on
            //}

            return teams;
        }

        private static void RemoveFromPreviousTeam(List<Team> teams, Member memberToRemove)
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

        private static bool UpdateTeamMeans(List<Team> teams)
        {
            foreach (var team in teams)
            {
                var memberCount = team.Members.Count;
                if (memberCount == 0)
                    continue; // avoid divide by zero

                var sum = team.Members.Sum(m => m.RatingChangeValue);
                team.Mean = (double)sum / memberCount;
            }

            return true;
        }

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
                RemoveFromPreviousTeam(teams, member);
                team.Members.Add(member); // Note - this won't handle teams with members > cluster size
                changed = true;
            }

            return changed;
        }

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
    }
}
