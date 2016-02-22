using System.Collections.Generic;

namespace FlavorOfTheMonth.Models
{
    public class TeamModel
    {
        /// <summary>
        /// Unique team Id.
        /// </summary>
        public long TeamID { get; set; }

        /// <summary>
        /// Pvp bracket this team is for.
        /// </summary>
        public HomeModel.Bracket PvpBracket { get; set; }

        /// <summary>
        /// The mean of the team's rating change values.
        /// </summary>
        public double Mean { get; set; }

        /// <summary>
        /// List of members belonging to this team.
        /// </summary>
        public List<TeamMember> Members { get; set; }

        /// <summary>
        /// String representation of teams.
        /// </summary>
        public List<string> TeamList { get; set; }

        /// <summary>
        /// A list of the percentages of the total for each teamp 
        /// composition. Index will correspond to the TeamList's index.
        /// </summary>
        public List<int> PercentageList { get; set; }

        /// <summary>
        /// You know what the fuck this does.
        /// </summary>
        public TeamModel()
        {
            Members = new List<TeamMember>();
        }
    }
}