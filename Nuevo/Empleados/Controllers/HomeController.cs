using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntidadesCompartidas;
using Logica;


namespace Empleados.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("Menu", "Home");
        }

        public ActionResult Menu()
        {
            if (Session["Logueo"] is EntidadesCompartidas.Empleados)
            {
                Session["Empleados"] = Session["Logueo"];
                return View();
            }
            else
                return RedirectToAction("Logueo", "Empleados");
        }
    }
}