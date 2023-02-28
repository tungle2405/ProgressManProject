using DataAccess.StudentAPI;
using Newtonsoft.Json;
using ProgressManStudent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ProgressManStudent.Controllers
{
    public class LoginController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:44336/api");
        HttpClient client;
        public LoginController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        // GET: Login
        public ActionResult Index()
        {
            try
            {
                if (Session["user"] != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel user)
        {
            try
            {
                string data = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = client.PostAsync(client.BaseAddress + "/login", content).Result;
                string contents = response.Content.ReadAsStringAsync().Result.ToString();
                CResponeMessage crMess = new CResponeMessage();
                crMess = JsonConvert.DeserializeObject<CResponeMessage>(contents);
                if (crMess.Code == 0)
                {
                    Session["user"] = crMess.Data;
                    return Json(contents);
                }
                else
                {
                    return Json(contents);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }

        public ActionResult Logout() {

            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}