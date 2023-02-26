using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using static ProgressManStudent.Models.LoginModel;
using System.Configuration;

namespace ProgressManStudent.Models
{
    public class RegisterModel
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
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

        public List<UserInfo> Login(string username, string password)
        {
            List<UserInfo> users = new List<UserInfo>();
            using (connection)
            {
                SqlCommand command = new SqlCommand("spLogin", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter param1 = new SqlParameter("@tenTaiKhoan", SqlDbType.VarChar);
                param1.Value = username;
                command.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@matKhau", SqlDbType.VarChar);
                param2.Value = password;
                command.Parameters.Add(param2);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //result = reader[0].ToString();
                    UserInfo user = new UserInfo();
                    user.HoTen = (string)reader["HoTen"];
                    user.LopNienChe = (string)reader["LopNienChe"];
                    user.GioiTinh = (string)reader["GioiTinh"];
                    user.MaSinhVien = (string)reader["MaSinhVien"];
                    users.Add(user);
                }
                connection.Close();
            }
            return users;
        }
    }
}