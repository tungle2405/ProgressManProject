using DataAccess.StudentAPI;
using DataAccess.StudentAPI.BUS;
using DataAccess.StudentAPI.DAL;
using DataAccess.StudentAPI.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ProgressManStudentAPI.Controllers
{
    public class LoginSVController : ApiController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        // GET: LoginSV
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [System.Web.Http.Route("api/login")]
        [System.Web.Http.HttpPost]
        public CResponeMessage Login(LoginModel login)
        {
            //CResponeMessage cRespone = new CResponeMessage();
            DBConnection.GetSqlConnection(connectionString); //Mở

            var loginCheck = new LoginSinhVien().CheckLogin(login);
            
            DBConnection.GetSqlConnection(connectionString); //Đóng
            return loginCheck;
        }
        
    }
}