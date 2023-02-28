using DataAccess.StudentAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.StudentAPI.BUS
{
    public interface ILoginSinhVien
    {
        CResponeMessage CheckLogin(LoginModel login);
    }
}
