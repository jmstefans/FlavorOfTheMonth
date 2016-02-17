using System.Collections.Generic;
using Fotm.Server.Models.Base;
using WowDotNetAPI.Models;

namespace Fotm.Server.Models
{
    public class Team : DbEntityBase
    {
        /// <summary>
        /// Unique team Id.
        /// </summary>
        public long TeamID { get; set; }

        /// <summary>
        /// Pvp bracket this team is for.
        /// </summary>
        public Bracket PvpBracket { get; set; }

        /// <summary>
        /// The mean of the team's rating change values.
        /// </summary>
        public double MeanRatingChange { get; set; }

        /// <summary>
        /// The mean of the team's rating.
        /// </summary>
        public double MeanRating { get; set; }

        /// <summary>
        /// List of members belonging to this team.
        /// </summary>
        [DbInsertProperty(false)]
        public List<TeamMember> Members { get; set; }

        /// <summary>
        /// You know what the fuck this does.
        /// </summary>
        public Team()
        {
            Members = new List<TeamMember>();
        }
    }
}
