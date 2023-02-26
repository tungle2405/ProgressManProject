using ProgressManStudent.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using static ProgressManStudent.Models.LoginModel;
using System.Web.UI;
using System.Xml.Linq;

namespace ProgressManStudent.Controllers
{
    public class LoginController : Controller
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        // GET: Login
        public ActionResult Index()
        {
            var test = Session["UserSuccess"];
            if (test != null)
            {
                // Nếu đã tồn tại, chuyển hướng đến trang chủ
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            List<UserInfo> list = new List<UserInfo>();
            //LoginModel loginModelValue = new LoginModel();
            list = model.Login(model.Username,model.Password);
            if(list.Count > 0)
            {
                Session["UserSuccess"] = model.Username.Trim();
                Session["UserId"] =  list[0].MaSinhVien.Trim();
                return RedirectToAction("Index", "Home", new {id = list[0].MaSinhVien.Trim(), name = list[0].HoTen.Trim() });
            }
            else
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu.";
                return View();
            }
            
        }
    }
}