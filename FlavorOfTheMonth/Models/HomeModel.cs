using Fotm.DAL;
using System.Collections.Generic;

namespace FlavorOfTheMonth.Models
{
    /// <summary>
    /// Model to be used with the home page.
    /// </summary>
    public class HomeModel
    {
        /// <summary>
        /// Contains string lists for all of the classes and their specs.
        /// </summary>
        public ClassModel ClassModel;

        /// <summary>
        /// Currently selected bracket
        /// </summary>
        public Bracket CurBracket;

        /// <summary>
        /// Currently selected region
        /// </summary>
        public Region CurRegion;

        /// <summary>
        /// List of all pvp brackets.
        /// </summary>
        public enum Bracket
        {
            _2v2 = 2,
            _3v3,
            _5v5 = 5,
            _rbg = 15
        }

        /// <summary>
        /// List of all of the regions in WoW.
        /// </summary>
        public enum Region
        {
            US,
            EU,
            KR,
            TW,
            CN,
            SEA
        }

        /// <summary>
        /// Enum for all WoW factions for use with filtering.
        /// </summary>
        public enum Faction
        {
            Any = -1,
            Alliance = 0,
            Horde = 1
        }

        /// <summary>
        /// A list to keep track of the currently selected classes in the filter dropdowns.
        /// </summary>
        public List<string> CurCharacterList;

        /// <summary>
        /// A list to keep track of the currently selected specs in the filter dropdowns
        /// and their respective index in all of the filters.
        /// </summary>
        public Dictionary<int, string> CurSelectedSpecList;

        /// <summary>
        /// An enum to represent the current faction filter (-1 if no filter).
        /// </summary>
        public Faction CurFaction;

        /// <summary>
        /// Holds teams, comps, team members, the team's average rating and bracket.
        /// </summary>
        public TeamModel TeamModel;

        /// <summary>
        /// Constructor which initializes the state of the home screen.
        /// </summary>
        public HomeModel()
        {
            ClassModel = new ClassModel();
            CurBracket = Bracket._3v3;
            CurRegion = Region.US;
            CurCharacterList = new List<string>();
            CurSelectedSpecList = new Dictionary<int, string>();
            CurFaction = Faction.Any;
            TeamModel = new TeamModel();
        }
    }
}