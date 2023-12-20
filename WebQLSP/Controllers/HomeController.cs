
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebQLSP.Models;

namespace WebQLSP.Controllers
{
    [Authentication]
    public class HomeController : Controller
    {
        private QLSPEntities db = new QLSPEntities();
        DateTime currentDate = DateTime.Now;

        public ActionResult Index()
        {

            return View();
        }

     
    }
}
