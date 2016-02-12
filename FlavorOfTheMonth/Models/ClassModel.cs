using System.Collections.Generic;

namespace FlavorOfTheMonth.Models
{
    /// <summary>
    /// A list of all the WoW classes to be used in the filter dropdowns.
    /// </summary>
    public class ClassModel
    {
        /// <summary>
        /// A list of strings to hold all of the names of the WoW classes.
        /// </summary>
        public List<string> ClassesList;

        public ClassModel()
        {
            ClassesList = new List<string>()
            {
                "Death Knight",
                "Druid",
                "Hunter",
                "Mage",
                "Monk",
                "Paladin",
                "Priest",
                "Rogue",
                "Shaman",
                "Warlock",
                "Warrior"
            };
        }
    }
}