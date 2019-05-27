using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OCFG.Models
{
    public class Canton
    {
        private int id;
        private string name;
        private Employee employee;

        public Canton()
        {

        }

        public Canton(int id, string name, Employee employee)
        {
            this.id = id;
            this.name = name;
            this.employee = employee;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public Employee Employee { get => employee; set => employee = value; }
    }
}