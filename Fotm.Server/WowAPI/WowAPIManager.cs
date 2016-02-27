using System;
using System.Collections.Generic;
using System.Net;
using Fotm.DAL.Models.Base;
using Fotm.DAL.Util;
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

        private string API_KEY = ConfigUtil.API_Key;

        #endregion

        #region Leaderboards

        /// <summary>
        /// Gets the collection of pvp stats from API.
        /// </summary>
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

        /// <summary>
        /// Gets the character by name and realm w/ optional region and locale.
        /// </summary>
        public Character GetCharacter(string name, string realmName,
                                      Region region = Region.US, Locale locale = Locale.en_US)
        {
            try
            {
                return new WowExplorer(region, locale, API_KEY)
                                      .GetCharacter(realmName, name);
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
