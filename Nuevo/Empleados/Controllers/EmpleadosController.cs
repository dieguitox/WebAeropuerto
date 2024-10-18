using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntidadesCompartidas;
using Logica;

namespace Empleados.Controllers
{
    public class EmpleadosController : Controller
    {
        // GET: Empleados
        [HttpGet]
        public ActionResult Logueo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logueo(string usuario, string pass)
        {
            try
            {
                EntidadesCompartidas.Empleados unE = FabricaLogica.GetLogicaEmpleados().Logueo(usuario, pass);

                if (unE != null)
                {
                    Session["Logueo"] = unE;
                    return RedirectToAction("Menu", "Home");
                }
                else
                    throw new Exception("Usuario / Pass incorrectos");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        public ActionResult Deslogueo()
        {
            Session["Logueo"] = null;
            return RedirectToAction("Logueo", "Empleados");
        }

    }
}