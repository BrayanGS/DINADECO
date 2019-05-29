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
        public ActionResult Edit(DateTime dateOut, string phoneNumber, Employee idEmployee)
        {
            employee = new Employee(dateOut, phoneNumber);
            canton = new Canton(idEmployee);
            try
            {
                administratorData.updateEmployee(employee, canton);
                return RedirectToAction("Details","Administrator");
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

    }
}