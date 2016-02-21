using System;
using System.Collections.Generic;
using System.Net;
using Fotm.DAL.Models.Base;
using Fotm.Server.Util;
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

        private const string API_KEY = "";

        #endregion

        #region Leaderboards

        public IEnumerable<PvpStats> GetPvpStats(Region region = Region.US,
            Locale locale = Locale.en_US,
            Bracket bracket = Bracket._3v3)
        {
            try
            {
                var explorer = new WowExplorer(region, locale, API_KEY);
                var leaders = explorer.GetLeaderBoards(bracket);
                return leaders.PvpStats;
            }
            catch (WebException ex)
            {
                LoggingUtil.LogMessage(DateTime.Now, $"WebException caught at API manager request: {ex}");
                throw;
            }
        }

        public Character GetCharacter(string name, string realmName,
                                      Region region = Region.US, Locale locale = Locale.en_US)
        {
            try
            {
                var explorer = new WowExplorer(region, locale, API_KEY);
                return explorer.GetCharacter(realmName, name);
            }
            catch (WebException ex)
            {
                LoggingUtil.LogMessage(DateTime.Now, $"WebException caught at API manager request: {ex}");
                throw;
            }
        }

        #endregion
    }
}
