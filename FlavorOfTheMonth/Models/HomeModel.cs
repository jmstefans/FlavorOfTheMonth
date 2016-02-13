using System.Collections.Generic;
using System.Linq;

namespace FlavorOfTheMonth.Models
{
    /// <summary>
    /// Model to be used with the home page.
    /// </summary>
    public class HomeModel
    {
        /// <summary>
        /// List of wow classes
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
        /// Ajax test purposes only.
        /// </summary>
        public List<Character> Character;

        /// <summary>
        /// Constructor which initializes the state of the home screen.
        /// </summary>
        public HomeModel()
        {
            ClassModel = new ClassModel();
            CurBracket = Bracket._2v2;
            CurRegion = Region.US;
            DataClassesDataContext context = new DataClassesDataContext();
            //Character = (from character in context.Characters
            //             select character).ToList();
            Character = new List<Character>();
        }
    }
}