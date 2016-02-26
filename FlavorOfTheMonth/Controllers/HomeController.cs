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
        private readonly HomeModel m_RespModel = new HomeModel();
        private HomeRequestModel m_ReqModel = new HomeRequestModel();

        #region View Returners

        // Returns the Index view when called with GET /Home/Index
        public ActionResult Index()
        {
            ViewBag.Message = "Flavor of the month lets you see which World of Warcraft comp compositions are currently the most popular.";

            SetCompStrings();
            CalcCompPercentages();
            SortCompsByPercentage();

            return View(m_RespModel);
        }

        // Returns the About view when called with GET /Home/About
        public ActionResult About()
        {
            ViewBag.Message = "We use a clustering algorithm to analyze the changes in ratings from the players on the leaderboards."
                + " Based on these differences we can guess who is playing with who and display the data to you.";
            ViewBag.Message += "Feel free to contact us at contactpandamic@gmail.com";
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
                CalcCompPercentages();
                SortCompsByPercentage();

                return PartialView(m_RespModel);
            }
            catch (Exception ex)
            {
                LoggingUtil.LogMessage(DateTime.Now, $"Message: {ex.Message}\nInnerException: {ex.InnerException}");
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

        // Create string interpretations of teams (Assuming only 3v3, US for now)
        // FORM OF: {TeamMember1Class}{TeamMember1Spec}{TeamMember2Class}{TeamMember2Spec}{TeamMember3Class}{TeamMember3Spec}
        private void SetCompStrings()
        {
            var db = new DataClassesDataContext();
            var teamMembers = from d in db.SP_GetAllTeamsByClassCompositionThenOrderThemByMostPopular()
                              select new
                              {
                                  d.TeamID,
                                  d.Name,
                                  d.BlizzName
                              };

            int playerCounter = 0;
            string str = "";
            foreach (var tm in teamMembers)
            {
                CompPercentModel compModel = new CompPercentModel();
                str += tm.Name + tm.BlizzName;

                // set a teamIndex if the current string matches a string already in the comps list
                int teamIndex = -1;
                foreach (var comp in m_RespModel.TeamModel.Comps)
                {
                    if (str == comp.strComp) // if already in list 
                        teamIndex = m_RespModel.TeamModel.Comps.IndexOf(comp);
                }

                // If we have finished iterating through a team then add the team to the comp. list
                if (playerCounter % (int)GetBracketFromString() == 2)
                {
                    if (teamIndex == -1) // if this is a new team comp., then add it to both lists (classes & percentages)
                    {
                        compModel.strComp = str;
                        compModel.Total = 1;
                        m_RespModel.TeamModel.Comps.Add(compModel);
                    }
                    else
                        m_RespModel.TeamModel.Comps[teamIndex].Total++;
                    str = "";
                }
                playerCounter++;
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

        private void CreateRequestModel(object paramObj)
        {
            // Convert parameter to team string
            string param = ((string[])paramObj).Length > 0 ? ((string[])paramObj).ElementAt(0) : null;

            param = GetTrimmedParam(param); // Remove extra \n and \\n chars and any whitespace

            if (param != null)
                m_ReqModel = JsonConvert.DeserializeObject<HomeRequestModel>(param);
        }

        private void BuildResponseModel()
        {
            // Build response HomeModel for the views.
            m_RespModel.CurRegion = GetRegionFromString();
            m_RespModel.CurBracket = GetBracketFromString();

            var stringList = m_ReqModel.classes.OfType<string>();
            stringList = stringList.Select(s => s.Trim());
            m_RespModel.CurCharacterList = new List<string>(stringList);

            var stringList2 = m_ReqModel.specs.OfType<string>();
            stringList2 = stringList2.Select(s => s.Trim());
            m_RespModel.CurSelectedSpecList = new List<string>(stringList2);
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
        private void FilterBySpec(List<string> specFilterList)
        {
            specFilterList.RemoveAll(x => x == "Select a spec...");
            specFilterList = specFilterList.OrderBy(s => s).ToList();
            
            // convert the pretty spec strings in specFilterList to the BlizzNames
            specFilterList = ConvertPrettySpecToBlizzSpec(specFilterList);

            // essentially spec1 followed by any thing followed by spec2 etc.
            string pattern = specFilterList.Aggregate("", (current, specFilter) => current + (specFilter + ".*"));
            m_RespModel.TeamModel.Comps.RemoveAll(x => !Regex.IsMatch(x.strComp, pattern));
        }

        private List<string> ConvertPrettySpecToBlizzSpec(List<string> specFilterList)
        {
            List<string> result = new List<string>();
            string aBlizzSpec = "";
            for (var i = 0; i < specFilterList.Count; i++)
            {
                bool found = false;
                switch (m_RespModel.CurCharacterList[i])
                {
                    case "Death Knight":
                    {
                        found = m_RespModel.ClassModel.SpecDicDK.TryGetValue(specFilterList[i], out aBlizzSpec);
                        break;
                    }
                    case "Druid":
                    {
                        found = m_RespModel.ClassModel.SpecDicDruid.TryGetValue(specFilterList[i], out aBlizzSpec);
                        break;
                    }
                    case "Hunter":
                    {
                        found = m_RespModel.ClassModel.SpecDicHunter.TryGetValue(specFilterList[i], out aBlizzSpec);
                        break;
                    }
                    case "Mage":
                    {
                        found = m_RespModel.ClassModel.SpecDicMage.TryGetValue(specFilterList[i], out aBlizzSpec);
                        break;
                    }
                    case "Monk":
                    {
                        found = m_RespModel.ClassModel.SpecDicMonk.TryGetValue(specFilterList[i], out aBlizzSpec);
                        break;
                    }
                    case "Paladin":
                    {
                        found = m_RespModel.ClassModel.SpecDicPaladin.TryGetValue(specFilterList[i], out aBlizzSpec);
                        break;
                    }
                    case "Priest":
                    {
                        found = m_RespModel.ClassModel.SpecDicPriest.TryGetValue(specFilterList[i], out aBlizzSpec);
                        break;
                    }
                    case "Rogue":
                    {
                        found = m_RespModel.ClassModel.SpecDicRogue.TryGetValue(specFilterList[i], out aBlizzSpec);
                        break;
                    }
                    case "Shaman":
                    {
                        found = m_RespModel.ClassModel.SpecDicShaman.TryGetValue(specFilterList[i], out aBlizzSpec);
                        break;
                    }
                    case "Warlock":
                    {
                        found = m_RespModel.ClassModel.SpecDicWarlock.TryGetValue(specFilterList[i], out aBlizzSpec);
                        break;
                    }
                    case "Warrior":
                    {
                        found = m_RespModel.ClassModel.SpecDicWarrior.TryGetValue(specFilterList[i], out aBlizzSpec);
                        break;
                    }
                }
                if (found)
                    result.Add(aBlizzSpec);
                else
                    LoggingUtil.LogMessage(DateTime.Now, $"Could not find a BlizzSpec for key {specFilterList[i]} in a class dictionary.");
            }
            return result;
        }

        #endregion Helpers
    }
}
