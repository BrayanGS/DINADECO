using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OCFG.Models
{
    public class Manager
    {
        private int idManager;
        private Officer officer;

        public Manager(int idManager, Officer officer)
        {
            this.idManager = idManager;
            this.officer = officer;
        }

        public int IdManager { get => idManager; set => idManager = value; }
        public Officer Officer { get => officer; set => officer = value; }
    }
}