using OCFG.Data;
using OCFG.Models;
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

        // GET: Association
        public ActionResult Index()
        {
            return View();
        }

        // GET: Association/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Association/Search
        public ActionResult Search()
        {
            return View();
        }

        // POST: Association/Search
        [HttpPost]
        public ActionResult Search(string search)
        {
            List<Association> associations = new List<Association>();

            if (!String.IsNullOrEmpty(search))
            {
                associations = associationData.getAssociationsByFilter(search);
            }
            else
            {
                associations = associationData.getAssociations();
            }
            return View(associations);
        }

        // GET: Association/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Association/Create
        [HttpPost]
        public ActionResult Create(int registryCode, string name, string region, string canton, string status,string active, string province)
        {
            try
            {
                Association associationInsert = new Association(0, registryCode, name, region, canton, status, active, province, null, null, null, null);
                associationData.insertarAssociation(associationInsert);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Association/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Association/Edit/5
        [HttpPost]
        public ActionResult Edit(Association assoUpdate)
        {
            try
            {
                association.EconomicReport.DateReceived = assoUpdate.EconomicReport.DateReceived;
                association.EconomicReport.Year = association.EconomicReport.Year;
                association.EconomicReport.Balance = assoUpdate.EconomicReport.Balance;
                association.Settlement.DateReceived = assoUpdate.Settlement.DateReceived;
                association.Settlement.Year = assoUpdate.Settlement.Year;
                association.WorkPlan.AssemblyDate = assoUpdate.WorkPlan.AssemblyDate;
                association.ConcreteLiquidation.DateReceived = assoUpdate.ConcreteLiquidation.DateReceived;
                association.ConcreteLiquidation.Year = assoUpdate.ConcreteLiquidation.Year;

                associationData.updateAssociation(association);

                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditWorkPlan(Association assoUpdate)
        {
            try
            {
                association.EconomicReport.DateReceived = assoUpdate.EconomicReport.DateReceived;
                association.EconomicReport.Year = association.EconomicReport.Year;
                association.EconomicReport.Balance = assoUpdate.EconomicReport.Balance;
                association.Settlement.DateReceived = assoUpdate.Settlement.DateReceived;
                association.Settlement.Year = assoUpdate.Settlement.Year;
                association.WorkPlan.AssemblyDate = assoUpdate.WorkPlan.AssemblyDate;
                association.ConcreteLiquidation.DateReceived = assoUpdate.ConcreteLiquidation.DateReceived;
                association.ConcreteLiquidation.Year = assoUpdate.ConcreteLiquidation.Year;

                associationData.updateAssociation(association);

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
    }
}
