using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OCFG.Models
{
    public class Bitacora
    {
        private string name;
        private string lasName;
        private DateTime date;
        private string action;

        public Bitacora()
        {
        }

        public string Name { get => name; set => name = value; }
        public string LasName { get => lasName; set => lasName = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Action { get => action; set => action = value; }
    }
}