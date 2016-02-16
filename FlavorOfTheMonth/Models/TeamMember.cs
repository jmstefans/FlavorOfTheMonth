namespace FlavorOfTheMonth.Models
{
    public class TeamMember
    {
        /// <summary>
        /// Unique team member Id.
        /// </summary>
        public long TeamMemberID { get; set; }

        /// <summary>
        /// ID of team this member belongs to.
        /// </summary>
        public long TeamID { get; set; }

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