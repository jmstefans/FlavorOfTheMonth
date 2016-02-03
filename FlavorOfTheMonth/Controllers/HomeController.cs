using System.Web.Mvc;
using FlavorOfTheMonth.Kmeans;

namespace FlavorOfTheMonth.Controllers
{
    public class HomeController : Controller
    {
        private const int C_CALL_INTERVAL_SECONDS = 120;

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            //Kmean k = new Kmean();
            //k.Main();

            //for (int i = 0; i < 120; i++)
            //{
            //    Requester r = new Requester();
            //    ResourceSet rs = r.MakeRequest("https://us.api.battle.net/wow/leaderboard/3v3?locale=en_US&apikey=8e4txvdtgmpp5gbavwm9pysb9nuak8mf");
            //    try
            //    {
            //        foreach (var item in rs.rows)
            //        {
            //            Leaderboard lb = new Leaderboard();
            //            lb.ClassID = Convert.ToInt32(item.classId);
            //            lb.FactionID = Convert.ToInt32(item.factionId);
            //            lb.GenderID = Convert.ToInt32(item.genderId);
            //            lb.Name = item.name;
            //            lb.RaceID = Convert.ToInt32(item.raceId);
            //            lb.Ranking = item.ranking;
            //            lb.Rating = item.rating;
            //            lb.RealmID = item.realmId;
            //            lb.RealmName = item.realmName;
            //            lb.RealmSlug = item.realmSlug;
            //            lb.SeasonLosses = Convert.ToInt32(item.seasonLosses);
            //            lb.SeasonWins = Convert.ToInt32(item.seasonWins);
            //            lb.SpecID = Convert.ToInt32(item.seasonWins);
            //            lb.WeeklyLosses = Convert.ToInt32(item.weeklyLosses);
            //            lb.WeeklyWins = Convert.ToInt32(item.weeklyWins);
            //            lb.ModifiedDate = DateTime.Now;
            //            lb.ModifiedStatus = 'I';
            //            lb.ModifiedUserID = 0;
            //            var dataContext = new DataClassesDataContext();
            //            dataContext.Leaderboards.InsertOnSubmit(lb);
            //            dataContext.SubmitChanges();
            //        }
            //        Thread.Sleep(1000 * C_CALL_INTERVAL_SECONDS);
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e.ToString());
            //    }
            //}
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
