using System.Linq;
using System.Web.Mvc;
using FlavorOfTheMonth.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FlavorOfTheMonth.Controllers
{
    public class HomeController : Controller
    {
        private HomeModel respModel = new HomeModel();
        private HomeRequestModel reqModel = new HomeRequestModel();

        // Returns the Index view when called with GET /Home/Index
        public ActionResult Index()
        {
            ViewBag.Message = "Flavor of the month lets you see which World of Warcraft team compositions are currently the most popular.";
            return View(new HomeModel());
        }

        // Returns the About view when called with GET /Home/About
        public ActionResult About()
        {
            ViewBag.Message = "We use a clustering algorithm to analyze the changes in ratings from the players on the leaderboards."
                + " Based on these differences we can guess who is playing with who and display the data to you.";
            ViewBag.Message += "Feel free to contact us at contactpandamic@gmail.com";
            return View();
        }

        // Example/demo purposes only
        public PartialViewResult GetCharacters()
        {
            DataClassesDataContext context = new DataClassesDataContext();
            Character[] chars = context.GetTable<Character>().ToArray();

            return PartialView(chars);
        }

        // Method to handle the POST /Home/RefreshData
        public PartialViewResult RefreshData(object paramObj)
        {
            // Convert parameter to a string
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
                case "3v3":
                    {
                        result = HomeModel.Bracket._3v3;
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
                case "2v2":
                default:
                    {
                        result = HomeModel.Bracket._2v2;
                        break;
                    }
            }

            return result;
        }

        private string GetTrimmedParam(string s)
        {
            return s.Replace("\n", "").Replace("\\n", "").Trim();
        }

        #endregion Helpers
    }
}
