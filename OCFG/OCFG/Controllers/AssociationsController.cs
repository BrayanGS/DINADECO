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
    public class AssociationsController : Controller
    {
        private OCFG_DataBaseEntities db = new OCFG_DataBaseEntities();

        // GET: Associations
        public ActionResult Index()
        {
            var association = db.Association.Include(a => a.Employee).Include(a => a.ConcreteLiquidation).Include(a => a.EconomicReport).Include(a => a.Settlement).Include(a => a.WorkPlan);
            return View(association.ToList());
        }

        // GET: Associations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Association association = db.Association.Find(id);
            if (association == null)
            {
                return HttpNotFound();
            }
            return View(association);
        }

        // GET: Associations/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.Employee, "id", "name_employee");
            ViewBag.id = new SelectList(db.ConcreteLiquidation, "id", "status");
            ViewBag.id = new SelectList(db.EconomicReport, "id", "balance");
            ViewBag.id = new SelectList(db.Settlement, "id", "status");
            ViewBag.id = new SelectList(db.WorkPlan, "id", "status");
            return View();
        }

        // POST: Associations/Create
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

        // GET: Associations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Association association = db.Association.Find(id);
            if (association == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.Employee, "id", "name_employee", association.id);
            ViewBag.id = new SelectList(db.ConcreteLiquidation, "id", "status", association.id);
            ViewBag.id = new SelectList(db.EconomicReport, "id", "balance", association.id);
            ViewBag.id = new SelectList(db.Settlement, "id", "status", association.id);
            ViewBag.id = new SelectList(db.WorkPlan, "id", "status", association.id);
            return View(association);
        }

        // POST: Associations/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,resgistry_code,name,region,canton")] Association association)
        {
            if (ModelState.IsValid)
            {
                db.Entry(association).State = EntityState.Modified;
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

        // GET: Associations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Association association = db.Association.Find(id);
            if (association == null)
            {
                return HttpNotFound();
            }
            return View(association);
        }

        // POST: Associations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Association association = db.Association.Find(id);
            db.Association.Remove(association);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
