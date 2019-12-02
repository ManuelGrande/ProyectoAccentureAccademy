using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoAccenture.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MenuLibros()
        {
            return View();
        }


        public ActionResult MenuAutores()
        {
            return View();
        }

        public ActionResult MenuGeneros()
        {
            return View();
        }

        public ActionResult MenuEditoriales()
        {
            return View();
        }
    }
}