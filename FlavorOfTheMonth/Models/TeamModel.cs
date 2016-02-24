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
        /// A list of comps with their percentage of the total comps.
        /// </summary>
        public List<CompPercentModel> Comps { get; set; }

        /// <summary>
        /// You know what the fuck this does.
        /// </summary>
        public TeamModel()
        {
            Comps = new List<CompPercentModel>();
        }
    }
}