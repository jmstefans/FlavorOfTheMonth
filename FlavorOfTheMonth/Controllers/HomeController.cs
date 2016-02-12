﻿using System.Web.Mvc;
using FlavorOfTheMonth.Models;

namespace FlavorOfTheMonth.Controllers
{
    public class HomeController : Controller
    {
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

        /*
        public ActionResult Contact()
        {
            ViewBag.Message = "Feel free to contact us at fotmclub7@gmail.com";

            return View();
        }
        */
    }
}
