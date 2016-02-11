using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotmServerApp.Models
{
    public class Team
    {
        /// <summary>
        /// The mean of the team's rating change values.
        /// </summary>
        public double Mean { get; set; }

        /// <summary>
        /// List of members belonging to this team.
        /// </summary>
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
