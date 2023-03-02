using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using static ProgressManLecturers.Models.LoginModel;

namespace ProgressManLecturers.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Mã không được để trống.")]
        public string studentId { get; set; }
        [Required(ErrorMessage = "class không được để trống.")]
        public string studentClass { get; set; }
        [Required(ErrorMessage = "Tên không được để trống.")]
        public string fullname { get; set; }
        [Required(ErrorMessage = "gender không được để trống.")]
        public string gender { get; set; }
    }
}