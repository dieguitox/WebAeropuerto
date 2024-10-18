using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntidadesCompartidas;
using Persistencias;
using Logica;

namespace Empleados.Controllers
{
    public class AeropuertosController : Controller
    {
        // GET: Aeropuertos
        public ActionResult Inicio()
        {    
            List<Aeropuertos> listado = FabricaLogica.GetLogicaAeropuertos().ListadoAeropuertos();
            return View(listado);
        }
        
        //Listar
        public ActionResult ListarAeropuertos(string dato)
        {
            try
            {
                List<Aeropuertos> lista = null;

                if (Session["Aeropuertos"] == null)
                {
                    lista = FabricaLogica.GetLogicaAeropuertos().ListadoAeropuertos();
                    Session["Aeropuertos"] = lista;
                }
                else
                    lista = (List<Aeropuertos>)Session["Aeropuertos"];

                if (lista.Count >= 1)
                {
                    if (string.IsNullOrEmpty(dato))
                        return View(lista);
                    else
                    {
                        lista = (from unA in lista
                                 where unA.NombreA.ToLower().StartsWith(dato.ToLower())
                                 select unA).ToList();
                    }
                }
                else
                    throw new Exception("No hay aeropuertos para mostrar");

                return View(lista);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new List<Aeropuertos>());
            }
        }
        
        //Buscar
        [HttpGet]
        public ActionResult BuscarAeropuerto(string CodigoA)
        {
            try
            {
                Aeropuertos a = FabricaLogica.GetLogicaAeropuertos().BuscarAeropuertoActivo(CodigoA);
                if (a != null)
                    return View(a);
                else
                    throw new Exception("No existe el Aeropuerto");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new Aeropuertos());
            }
        }

        //Alta
        [HttpGet]
        public ActionResult AltaAeropuerto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AltaAeropuerto(Aeropuertos unA)
        {
            try
            {
                unA.Validar();

                FabricaLogica.GetLogicaAeropuertos().AltaAeropuertos(unA);
                return RedirectToAction("ListarAeropuertos", "Aeropuertos");

            }
            catch (Exception ex)
            {

                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        //Modificar
        [HttpGet]
        public ActionResult ModificarAeropuerto(string CodigoA)
        {
            try
            {
                Aeropuertos a = FabricaLogica.GetLogicaAeropuertos().BuscarAeropuertoActivo(CodigoA);
                if (a != null)
                    return View(a);
                else
                    throw new Exception("No existe el Aeropuerto");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new Aeropuertos());
            }
        }

        [HttpPost]
        public ActionResult ModificarAeropuerto(Aeropuertos unA)
        {
            try
            {
                unA.Validar();

                FabricaLogica.GetLogicaAeropuertos().ModificarAeropuertos(unA);
                ViewBag.Mensaje = "Modificacion Exitosa";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }

        }

        //Eliminar
        [HttpGet]
        public ActionResult EliminarAeropuerto(string CodigoA)
        {
            try
            {
                Aeropuertos a = FabricaLogica.GetLogicaAeropuertos().BuscarAeropuertoActivo(CodigoA);
                if (a != null)
                    return View(a);
                else
                    throw new Exception("No existe el Aeropuerto");

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult EliminarAeropuerto(Aeropuertos unA)
        {
            try
            {
                FabricaLogica.GetLogicaAeropuertos().BajaAeropuertos(unA);
                return RedirectToAction("ListarAeropuertos", "Aeropuertos");

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }
    }
}