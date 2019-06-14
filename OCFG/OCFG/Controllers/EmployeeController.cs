using OCFG.Data;
using OCFG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OCFG.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeData employeeData = new EmployeeData();
        AssociationData associationData = new AssociationData();
        Association association = new Association();
        CantonData cantonData = new CantonData();

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        // GET: Employee/Search
        public ActionResult Search()
        {
            List<Association> associations = new List<Association>();

            return View(associations);
        }

        // POST: Employee/Search
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

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            association = associationData.getAssociationById(id);
            return View(association);
        }


        // GET: Employee/Create
        public ActionResult Create()
        {
            List<Canton> cantons = cantonData.getCantonWithoutAssociation();
            ViewData["cantons"] = cantons;
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(string name, string lastName, string idCard, string address, string phoneNumber,
            string email, DateTime dateIn, string[] ICanton)
        {
            try
            {

                DateTime dateOut = new DateTime(0001, 1, 1);
                Officer officer = new Officer(0, null, null, null);
                Employee employeeInsert = new Employee(0, name, lastName, idCard, address, phoneNumber, email, dateIn, dateOut, officer, ICanton);
                employeeData.insertEmployee(employeeInsert);
                return RedirectToAction("Menu","Administrator");
            }
            catch
            {
                List<Canton> cantons = cantonData.getCantonWithoutAssociation();
                ViewData["cantons"] = cantons;
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
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

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {

            return View();

        }
    }
}
