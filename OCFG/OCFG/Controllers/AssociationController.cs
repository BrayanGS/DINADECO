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
        public ActionResult Create(int registryCode, string name, string region, string ICanton, string status,string active, string province, string legalDocument, int type, int idLogin)
        {
            try
            {
              
                Association associationInsert = new Association(0, registryCode, name, region, ICanton, status, active, province, legalDocument,null,null,null, type, null, null, null, null,null);
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
        public ActionResult Edit(Association assoUpdate)
        {
            try
            {
                associationData.updateAssociation(assoUpdate);
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
    }
}
