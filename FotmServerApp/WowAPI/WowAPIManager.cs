using System.Collections.Generic;
using Fotm.Server.Models.Base;
using WowDotNetAPI;
using WowDotNetAPI.Models;

namespace Fotm.Server.WowAPI
{
    /// <summary>
    /// Class for retrieving data from the WoW REST API. 
    /// </summary>
    public class WowAPIManager : ManagerBase<WowAPIManager>
    {
        #region API KEY

        private const string API_KEY = "wxkatqct3862fp52eqcbwuqr3judxzdu";
        
        #endregion

        #region Leaderboards

        public IEnumerable<PvpStats> GetPvpStats(Region region = Region.US,
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
