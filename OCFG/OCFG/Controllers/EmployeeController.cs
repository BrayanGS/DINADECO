using iTextSharp.text;
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
    public class EmployeeController: Controller
    {
        EmployeeData employeeData = new EmployeeData();
        static Employee employee = new Employee();
        static Officer officer = new Officer();


        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: Employee/Search
        public ActionResult Search()
        {
            List<Association> associations = new List<Association>();

            return View(associations);
        }

        // GET: Employee/Create
        public ActionResult Create(){

            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(string name, string lastName, string idCard, string address, string phoneNumber,
            string email, DateTime dateIn, string[] cantons)
        {
            Canton canton = new Canton();
            List<Canton> cantons1 = new List<Canton>();
            try
            {

                for (int i = 0; i < cantons.Length; i++)
                {
                    canton.Id = i;
                    canton.Name = cantons[i];
                    cantons1.Insert(i,canton);
    
                }

                DateTime dateOut = new DateTime(0001,1,1);
                Officer officer = new Officer(0, null, null, null);
                Employee employeeInsert = new Employee(0, name, lastName, idCard, address, phoneNumber, email, dateIn, dateOut, officer, cantons1);
                employeeData.insertEmployee(employeeInsert);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        /*
     // POST: Association/Search

     [HttpPost]
     public ActionResult Search(string search, string filter)
     {
         List<Employee> employees = new List<Employee>();

         if (!String.IsNullOrEmpty(search))
         {
             employees = employeeData.getAssociationsByFilter(search, filter);
         }
         return View(associations);
     }


     

     // GET: Association/Edit/5
     public ActionResult Edit(int id)
     {
         association = associationData.getAssociationById(id);
         return View(association);
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
     */
    }
}
