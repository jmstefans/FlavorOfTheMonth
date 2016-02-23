using Fotm.DAL;
using System.Linq;
using System.Web.Mvc;
using FlavorOfTheMonth.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace FlavorOfTheMonth.Controllers
{
    /// <summary>
    /// Class that handles all of the user actions that can occur from the home page.
    /// </summary>
    public class HomeController : Controller
    {
        private HomeModel respModel = new HomeModel();
        private HomeRequestModel reqModel = new HomeRequestModel();

        // Returns the Index view when called with GET /Home/Index
        public ActionResult Index()
        {
            ViewBag.Message = "Flavor of the month lets you see which World of Warcraft team compositions are currently the most popular.";
            ViewBag.Test = 
            // Get default data
            respModel.TeamModel.TeamList = GetData();

            return View(respModel);
        }

        // Returns the About view when called with GET /Home/About
        public ActionResult About()
        {
            ViewBag.Message = "We use team clustering algorithm to analyze the changes in ratings from the players on the leaderboards."
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

        private List<string> GetData()
        {
            var db = new DataClassesDataContext();
            var teamMembers = from d in db.SP_GetAllTeamsByClassCompositionThenOrderThemByMostPopular()
                              select new
                              {
                                  d.TeamID,
                                  d.Name,
                                  d.SpecName
                              };

            // Create string interpretations of teams (Assuming only 3v3, US for now)
            // FORM OF: {TeamMember1Class}{TeamMember1Spec}{TeamMember2Class}{TeamMember2Spec}{TeamMember3Class}{TeamMember3Spec}
            int playerCounter = 0;
            List<string> teamsList = new List<string>();
            List<int> teamCompTotalsList = new List<int>();
            string str = "";
            foreach (var tm in teamMembers)
            {
                str += tm.Name + tm.SpecName;

                int teamIndex = -1;
                foreach (var team in teamsList)
                {
                    if (str == team) // team comp has already been added to the team so don't add again but go on to update percentages
                        teamIndex = teamsList.IndexOf(team);
                }

                // If we have finished iterating through a team then add the team to the team list
                // and update the percentage list.
                if (playerCounter % (int)GetBracketFromString() == 2)
                {
                    if (teamIndex == -1) // if this is a new team comp., then add it to both lists (classes & percentages)
                    {
                        teamsList.Add(str);
                        teamCompTotalsList.Add(1);
                    }
                    else
                        teamCompTotalsList[teamIndex]++;
                    str = "";
                }
                playerCounter++;
            }
            ConvertCounterListToPercentageList(teamCompTotalsList);
            return teamsList;
        }

        private void ConvertCounterListToPercentageList(List<int> teamCompCountList)
        {
            // compute sum
            int sum = 0;
            foreach(var i in teamCompCountList)
            {
                sum += i;
            }
            // Replace number of times each unique class comp. appeared with it's percentage of the total team comps.
            foreach(var i in teamCompCountList)
            {
                respModel.TeamModel.PercentageList.Add((float)i / sum);
            }
        }

        #endregion Helpers
    }
}
