using System.Collections.Generic;
using WowDotNetAPI;
using WowDotNetAPI.Models;

namespace FotmServerApp.WowAPI
{
    /// <summary>
    /// Class for retrieving data from the WoW REST API. 
    /// </summary>
    public class WowAPIManager
    {
        #region API KEY

        private const string API_KEY = "wxkatqct3862fp52eqcbwuqr3judxzdu";
        
        #endregion

        #region Leaderboards

        public static IEnumerable<PvpStats> GetPvpStats(Region region = Region.US,
                                                        Locale locale = Locale.en_US,
                                                        Bracket bracket = Bracket._3v3)
        {
            var explorer = new WowExplorer(region, locale, API_KEY);
            var leaders = explorer.GetLeaderBoards(bracket);
            return leaders.PvpStats;
        }

        #endregion

    }
}
