using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotmServerApp.Models
{
    public class TeamMember
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
}
