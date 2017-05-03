﻿using System.Web.Mvc;

namespace MusicUploader.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        [Route]
        [Route("Index")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("About")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Route("Contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}