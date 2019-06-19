using OCFG.Data;
using OCFG.Models;
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
        Officer officer = new Officer();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(string userName, string password)
        {
            string mensaje = " ";
            try
            {
                string rol = loginData.getRolEmployee(userName, password);
                if (rol.Equals("Administrador"))
                {
                    this.ViewBag.Message = mensaje;
                    return RedirectToAction("Menu", "Administrator");
                }
                else if (rol.Equals("Empleado"))
                {
                    this.ViewBag.Message = mensaje;
                    return RedirectToAction("Search", "Employee");
                }
                else {
                    mensaje = "*Usuario no encontrado, verifique los datos";
                    this.ViewBag.Message = mensaje;
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
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

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
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
