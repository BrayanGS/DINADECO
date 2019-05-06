using OCFG.Data;
using OCFG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // GET: Association/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Association/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

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
