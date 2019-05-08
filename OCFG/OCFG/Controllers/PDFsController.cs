using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OCFG.Controllers
{
    public class PdfsController : Controller
    {
        // GET: Pdfs
        public ActionResult Index()
        {
            return View();
        }

        // GET: Pdfs/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pdfs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pdfs/Create
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

        // GET: Pdfs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pdfs/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pdfs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pdfs/Delete/5
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
