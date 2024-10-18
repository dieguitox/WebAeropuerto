using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntidadesCompartidas;
using Logica;

namespace Empleados.Controllers
{
    public class VuelosController : Controller
    {
        // GET: Vuelos
        public ActionResult Inicio()
        {
            List<Vuelos> listado = FabricaLogica.GetLogicaVuelo().ListadoVuelos();
            return View(listado);
        }

        public ActionResult ListadoVuelos(string aero,string fecha,string fechaS)
        {
            try
            {
                List<Vuelos> lista = null;

                if (Session["Vuelos"] == null)
                {
                    lista = FabricaLogica.GetLogicaVuelo().ListadoVuelos();
                    Session["Vuelos"] = lista;
                }
                else
                    lista = (List<Vuelos>)Session["Vuelos"];

                if (lista.Count >= 1)
                {
                    if (!string.IsNullOrEmpty(aero))
                    {
                        lista = (from unA in lista
                                 where unA.CodA.NombreA.ToLower().StartsWith(aero.ToLower())
                                 select unA).ToList();
                    }

                    if (!string.IsNullOrEmpty(fecha))
                    {
                        lista = (from unA in lista
                                 where Convert.ToDateTime(unA.FechaA) == Convert.ToDateTime(fecha)
                                 select unA).ToList();
                    }

                    if (!string.IsNullOrEmpty(fechaS))
                    {
                        lista = (from unA in lista
                                 where Convert.ToDateTime(unA.FechaD) == Convert.ToDateTime(fechaS)
                                 select unA).ToList();
                    }
                    
                }
                else
                    throw new Exception("No hay Vuelos para mostrar");

                return View(lista);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new List<Vuelos>());
            }
        }

        [HttpGet]
        public ActionResult AltaVuelos()
        {
            try
            {
                List<Aeropuertos> listaA = FabricaLogica.GetLogicaAeropuertos().ListadoAeropuertos();
                ViewBag.ListadoVuelos = new SelectList(listaA, "CodigoA", "NombreA");

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ListaArticulos = new SelectList(null);
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }
        //Alta
        [HttpPost]
        public ActionResult AltaVuelos(Vuelos unV)
        {
            try
            {

                if(unV.CodA.CodigoA.Trim().Length > 0)
                {
                    FabricaLogica.GetLogicaAeropuertos().BuscarAeropuertoActivo(unV.CodA.CodigoA);
                }
                if (unV.CodB.CodigoA.Trim().Length > 0)
                {
                    FabricaLogica.GetLogicaAeropuertos().BuscarAeropuertoActivo(unV.CodB.CodigoA);
                }

                unV.Validar();
                FabricaLogica.GetLogicaVuelo().AltaVuelos(unV);

                ViewBag.Mensaje = "El vuelo creado es:" + unV.CodigoV;
                return RedirectToAction("AltaVuelos", "Vuelos");

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }
        //Buscar
        [HttpGet]
        public ActionResult BuscarVuelo(string CodigoV)
        {
            try
            {
                Vuelos vuelo = FabricaLogica.GetLogicaVuelo().BuscarVuelo(CodigoV);
                if (vuelo != null)
                    return View(vuelo);
                else
                    throw new Exception("No existe el Vuelo");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new List<Vuelos>());
            }
        }
    }
}