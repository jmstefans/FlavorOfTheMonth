using System.Linq;
using System.Web.Mvc;
using FlavorOfTheMonth.Models;

namespace FlavorOfTheMonth.Controllers
{
    public class HomeController : Controller
    {
        private HomeModel m_homeModel = new HomeModel();

        public ActionResult Index()
        {
            ViewBag.Message = "Flavor of the month lets you see which World of Warcraft team compositions are currently the most popular.";
            return View(new HomeModel());
        }

        public ActionResult About()
        {
            ViewBag.Message = "We use a clustering algorithm to analyze the changes in ratings from the players on the leaderboards."
                + " Based on these differences we can guess who is playing with who and display the data to you.";
            ViewBag.Message += "Feel free to contact us at contactpandamic@gmail.com";
            return View();
        }

        public PartialViewResult GetCharacters()
        {
            DataClassesDataContext context = new DataClassesDataContext();
            Character[] chars = context.GetTable<Character>().ToArray();

            return PartialView(chars);
        }

        public PartialViewResult RefreshFilters(object paramObj)
        {
            int bracket;
            string param = ((string[]) paramObj).Length > 0 ? ((string[]) paramObj).ElementAt(0) : null;

            if (int.TryParse(param, out bracket))
            {
                switch (bracket)
                {
                    case 2:
                        m_homeModel.CurBracket = HomeModel.Bracket._2v2;
                        break;
                    case 3:
                        m_homeModel.CurBracket = HomeModel.Bracket._3v3;
                        break;
                    case 5:
                        m_homeModel.CurBracket = HomeModel.Bracket._5v5;
                        break;
                    case 15:
                        m_homeModel.CurBracket = HomeModel.Bracket._rbg;
                        break;
                    default:
                        m_homeModel.CurBracket = HomeModel.Bracket._2v2;
                        break;
                }
            }

            if (param == "US")
                m_homeModel.CurRegion = HomeModel.Region.US;
            if (param == "EU")
                m_homeModel.CurRegion = HomeModel.Region.EU;
            if (param == "KR")
                m_homeModel.CurRegion = HomeModel.Region.KR;
            if (param == "TW")
                m_homeModel.CurRegion = HomeModel.Region.TW;
            if (param == "CN")
                m_homeModel.CurRegion = HomeModel.Region.CN;
            if (param == "SEA")
                m_homeModel.CurRegion = HomeModel.Region.SEA;

            if (param == "Death Knight")
                m_homeModel.CurRegion = HomeModel.Region.US;

            return PartialView(m_homeModel);
        }
    }
}
