using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntidadesCompartidas;
using Logica;

namespace Empleados.Controllers
{
    public class VentasController : Controller
    {
        // GET: Ventas
        [HttpGet]
        public ActionResult AltaVenta(string NroPasaporte)
        {
            try
            {
                List<Vuelos> listadoV = FabricaLogica.GetLogicaVuelo().ListadoVuelos();
                List<Clientes> listaC = FabricaLogica.GetLogicaClientes().ListadoClientes();

                ViewBag.ListadoVuelos = new SelectList(listadoV, "CodigoV", "CodigoV");
                ViewBag.ListadoClientes = new SelectList(listaC, "NroPasaporte", "NroPasaporte");

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ListadoVuelos = new SelectList(null);
                ViewBag.ListadoClientes = new SelectList(null);
                ViewBag.Mensaje = ex.Message;
                return View();
            }   
            
        }

        [HttpPost]
        public ActionResult AltaVenta(Ventas unaV)
        {
            try
            {
                unaV.Emp = (EntidadesCompartidas.Empleados)Session["Empleados"];
                unaV.Cli = FabricaLogica.GetLogicaClientes().BuscarClientesActivos(unaV.Cli.NroPasaporte);
                unaV.Vue = FabricaLogica.GetLogicaVuelo().BuscarVuelo(unaV.Vue.CodigoV);

                unaV.Validar();
                FabricaLogica.GetLogicaVenta().AltaVenta(unaV);

                return RedirectToAction("AltaAdquieren", "Adquieren", new { nroTicket = unaV.NroTicket, codViaje = unaV.Vue.CodigoV});
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult MostrarFactura(int factura)
        {
            try
            {
                Ventas v = FabricaLogica.GetLogicaVenta().BuscarVenta(factura);
                Adquieren a = FabricaLogica.GetLogicaAdquieren().ListadoAdquieren().Where(x => x.NroTicket.NroTicket == factura).FirstOrDefault();

                if (v != null && a != null)
                {
                    ViewBag.Adquiere = a;
                    return View(v);
                }
                else
                    throw new Exception("No existe la factura");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }
    }
}