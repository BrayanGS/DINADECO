using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OCFG.Models
{
    public class ViewAssociation
    {
        private int idAssociation;
        private string nameAssociation;

        public ViewAssociation()
        {
        }

        public ViewAssociation(int idAssociation)
        {
            this.IdAssociation = idAssociation;
        }

        public int IdAssociation { get => idAssociation; set => idAssociation = value; }
        public string NameAssociation { get => nameAssociation; set => nameAssociation = value; }
    }
}