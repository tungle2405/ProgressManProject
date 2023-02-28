using DataAccess.StudentAPI.BUS;
using DataAccess.StudentAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DataAccess.StudentAPI;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess.StudentAPI.DAL
{
    public class LoginSinhVien : ILoginSinhVien
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        /// <summary>
        /// Hàm này sử dụng để kiểm tra login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public CResponeMessage CheckLogin(LoginModel login)
        {
            try{
                CResponeMessage resMess = new CResponeMessage();

                var sqlcon = DBConnection.GetSqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SV_SP_CheckLogin", sqlcon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@taikhoan", login.TaiKhoan);
                cmd.Parameters.AddWithValue("@matkhau", login.MatKhau);

                cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 100);
                cmd.Parameters["@Code"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 100);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Data", SqlDbType.NVarChar, 100);
                cmd.Parameters["@Data"].Direction = ParameterDirection.Output;

                var reader = cmd.ExecuteNonQuery();
                resMess.Code = Convert.ToInt32(cmd.Parameters["@Code"].Value);
                resMess.Message = Convert.ToString(cmd.Parameters["@Message"].Value);
                resMess.Data = Convert.ToString(cmd.Parameters["@Data"].Value);

                 return resMess;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
