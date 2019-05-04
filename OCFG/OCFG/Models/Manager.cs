using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OCFG.Models
{
    public class Manager
    {
        private int idManager;
        private string userName;
        private string password;
        private Employee employee;

        public Manager(int idManager, string userName, string password, Employee employee)
        {
            this.idManager = idManager;
            this.userName = userName;
            this.password = password;
            this.employee = employee;
        }

        public int IdManager { get => idManager; set => idManager = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public Employee Employee { get => employee; set => employee = value; }
    }
}