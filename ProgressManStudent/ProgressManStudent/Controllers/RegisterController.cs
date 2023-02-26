using ProgressManStudent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ProgressManStudent.Models.LoginModel;

namespace ProgressManStudent.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(RegisterModel model)
        {
            List<UserInfo> list = new List<UserInfo>();
            //LoginModel loginModelValue = new LoginModel();
            //list = model.Login(model.Username, model.Password);
            if (ModelState.IsValid)
            {
                return View();
            }
            else
            {
                ViewBag.Error = "Sai";
                return View(model);
            }

        }
    }
}