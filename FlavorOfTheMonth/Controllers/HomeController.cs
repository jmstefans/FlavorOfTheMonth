using System.Web.Mvc;

namespace FlavorOfTheMonth.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Flavor of the month lets you see which World of Warcraft team compositions are currently the most popular.";
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "We use a clustering algorithm to analyze the changes in ratings from the players on the leaderboards."
                + " Based on these differences we can guess who is playing with who and display the data to you.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Feel free to contact us at fotmclub7@gmail.com";

            return View();
        }
    }
}
