using iTextSharp.text;
using OCFG.Data;
using OCFG.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OCFG.Controllers
{
    public class AssociationController : Controller
    {
        AssociationData associationData = new AssociationData();
        static Association association = new Association();
        CantonData cantonData = new CantonData();
        EmployeeData employeeData = new EmployeeData();

        // GET: Association
        public ActionResult Index()
        {
            return View();
        }

        // GET: Association/Details/5
        public ActionResult Details(int id)
        {
            association = associationData.getAssociationById(id);
            return View(association);
        }


        // GET: Association/Search
        public ActionResult Search()
        {
            List<Association> associations = new List<Association>();

            return View(associations);
        }

        // POST: Association/Search
        [HttpPost]
        public ActionResult Search(string search, string filter)
        {
            List<Association> associations = new List<Association>();

            if (!String.IsNullOrEmpty(search))
            {
                associations = associationData.getAssociationsByFilter(search, filter);
            }
            return View(associations);
        }

        // GET: Association/Create
        public ActionResult Create(int idLogin)
        {
            ViewBag.Id = idLogin;
            List<Canton> cantons = cantonData.getAll();
            ViewData["cantons"] = cantons;

            return View();
        }

        // POST: Association/Create
        [HttpPost]
        public ActionResult Create(int registryCode, string name, string region, string ICanton, string status, string active, string province, string legalDocument, string superavit, string adequacy, string affiavit, int type, int idLogin)
        {
            try
            {
                string nameAssociation = getNameAssociation(type, name);

                Association associationInsert = new Association(0, registryCode, nameAssociation, region, ICanton, status, active, province, legalDocument, superavit, adequacy, affiavit, type, null, null, null, null, null);
                associationData.insertarAssociation(associationInsert, idLogin);
                return RedirectToAction("Index");
            }
            catch
            {
                List<Canton> cantons = cantonData.getAll();
                ViewData["cantons"] = cantons;
                return View();
            }
        }

        // GET: Association/Edit/5
        public ActionResult Edit(int id)
        {
            association = associationData.getAssociation(id);
            return View(association);
        }


        // POST: Association/Edit/5
        [HttpPost]
        public ActionResult Edit(int Id, string adequacy, string affiavit, string superavit)
        {
            try
            {
                associationData.updateAssociation(Id, adequacy, affiavit, superavit);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Association/Delete/5
        public ActionResult Delete(int id)
        {

            return View();
        }

        // POST: Association/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //Methods for reports
        // GET: Association/DetailsForReport/5
        public ActionResult DetailsForReport(int id)
        {

            DateTime date = DateTime.Now;
            this.ViewBag.Message = date;
            association = associationData.getAssociationById(id);
            return View(association);
        }

        public ActionResult PrintDeatilsAssociation(int id)
        {

            return new ActionAsPdf("DetailsForReport", new { id }) { FileName = "Informe Asociación " + id + ".pdf", PageSize = Rotativa.Options.Size.A4 };
        }

        //get name association

        public string getNameAssociation(int type, string name) {
            string nameAssociation = " ";
            switch (type)
            {
                case 1:
                    nameAssociation = "ASOCIACIÓN DE DESARROLLO ESPECÍFICAPRO " + name.ToUpper();
                    break;
                case 2:
                    nameAssociation = "ASOCIACIÓN DE DESARROLLO INTEGRAL " + name.ToUpper();
                    break;
                case 3:
                    nameAssociation = "UNION ZONAL " + name.ToUpper();
                    break;
                case 4:
                    nameAssociation = "FEDERACION DE UNIONES " + name.ToUpper();
                    break;
                case 5:
                    nameAssociation = "ASOCIACION DE DESARROLLO INTEGRAL DE RESERVA INDIGENA " + name.ToUpper();
                    break;
                case 10:
                    nameAssociation = "DISUELTA " + name.ToUpper();
                    break;
                default:
                    break;
            }
            return nameAssociation;
        }
    }
}
