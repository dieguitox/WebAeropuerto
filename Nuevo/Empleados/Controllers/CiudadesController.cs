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
    public class CiudadesController : Controller
    {
        // GET: Ciudades
        public ActionResult Index()
        {
            return View();
        }

        //Listado
        public ActionResult ListadoCiudades(string dato)
        {
            try
            {
                List<Ciudades> lista = FabricaLogica.GetLogicaCiudades().ListarCiudad();

                if (lista.Count >= 1)
                {
                    if (string.IsNullOrEmpty(dato))
                        return View(lista);
                    else
                    {
                        lista = (from unC in lista
                                 where unC.Ciudad.ToLower().StartsWith(dato.ToLower())
                                 select unC).ToList();
                        return View(lista);
                    }
                }
                else
                    throw new Exception("No hay ciudades que mostrar");
                
            }
            catch (Exception ex) 
            {
                ViewBag.Mensaje = ex.Message;
                return View(new List<Ciudades>());
            }
        }

        //Consulta
        public ActionResult BuscarCiudades(string CodigoC)
        {
            try
            {
                Ciudades c = FabricaLogica.GetLogicaCiudades().BuscarCiudadesActivas(CodigoC);
                if (c != null)
                    return View(c);
                else
                    throw new Exception("No existe la Ciudad");

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        //Alta
        [HttpGet]
        public ActionResult AltaCiudad()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AltaCiudad(Ciudades unC)
        {
            try
            {
                unC.Validar();

                FabricaLogica.GetLogicaCiudades().AltaCiudades(unC);

                return RedirectToAction("ListadoCiudades", "Ciudades");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        //Baja
        [HttpGet]
        public ActionResult BajaCiudad(string CodigoC)
        {
            try
            {
                Ciudades c = FabricaLogica.GetLogicaCiudades().BuscarCiudadesActivas(CodigoC);
                if (c != null)
                    return View(c);
                else
                    throw new Exception("No existe la Ciudad");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult BajaCiudad(Ciudades unC)
        {
            try
            {
                FabricaLogica.GetLogicaCiudades().BajaCiudades(unC);
                return RedirectToAction("ListadoCiudades", "Ciudades");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        //Modificar
        [HttpGet]
        public ActionResult ModificarCiudad(string CodigoC)
        {
            try
            {
                Ciudades c = FabricaLogica.GetLogicaCiudades().BuscarCiudadesActivas(CodigoC);
                if (c != null)
                    return View(c);
                else
                    throw new Exception("No existe la Ciudad");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult ModificarCiudad(Ciudades unC)
        {
            try
            {
                unC.Validar();

                FabricaLogica.GetLogicaCiudades().ModificarCiudades(unC);
                ViewBag.Mensaje = "Modificacion Exitosa";
                return View(unC);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }
    }
}