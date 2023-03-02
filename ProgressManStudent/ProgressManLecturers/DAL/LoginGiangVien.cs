using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ProgressManLecturers.KB;
using CoreLib.DAL;

namespace DataAccess.StudentAPI_.DAL
{
    internal class LoginGiangVien
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
                password = (password != null) ? new HashCode().Encrypt(password) : null;
                param2.Value = password;
                command.Parameters.Add(param2);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //result = reader[0].ToString();
                    UserInfo user = new UserInfo();
                    user.HoTen = (string)reader["HoTen"];
                    user.TrinhDo = (string)reader["TrinhDo"];
                    user.MaDonVi = (string)reader["MaDonVi"];
                    user.ChuyenMon = (string)reader["ChuyenMon"];
                    user.MaPhanQuyen = (string)reader["MaPhanQuyen"];
                    user.GioiTinh = (string)reader["GioiTinh"];
                    user.MaGiangVien = (string)reader["MaGiangVien"];
                    users.Add(user);
                }
                connection.Close();
            }
            return users;
        }
    }
}
