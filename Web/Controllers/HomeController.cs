using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace Emotweecon.Web.Controllers
{
    public class HomeController : Controller
    {
        private CampusCtx _datacontext;

        public HomeController()
        {
            _datacontext = new CampusCtx();
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            
            return View();
        }
    }
}
