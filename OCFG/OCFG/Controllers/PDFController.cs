using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OCFG.Models.OCFG_Data;

namespace OCFG.Controllers
{
    public class PDFController : Controller
    {
        private OCFG_DataBaseEntities db = new OCFG_DataBaseEntities();

        // GET: PDF
        public ActionResult Index()
        {
            var association = db.Association.Include(a => a.Employee).Include(a => a.ConcreteLiquidation).Include(a => a.EconomicReport).Include(a => a.Settlement).Include(a => a.WorkPlan);
            return View(association.ToList());
        }
        
        // GET: PDF/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.Employee, "id", "name_employee");
            ViewBag.id = new SelectList(db.ConcreteLiquidation, "id", "status");
            ViewBag.id = new SelectList(db.EconomicReport, "id", "balance");
            ViewBag.id = new SelectList(db.Settlement, "id", "status");
            ViewBag.id = new SelectList(db.WorkPlan, "id", "status");
            return View();
        }

        // POST: PDF/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,resgistry_code,name,region,canton")] Association association)
        {
            if (ModelState.IsValid)
            {
                db.Association.Add(association);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.Employee, "id", "name_employee", association.id);
            ViewBag.id = new SelectList(db.ConcreteLiquidation, "id", "status", association.id);
            ViewBag.id = new SelectList(db.EconomicReport, "id", "balance", association.id);
            ViewBag.id = new SelectList(db.Settlement, "id", "status", association.id);
            ViewBag.id = new SelectList(db.WorkPlan, "id", "status", association.id);
            return View(association);
        }

       
    }
}
