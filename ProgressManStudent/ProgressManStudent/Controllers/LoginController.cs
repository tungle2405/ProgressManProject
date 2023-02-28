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
            LoginModel user = new LoginModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/login").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                JsonConvert.DeserializeObject<LoginModel>(data);
                return Json(response.Content);
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
                    return Json(contents);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }

            return View("Index");
        }

        public ActionResult Logout() {

            return RedirectToAction("Index", "Login");
        }
    }
}