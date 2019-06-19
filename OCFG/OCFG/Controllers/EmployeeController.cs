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
        EconomicReport economicReport = new EconomicReport();
        WorkPlan workPlan = new WorkPlan();
        Settlement settlement = new Settlement();
        ConcreteLiquidation concreteLiquidation = new ConcreteLiquidation();

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        // GET: Employee/Search
        public ActionResult Search(int loginUser)
        {
            LoginUser User = employeeData.getLoginUser(loginUser);
            this.ViewBag.User = User.NameLogin;
            List<Association> associations = new List<Association>();

            return View(associations);
        }

        // POST: Employee/Search
        [HttpPost]
        public ActionResult Search(string search, string filter, int loginUser)
        {
            List<Association> associations = new List<Association>();
            LoginUser User = employeeData.getLoginUser(loginUser);
            this.ViewBag.User = User.NameLogin;
            this.ViewBag.Id = loginUser;

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
                int permit = 0;
                DateTime dateOut = new DateTime(0001, 1, 1);
                Officer officer = new Officer(0, null, null, null);
                Employee employeeInsert = new Employee(0, name, lastName, idCard, address, phoneNumber, email, dateIn, dateOut, officer, ICanton, permit);
                employeeData.insertEmployee(employeeInsert);
                return RedirectToAction("Menu", "Administrator");
            }
            catch
            {
                List<Canton> cantons = cantonData.getCantonWithoutAssociation();
                ViewData["cantons"] = cantons;
                return View();
            }
        }

        // GET: Association/Edit/5
        public ActionResult Edit(int id, int idLogin)
        {
            this.ViewBag.Id = idLogin;
            association = employeeData.getAssociationId(id);
            association.RegistryCode = id;
            return View(association);
        }

        // POST: Association/Edit/5
        [HttpPost]
        public ActionResult Edit(Association assoUpdate, int idLogin)
        {
            try
            {
                this.ViewBag.Id = idLogin;
                associationData.updateAssociation(assoUpdate);
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

        //Edit documents by employee
        /*Edit By Employee For Association*/

        // GET: Association/EditEconomicReport/5
        public ActionResult EditEconomicReport(int id, int idLogin)
        {
            this.ViewBag.Id = idLogin;
            economicReport = employeeData.getEconomicReportById(id);
            economicReport.Id = id;
            return View(economicReport);
        }

        //GET: Association/EditWorkPlan/5
        public ActionResult EditWorkPlan(int id, int idLogin)
        {
            this.ViewBag.Id = idLogin;
            workPlan = employeeData.getWorkPlanById(id);
            workPlan.Id = id;
            return View(workPlan);
        }

        //GET: Employee/EditConcreteLiquidation/5
        public ActionResult EditConcreteLiquidation(int id, int idLogin)
        {
            this.ViewBag.Id = idLogin;
            concreteLiquidation = employeeData.getConcreteById(id);
            concreteLiquidation.Id = id;
            return View(concreteLiquidation);
        }

        //GET: Employee/EditConcreteLiquidation/5
        public ActionResult EditSettlement(int id, int idLogin)
        {
            this.ViewBag.Id = idLogin;
            settlement = employeeData.getSettlementById(id);
            settlement.Id = id;
            return View(settlement);
        }

        /*HTTPPOST*/

        // POST: Association/Edit/5
        [HttpPost]
        public ActionResult EditWorkPlan(int id ,string assemblyDate, int idLogin)
        {
            try
            {
                this.ViewBag.Id = idLogin;
                WorkPlan workPlan = new WorkPlan(id,assemblyDate);
                employeeData.updateWorkPlan(workPlan);
                employeeData.insertBitacora(idLogin, "Modifico una asociacion");
                return RedirectToAction("Search", "Employee", new {loginUser = idLogin} );
            }
            catch
            {
                return View();
            }
        }

        // POST: Association/Edit/5
        [HttpPost]
        public ActionResult EditEconomicReport(int id, DateTime dateReceived, string year, float balance, int idLogin)
        {
            try
            {
                this.ViewBag.Id = idLogin;
                EconomicReport economicReport = new EconomicReport(id, dateReceived, year, balance);
                employeeData.updateEconomicReport(economicReport);
                employeeData.insertBitacora(idLogin, "Modifico una asociacion");
                return RedirectToAction("Search", "Employee", new { loginUser = idLogin });
            }
            catch
            {
                return View();
            }
        }

        // POST: Association/Edit/5
        [HttpPost]
        public ActionResult EditConcreteLiquidation(int id, DateTime dateReceived, string year, int idLogin)
        {
            try
            {
                this.ViewBag.Id = idLogin;
                ConcreteLiquidation concreteLiquidation = new ConcreteLiquidation(id, dateReceived, year);
                employeeData.updateConcreteLiquidation(concreteLiquidation);
                employeeData.insertBitacora(idLogin, "Modifico una asociacion");
                return RedirectToAction("Search", "Employee", new { loginUser = idLogin });
            }
            catch
            {
                return View();
            }
        }

        // POST: Association/Edit/5
        [HttpPost]
        public ActionResult EditSettlement(int id, DateTime dateReceived, string year, int idLogin)
        {
            try
            {
                this.ViewBag.Id = idLogin;
                Settlement settlement = new Settlement(id, dateReceived, year);
                employeeData.updateSettlement(settlement);
                employeeData.insertBitacora(idLogin, "Modifico la asociacion");
                return RedirectToAction("Search", "Employee", new { loginUser = idLogin });
            }
            catch
            {
                return View();
            }
        }
    }
}
