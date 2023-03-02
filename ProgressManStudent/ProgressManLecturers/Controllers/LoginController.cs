using DataAccess.StudentAPI_.DAL;
using ProgressManLecturers.KB;
using ProgressManLecturers.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ProgressManLecturers.Models.LoginModel;

namespace ProgressManLecturers.Controllers
{
    public class LoginController : Controller
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
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
            list = new LoginGiangVien().Login(model.Username, model.Password);
            if (list.Count > 0)
            {
                Session["UserSuccess"] = model.Username.Trim();
                Session["UserId"] = list[0].MaGiangVien.Trim();
                return RedirectToAction("Index", "Home", new { id = list[0].MaGiangVien.Trim(), name = list[0].HoTen.Trim() });
            }
            else
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu.";
                return View();
            }

        }
    }
}