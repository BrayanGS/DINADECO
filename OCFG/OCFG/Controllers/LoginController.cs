using OCFG.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OCFG.Controllers
{
    public class LoginController : Controller
    {
        LoginData loginData = new LoginData();

        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Index/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Index/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Index/Create
        [HttpPost]
        public ActionResult Create(string userName, string password)
        {
            try
            {
                string rol = loginData.getRolEmployee(userName, password);
                if (rol.Equals("Administrador"))
                {
                    return RedirectToAction("Administrador/MenuAdministrator");
                }
                else if (rol.Equals("Empleado"))
                {
                    return RedirectToAction("Empleado");
                }
                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }

        // GET: Index/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Index/Edit/5
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

        // GET: Index/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Index/Delete/5
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
