using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OCFG.Models
{
    public class LoginUser
    {
        private int idEmployee;
        private  string idCardLogin;
        private  string nameLogin;
        private  string lastNameLogin;

        public LoginUser()
        {
        }

        public  string IdCardLogin { get => idCardLogin; set => idCardLogin = value; }
        public  string NameLogin { get => nameLogin; set => nameLogin = value; }
        public  string LastNameLogin { get => lastNameLogin; set => lastNameLogin = value; }
        public int IdEmployee { get => idEmployee; set => idEmployee = value; }
    }
}