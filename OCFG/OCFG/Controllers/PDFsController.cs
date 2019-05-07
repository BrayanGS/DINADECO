using OCFG.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OCFG.Controllers
{
    public class PDFsController : Controller
    {
        PDFReportAssotiationForEmployee pDFReportAssotiationForEmployee;

        // GET: PDFs
        public ActionResult Index()
        {
            //pDFReportAssotiationForEmployee = new PDFReportAssotiationForEmployee();
            return View();
        }

        // GET: PDFs/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PDFs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PDFs/Create
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

        // GET: PDFs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PDFs/Edit/5
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

        // GET: PDFs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PDFs/Delete/5
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
