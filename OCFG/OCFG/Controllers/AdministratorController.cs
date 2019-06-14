using OCFG.Data;
using OCFG.Models;
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
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                administratorData.deleteEmployee(id);

                return RedirectToAction("Search");
            }
            catch
            {
                return View();
            }
        }

    }
}