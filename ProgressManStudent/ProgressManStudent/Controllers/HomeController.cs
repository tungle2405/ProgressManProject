using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ProgressManStudent.Controllers
{
    public class HomeController : Controller
    {
        private void CheckSession()
        {
            var test = Session["UserSuccess"];
            if (test == null)
            {
                Response.Redirect("~/Login");
            }

        }
        public ActionResult Index(string id = "", string name = "")
        {
            CheckSession();
            ViewBag.Id = id;
            ViewBag.Name = name;
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
        public ActionResult Logout()
        {
            Session.Clear(); 
            return RedirectToAction("Index", "Login"); 
        }
    }
}