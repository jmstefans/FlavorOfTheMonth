using FlavorOfTheMonth.Models;
using Fotm.DAL;
using Fotm.DAL.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace FlavorOfTheMonth.Controllers
{
    /// <summary>
    /// Class that handles all of the user actions that can occur from the home page.
    /// </summary>
    public class HomeController : Controller
    {
        private const int MAX_NUM_COMPS_TO_RETURN = 10;
        private readonly HomeModel m_RespModel = new HomeModel();
        private HomeRequestModel m_ReqModel = new HomeRequestModel();

        #region View Returners

        // Returns the Index view when called with GET /Home/Index
        public ActionResult Index()
        {
            ViewBag.Message = "Flavor of the month lets you see which World of Warcraft arena team compositions are currently the most popular among the leaderboard.";

            SetCompStrings();
            CalcCompPercentages();
            SortCompsByPercentage();
            GetTopNComps(MAX_NUM_COMPS_TO_RETURN);
            // redo the percentages so they reflect the current comps. after trimming down
            CalcCompPercentages();

            return View(m_RespModel);
        }

        // Returns the About view when called with GET /Home/About
        public ActionResult About()
        {
            ViewBag.Message = "We use a clustering algorithm to analyze the changes in ratings from the players on the leaderboards."
                + " Based on these differences we can guess who is playing with who and display the data to you."
                + " Feel free to contact us at contactpandamic@gmail.com.";
            return View();
        }

        // Method to handle the POST /Home/RefreshData
        public PartialViewResult RefreshData(object paramObj)
        {
            try
            {
                CreateRequestModel(paramObj);
                BuildResponseModel();

                SetCompStrings();
                if (m_RespModel.CurCharacterList.Count > 0)
                    FilterByClass(m_RespModel.CurCharacterList);
                if (m_RespModel.CurSelectedSpecList.Count > 0)
                    FilterBySpec(m_RespModel.CurSelectedSpecList);
                if (m_RespModel.CurFaction >= 0)
                    FilterByFaction(m_RespModel.CurFaction);
                CalcCompPercentages();
                SortCompsByPercentage();
                GetTopNComps(MAX_NUM_COMPS_TO_RETURN);
                // redo the percentages so they reflect the current comps. after trimming down
                CalcCompPercentages();

                return PartialView(m_RespModel);
            }
            catch (Exception ex)
            {
                LoggingUtil.LogMessage(DateTime.Now, $"Message: {ex.Message}\nInnerException: {ex.InnerException}", LoggingUtil.LogType.Error, true, false);
                return PartialView(m_RespModel);
            }
        }

        #endregion View Returners

        #region Helpers

        /// <summary>
        /// Returns the region enum from the request model object's string value.
        /// </summary>
        /// <returns></returns>
        public HomeModel.Region GetRegionFromString()
        {
            HomeModel.Region result;

            switch (m_ReqModel.region)
            {
                case "EU":
                {
                    result = HomeModel.Region.EU;
                    break;
                }
                case "KR":
                {
                    result = HomeModel.Region.KR;
                    break;
                }
                case "SEA":
                {
                    result = HomeModel.Region.SEA;
                    break;
                }
                case "TW":
                {
                    result = HomeModel.Region.TW;
                    break;
                }
                case "CN":
                {
                    result = HomeModel.Region.CN;
                    break;
                }
                default:
                {
                    result = HomeModel.Region.US;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the bracket enum from the request model object's string value.
        /// </summary>
        /// <returns></returns>
        public HomeModel.Bracket GetBracketFromString()
        {
            HomeModel.Bracket result;

            switch (m_ReqModel.bracket)
            {
                case "2v2":
                    {
                        result = HomeModel.Bracket._2v2;
                        break;
                    }
                case "5v5":
                    {
                        result = HomeModel.Bracket._5v5;
                        break;
                    }
                case "rbg":
                    {
                        result = HomeModel.Bracket._rbg;
                        break;
                    }
                default:
                    {
                        result = HomeModel.Bracket._3v3;
                        break;
                    }
            }

            return result;
        }

        /// <summary>
        /// Returns the faction enum from the request model object's string value.
        /// </summary>
        /// <returns></returns>
        public HomeModel.Faction GetFactionFromString()
        {
            HomeModel.Faction result;
            switch (m_ReqModel.faction)
            {
                case "0":
                    {
                        result = HomeModel.Faction.Alliance;
                        break;
                    }
                case "1":
                    {
                        result = HomeModel.Faction.Horde;
                        break;
                    }
                case "-1":
                default:
                    {
                        result = HomeModel.Faction.Any;
                        break;
                    }
            }
            return result;
        }

        /// <summary>
        /// Something is fudged with the AJAX request and we end up with extra return
        /// characters and whitespace. I think it has something to do with calling 
        /// JSON stringify on something that's already a string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string GetTrimmedParam(string s)
        {
            return s.Replace("\n", "").Replace("\\n", "").Trim();
        }

        // Create string interpretations of teams (Assuming only US for now)
        // FORM OF: {TeamMember1Class}{TeamMember1Spec}{TeamMember2Class}{TeamMember2Spec}{TeamMember3Class}{TeamMember3Spec}
        private void SetCompStrings()
        {
            var db = new DataClassesDataContext();
            var teamMembers = from d in db.SP_GetAllTeamsByClassCompositionThenOrderThemByMostPopular(m_RespModel.CurBracket.ToString(), (int)m_RespModel.CurRegion, (int)m_RespModel.CurFaction)
                              select new
                              {
                                  d.TeamID,
                                  d.Name,
                                  d.BlizzName,
                                  d.FactionID
                              };

            int playerCounter = 0;
            int prevTeamMembersTeamId = 0;
            string str = "";
            int teamIndex = -1;
            foreach (var tm in teamMembers)
            {
                // Avoiding the situation where the sql query returns a number of teammembers
                // that is not evenly divisible by the number of people on each team. The odd 
                // man/men/woman/women out can only be at the beginning of the result set I think.
                // TODO Probably best to fix stored proc. to only return full teams.
                if (prevTeamMembersTeamId == 0 || prevTeamMembersTeamId != tm.TeamID)
                {
                    str = "";
                    playerCounter = 0;
                }
                prevTeamMembersTeamId = (int)tm.TeamID;

                str += tm.Name + tm.BlizzName; // + BitToFaction(tm.FactionID);  Can maybe tack this onto the end for an icon

                // set a teamIndex if the current string matches a string already in the comps list
                teamIndex = -1;
                foreach (var comp in m_RespModel.TeamModel.Comps)
                {
                    if (str == comp.strComp) // if already in list 
                        teamIndex = m_RespModel.TeamModel.Comps.IndexOf(comp);
                }

                // If we have finished iterating through a team then add the team to the comp. list
                if (playerCounter % (int)GetBracketFromString() == (int)m_RespModel.CurBracket - 1)
                {
                    if (teamIndex == -1) // if this is a new team comp., then add it to both lists (classes & percentages)
                    {
                        m_RespModel.TeamModel.Comps.Add(new CompPercentModel() { strComp = str, Total = 1 });
                    }
                    else
                        m_RespModel.TeamModel.Comps[teamIndex].Total++;
                    str = "";
                }
                playerCounter++;
            }
        }

        /// <summary>
        /// Converts a bool? to a Faction.
        /// </summary>
        /// <param name="factionID"></param>
        /// <returns></returns>
        private HomeModel.Faction BitToFaction(bool? factionID)
        {
            switch (factionID)
            {
                case false:
                    return HomeModel.Faction.Alliance;
                case true:
                    return HomeModel.Faction.Horde;
                default:
                    return HomeModel.Faction.Any;
            }
        }

        /// <summary>
        /// Fill in the Percentage property of each team composition.
        /// </summary>
        private void CalcCompPercentages()
        {
            // compute sum
            int sum = m_RespModel.TeamModel.Comps.Sum(comp => comp.Total);

            // Replace number of times each unique class comp. appeared with it's percentage of the total team comps.
            foreach(var comp in m_RespModel.TeamModel.Comps)
            {
                comp.Percentage = (float)comp.Total / sum;
            }
        }

        /// <summary>
        /// Sort the team compositions by their popularity percentage descending.
        /// </summary>
        private void SortCompsByPercentage()
        {
            List<CompPercentModel> sortedList = m_RespModel.TeamModel.Comps.OrderBy(c => c.Percentage).ToList();
            sortedList.Reverse();
            m_RespModel.TeamModel.Comps = sortedList;
        }

        /// <summary>
        /// Create a C# object from the passed in json data.
        /// </summary>
        /// <param name="paramObj"></param>
        private void CreateRequestModel(object paramObj)
        {
            // Convert parameter to team string
            string param = ((string[])paramObj).Length > 0 ? ((string[])paramObj).ElementAt(0) : null;

            param = GetTrimmedParam(param); // Remove extra \n and \\n chars and any whitespace

            if (param != null)
                m_ReqModel = JsonConvert.DeserializeObject<HomeRequestModel>(param);
        }

        // Build the response's HomeModel for the views.
        private void BuildResponseModel()
        {
            m_RespModel.CurRegion = GetRegionFromString();
            m_RespModel.CurBracket = GetBracketFromString();
            m_RespModel.CurFaction = GetFactionFromString();

            var characterList = m_ReqModel.classes.OfType<string>();
            characterList = characterList.Select(s => s.Trim());
            m_RespModel.CurCharacterList = new List<string>(characterList);

            var specList = m_ReqModel.specs.OfType<string>();
            specList = specList.Select(s => s.Trim());
            for (var i = 0; i < specList.Count(); i++)
            {
                m_RespModel.CurSelectedSpecList.Add(i, specList.ElementAt(i));
            }
        }

        /// <summary>
        /// Remove all of the comps that don't contain the current filtered classes.
        /// Also the percentages will be of the subset of data and not the entire population.
        /// TODO Add a switch to flip between entire population and subset
        /// </summary>
        private void FilterByClass(List<string> classFilterList)
        {
            classFilterList.RemoveAll(x => x == "Select a class...");
            classFilterList = classFilterList.OrderBy(c => c).ToList();
            // essentially class1 followed by any thing followed by class2 etc.
            string pattern = classFilterList.Aggregate("", (current, classFilter) => current + (classFilter + ".*"));
            m_RespModel.TeamModel.Comps.RemoveAll(x => !Regex.IsMatch(x.strComp, pattern));
        }

        /// <summary>
        /// Remove all of the comps that don't contain the current filtered specs.
        /// Also the percentages will be of the subset of data and not the entire population.
        /// TODO Add a switch to flip between entire population and subset
        /// </summary>
        private void FilterBySpec(Dictionary<int, string> specFilterList)
        {
            var newSpecFilterList = specFilterList.Where(x => x.Value != "Select a spec...");
            newSpecFilterList = newSpecFilterList.OrderBy(s => s.Value);
            
            // convert the pretty spec strings in newSpecFilterList to the BlizzNames
            specFilterList = ConvertPrettySpecToBlizzSpec(newSpecFilterList.ToDictionary(x => x.Key, x => x.Value));

            // essentially spec1 followed by any thing followed by spec2 etc.
            string pattern = specFilterList.Aggregate("", (current, specFilter) => current + (specFilter.Value + ".*"));
            m_RespModel.TeamModel.Comps.RemoveAll(x => !Regex.IsMatch(x.strComp, pattern));
        }

        // Based on the class at the same index as the spec., in their respective lists,
        // we convert the pretty string to the unique spec string (Blood -> DK_BLOOD)
        private Dictionary<int, string> ConvertPrettySpecToBlizzSpec(Dictionary<int, string> specFilterList)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            string aBlizzSpec = "";
            for (var i = 0; i < specFilterList.Count; i++) // For all the specs in the dictionary paramter (doesn't include "Select a spec..." anymore)
            {
                bool found = false;
                // Get the class at that current spec's index. E.g. If the first filter had a class selected but no spec selected and the second filter
                // specified class and spec, we will only have one spec in the specFilterList. So we need to look at the second class by using the index
                // in the dictionary.
                switch (m_RespModel.CurCharacterList[specFilterList.ElementAt(i).Key]) // This line assumes same index for spec and class filters but spec could've shifted down the line.
                {
                    case "Death Knight":
                    {
                        found = m_RespModel.ClassModel.SpecDicDK.TryGetValue(specFilterList.ElementAt(i).Value, out aBlizzSpec);
                        break;
                    }
                    case "Druid":
                    {
                        found = m_RespModel.ClassModel.SpecDicDruid.TryGetValue(specFilterList.ElementAt(i).Value, out aBlizzSpec);
                        break;
                    }
                    case "Hunter":
                    {
                        found = m_RespModel.ClassModel.SpecDicHunter.TryGetValue(specFilterList.ElementAt(i).Value, out aBlizzSpec);
                        break;
                    }
                    case "Mage":
                    {
                        found = m_RespModel.ClassModel.SpecDicMage.TryGetValue(specFilterList.ElementAt(i).Value, out aBlizzSpec);
                        break;
                    }
                    case "Monk":
                    {
                        found = m_RespModel.ClassModel.SpecDicMonk.TryGetValue(specFilterList.ElementAt(i).Value, out aBlizzSpec);
                        break;
                    }
                    case "Paladin":
                    {
                        found = m_RespModel.ClassModel.SpecDicPaladin.TryGetValue(specFilterList.ElementAt(i).Value, out aBlizzSpec);
                        break;
                    }
                    case "Priest":
                    {
                        found = m_RespModel.ClassModel.SpecDicPriest.TryGetValue(specFilterList.ElementAt(i).Value, out aBlizzSpec);
                        break;
                    }
                    case "Rogue":
                    {
                        found = m_RespModel.ClassModel.SpecDicRogue.TryGetValue(specFilterList.ElementAt(i).Value, out aBlizzSpec);
                        break;
                    }
                    case "Shaman":
                    {
                        found = m_RespModel.ClassModel.SpecDicShaman.TryGetValue(specFilterList.ElementAt(i).Value, out aBlizzSpec);
                        break;
                    }
                    case "Warlock":
                    {
                        found = m_RespModel.ClassModel.SpecDicWarlock.TryGetValue(specFilterList.ElementAt(i).Value, out aBlizzSpec);
                        break;
                    }
                    case "Warrior":
                    {
                        found = m_RespModel.ClassModel.SpecDicWarrior.TryGetValue(specFilterList.ElementAt(i).Value, out aBlizzSpec);
                        break;
                    }
                }
                if (found)
                    // Add the new, blizz spec name and the index of it's place amongst all the filters to the return dictionary.
                    result.Add(specFilterList.ElementAt(i).Key, aBlizzSpec);
                else
                    LoggingUtil.LogMessage(DateTime.Now, $"Could not find a BlizzSpec for key {specFilterList.ElementAt(i).Value} in a class dictionary.", LoggingUtil.LogType.Error, true, false);
            }
            return result;
        }

        /// <summary>
        /// Filter the current list of comps. in the response (HomeModel) by the user's faction choice.
        /// </summary>
        /// <param name="curFaction"></param>
        private void FilterByFaction(HomeModel.Faction curFaction)
        {
            foreach (var comp in m_RespModel.TeamModel.Comps)
            {
                m_RespModel.TeamModel.Comps = m_RespModel.TeamModel.Comps.Where(c => !c.strComp.Contains(Convert.ToString((int)curFaction))).ToList();
            }
        }

        /// <summary>
        /// Trims the current comp. list down to n total compositions.
        /// </summary>
        /// <param name="n"></param>
        private void GetTopNComps(int n)
        {
            m_RespModel.TeamModel.Comps = m_RespModel.TeamModel.Comps.GetRange(0, Math.Min(n, m_RespModel.TeamModel.Comps.Count));
        }

        #endregion Helpers
    }
}
