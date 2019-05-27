using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OCFG.Models
{
    public class Officer
    {
        private int idOfficer;
        private string userName;
        private string password;
        private string departament;

        public Officer()
        {
        }

        public Officer(int idOfficer, string userName, string password, string departament)
        {
            this.idOfficer = idOfficer;
            this.userName = userName;
            this.password = password;
            this.departament = departament;
        }

        public int IdOfficer { get => idOfficer; set => idOfficer = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string Departament { get => departament; set => departament = value; }
    }
}