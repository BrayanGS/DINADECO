﻿using iTextSharp.text;
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
        static WorkPlan workPlan = new WorkPlan();
        static Settlement settlement = new Settlement();
        static ConcreteLiquidation concreteLiquidation = new ConcreteLiquidation();
        static EconomicReport economicReport = new EconomicReport();
        CantonData cantonData = new CantonData();

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
        public ActionResult Create()


        {
            List<Canton> cantons = cantonData.getAll();
            ViewData["cantons"] = cantons;

            return View();
        }

        // POST: Association/Create
        [HttpPost]
        public ActionResult Create(int registryCode, string name, string region, string ICanton, string status,string active, string province, string legalDocument, int type )
        {
            try
            {
              
                Association associationInsert = new Association(0, registryCode, name, region, ICanton, status, active, province, legalDocument,null,null,null, type, null, null, null, null,null);
                associationData.insertarAssociation(associationInsert);
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
            association = associationData.getAssociationById(id);
            return View(association);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assoUpdate"></param>
        /// <returns></returns>
        public ActionResult EditWorkPlan(Association assoUpdate)
        {
            try
            {
                associationData.updateWorkPlan(assoUpdate);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assoUpdate"></param>
        /// <returns></returns>
        public ActionResult EditEconomicReport(Association assoUpdate)
        {
            try
            {
                associationData.updateEconomicReport(assoUpdate);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assoUpdate"></param>
        /// <returns></returns>
        public ActionResult EditSettlement(Association assoUpdate)
        {
            try
            {
                associationData.updateSettlement(assoUpdate);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assoUpdate"></param>
        /// <returns></returns>
        public ActionResult EditConcrete(Association assoUpdate)
        {
            try
            {
                associationData.updateConcreteLiquidation(assoUpdate);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
    }
}
