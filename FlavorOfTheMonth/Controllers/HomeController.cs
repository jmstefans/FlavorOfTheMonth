using Fotm.DAL;
using System.Linq;
using System.Web.Mvc;
using FlavorOfTheMonth.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FlavorOfTheMonth.Controllers
{
    /// <summary>
    /// Class that handles all of the user actions that can occur from the home page.
    /// </summary>
    public class HomeController : Controller
    {
        private HomeModel respModel = new HomeModel();
        private HomeRequestModel reqModel = new HomeRequestModel();

        #region View Returners

        // Returns the Index view when called with GET /Home/Index
        public ActionResult Index()
        {
            ViewBag.Message = "Flavor of the month lets you see which World of Warcraft comp compositions are currently the most popular.";

            SetCompStrings();
            CalcCompPercentages();
            SortCompsByPercentage();

            return View(respModel);
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
            // Convert parameter to team string
            string param = ((string[])paramObj).Length > 0 ? ((string[])paramObj).ElementAt(0) : null;

            param = GetTrimmedParam(param); // Remove extra \n and \\n chars and any whitespace

            if (param != null)
                reqModel = JsonConvert.DeserializeObject<HomeRequestModel>(param);

            // Build response HomeModel for the views.
            respModel.CurRegion = GetRegionFromString();
            respModel.CurBracket = GetBracketFromString();

            var stringList = reqModel.classes.OfType<string>();
            stringList = stringList.Select(s => s.Trim());
            respModel.CurCharacterList = new List<string>(stringList);

            var stringList2 = reqModel.specs.OfType<string>();
            stringList2 = stringList2.Select(s => s.Trim());
            respModel.CurSelectedSpecList = new List<string>(stringList2);
            
            return PartialView(respModel);
        }

        #endregion View Returners

        #region Helpers

        public HomeModel.Region GetRegionFromString()
        {
            HomeModel.Region result;

            switch (reqModel.region)
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
                case "US":
                default:
                {
                    result = HomeModel.Region.US;
                    break;
                }
            }

            return result;
        }

        public HomeModel.Bracket GetBracketFromString()
        {
            HomeModel.Bracket result;

            switch (reqModel.bracket)
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
                case "3v3":
                default:
                    {
                        result = HomeModel.Bracket._3v3;
                        break;
                    }
            }

            return result;
        }

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
                                  d.SpecName
                              };

            int playerCounter = 0;
            string str = "";
            foreach (var tm in teamMembers)
            {
                CompPercentModel compModel = new CompPercentModel();
                str += tm.Name + tm.SpecName;

                // set a teamIndex if the current string matches a string already in the comps list
                int teamIndex = -1;
                foreach (var comp in respModel.TeamModel.Comps)
                {
                    if (str == comp.strComp) // if already in list 
                        teamIndex = respModel.TeamModel.Comps.IndexOf(comp);
                }

                // If we have finished iterating through a team then add the team to the comp. list
                if (playerCounter % (int)GetBracketFromString() == 2)
                {
                    if (teamIndex == -1) // if this is a new team comp., then add it to both lists (classes & percentages)
                    {
                        compModel.strComp = str;
                        compModel.Total = 1;
                        respModel.TeamModel.Comps.Add(compModel);
                    }
                    else
                        respModel.TeamModel.Comps[teamIndex].Total++;
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
            int sum = 0;
            foreach(var comp in respModel.TeamModel.Comps)
            {
                sum += comp.Total;
            }
            // Replace number of times each unique class comp. appeared with it's percentage of the total team comps.
            foreach(var comp in respModel.TeamModel.Comps)
            {
                comp.Percentage = (float)comp.Total / sum;
            }
        }

        /// <summary>
        /// Sort the team compositions by their popularity percentage descending.
        /// </summary>
        private void SortCompsByPercentage()
        {
            List<CompPercentModel> sortedList = respModel.TeamModel.Comps.OrderBy(c => c.Percentage).ToList();
            sortedList.Reverse();
            respModel.TeamModel.Comps = sortedList;
        }

        #endregion Helpers
    }
}
