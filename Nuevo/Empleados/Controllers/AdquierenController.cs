using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntidadesCompartidas;
using Logica;

namespace Empleados.Controllers
{
    public class AdquierenController : Controller
    {
        // GET: Adquieren
  
        [HttpGet]
        public ActionResult AltaAdquieren(int nroTicket, string codViaje)
        {
            ViewBag.NroTicket = nroTicket;
            ViewBag.CodigoViaje = codViaje;

            var model = new Adquieren
            {
                NroTicket = new Ventas { NroTicket = nroTicket },
                NroPasaporte = new Clientes(),
                NroAsiento = 0
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AltaAdquieren(Adquieren unaF, int nroTicket, string codViaje)
        {
            try
            {
                unaF.NroTicket = new Ventas { NroTicket = nroTicket, Vue = new Vuelos { CodigoV = codViaje } };
                unaF.Validar();

                FabricaLogica.GetLogicaAdquieren().AltaAdquieren(unaF);
                return RedirectToAction("MostrarFactura", "Ventas", new { factura = unaF.NroTicket.NroTicket});
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }  
    }
}