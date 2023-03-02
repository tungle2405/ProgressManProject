using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using static DataAccess.StudentAPI_.DAL.LoginGiangVien;
using System.Configuration;
using ProgressManLecturers.KB;

namespace ProgressManLecturers.DAL
{

    public class RegisterGiangVien
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        public List<UserInfo> Login(string username, string password)
        {
            List<UserInfo> users = new List<UserInfo>();
            using (connection)
            {
                SqlCommand command = new SqlCommand("GV_SP_CheckLogin", connection);
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
                    //user.LopNienChe = (string)reader["LopNienChe"];
                    user.GioiTinh = (string)reader["GioiTinh"];
                    //user.MaSinhVien = (string)reader["MaSinhVien"];
                    users.Add(user);
                }
                connection.Close();
            }
            return users;
        }
    }
}