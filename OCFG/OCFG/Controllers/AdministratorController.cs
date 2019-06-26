using OCFG.Data;
using OCFG.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OCFG.Controllers
{
    public class AdministratorController : Controller
    {
        AdministratorData administratorData = new AdministratorData();
        EmployeeData employeeData = new EmployeeData();
        static Employee employee = new Employee();
        static Canton canton = new Canton();
        CantonData cantonData = new CantonData();
        AssociationData associationData = new AssociationData();

        public ActionResult Menu()
        {
            return View();
        }

        //GET: Administrator/Edit/111111111
        public ActionResult Edit(string id)
        {
            employee = administratorData.getEmployeeByIdCard(id); 
            return View(employee);
        }

        // POST: Asministrator/Edit/111111111
        [HttpPost]
        public ActionResult Edit(DateTime dateOut, string phoneNumber, string IdCard)
        {
            employee = new Employee(dateOut, phoneNumber);
            Employee empleadoBuscar = administratorData.getEmployeeByIdCard(IdCard);
            canton = new Canton(1, "Alvarado", empleadoBuscar);
            try
            {
                administratorData.updateEmployee(employee, canton);
                return RedirectToAction("Search","Administrator");
            }
            catch
            {
                return View();
            }
        }
        //GET: Administrator/Enable/111111111
        public ActionResult Enable(string id)
        {
            employee = administratorData.getEmployeeByIdCard(id);
            return View(employee);
        }

        // POST: Asministrator/Enable/111111111
        [HttpPost]
        public ActionResult Enable(DateTime dateIn, string phoneNumber, string IdCard)
        {
            try
            {
                administratorData.updateEmployee(dateIn,phoneNumber,IdCard);
                return RedirectToAction("Search", "Administrator");
            }
            catch
            {
                return View();
            }
        }

        // GET: Administrator/Authorization
        public ActionResult Authorization()
        {
            List<Employee> employees = new List<Employee>();
            return View(employees);
        }

        // POST: Administrator/Authorization
        [HttpPost]
        public ActionResult Authorization(string search)
        {
            List<Employee> employees = new List<Employee>();

            if (!String.IsNullOrEmpty(search))
            {
                employees = administratorData.searchEmployeeByFilter(search);
            }
            return View(employees);
        }

        // GET: Administrator/Search
        public ActionResult Search()
        {
            List<Employee> employees = new List<Employee>();
            return View(employees);
        }

        // POST: Administrator/Search
        [HttpPost]
        public ActionResult Search(string search)
        {
            List<Employee> employees = new List<Employee>();

            if (!String.IsNullOrEmpty(search))
            {
                employees = administratorData.searchEmployeeByFilter(search);
            }
            return View(employees);
        }

        // GET: Administrator/Delete/5
        public ActionResult Delete(string id)
        {
            employee = administratorData.getEmployeeByIdCard(id);
            return View(employee);
        }

        // POST: Administrator/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collectionm, DateTime dateOut)
        {
            try
            {
                administratorData.deleteEmployee(id, dateOut);

                return RedirectToAction("Search");
            }
            catch
            {
                return RedirectToAction("Search");
            }
        }


        // GET: Association/Details/5
        public ActionResult Details(string id)
        {
            List<Canton> cantons = cantonData.getByEmployee(id);
            ViewData["cantons"] = cantons;
            employee = employeeData.getEmployeeById(id);
            return View(employee);
        }

        //methods for reports 
        public ActionResult PrintAllAssociations()
        {
            return new ActionAsPdf("GetAllAssociations") { FileName = "Informe General.pdf", PageOrientation = Rotativa.Options.Orientation.Landscape, PageSize = Rotativa.Options.Size.A4};
        }

        public ActionResult PrintStatusAssociations()
        {
            return new ActionAsPdf("GetStatusAssociations") { FileName = "Informe Anual.pdf"};
        }

        // GET: Administrator/GetAllAssociations
        public ActionResult GetAllAssociations()
        {
            DateTime date = DateTime.Now;
            this.ViewBag.Message = date;

            List<Association> associations = new List<Association>();
            associations = associationData.getAllAssociations();
            return View(associations);
        }

        // GET: Administrator/GetStatusAssociations
        public ActionResult GetStatusAssociations()
        {
            DateTime date = DateTime.Now;
            this.ViewBag.Message = date;

            List<Association> associations = new List<Association>();
            associations = associationData.getStatusAssociations();
            return View(associations);
        }

        // GET: Administrator/bitacora
        public ActionResult bitacora()
        {
            List<Bitacora> moviments = new List<Bitacora>();
            DateTime dateTime = DateTime.Now;
            moviments = administratorData.getBitacora(dateTime);
            return View(moviments);
        }
        // POST: Administrator/bitacora/111
        [HttpPost]
        public ActionResult bitacora(DateTime fechaBuscar)
        {
            List<Bitacora> moviments = new List<Bitacora>();
            moviments = administratorData.getBitacora(fechaBuscar);
            return View(moviments);
        }

    }
}