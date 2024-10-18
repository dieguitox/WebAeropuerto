using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntidadesCompartidas;
using Logica;

namespace Empleados.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        //Listado
        public ActionResult ListadoClientes(string dato)
        {
            try
            {
                List<Clientes> lista = FabricaLogica.GetLogicaClientes().ListadoClientes();

                if (lista.Count >= 1)
                {
                    if (string.IsNullOrEmpty(dato))
                        return View(lista);
                    else
                    {
                        lista = (from unC in lista
                                 where unC.NroPasaporte.ToLower().StartsWith(dato.ToLower())
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
                return View(new List<Clientes>());
            }
        }

        //Consulta
        [HttpGet]
        public ActionResult BuscarClientes(string NroPasaporte)
        {
            try
            {
                Clientes c = FabricaLogica.GetLogicaClientes().BuscarClientesActivos(NroPasaporte);
                if (c != null)
                    return View(c);
                else
                    throw new Exception("No existe el Cliente.");

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        //Alta
        [HttpGet]
        public ActionResult AltaCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AltaCliente(Clientes unC)
        {
            try
            {
                unC.Validar();

                FabricaLogica.GetLogicaClientes().AltaClientes(unC);

                return RedirectToAction("ListadoClientes", "Cliente");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        //Baja
        [HttpGet]
        public ActionResult BajaCliente(string NroPasaporte)
        {
            try
            {
                Clientes c = FabricaLogica.GetLogicaClientes().BuscarClientesActivos(NroPasaporte);
                if (c != null)
                    return View(c);
                else
                    throw new Exception("No existe la Cliente");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public ActionResult BajaCliente(Clientes unC)
        {
            try
            {
                FabricaLogica.GetLogicaClientes().BajaClientes(unC);
                return RedirectToAction("ListadoClientes", "Cliente");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        //Modificar
        [HttpGet]
        public ActionResult ModificarCliente(string NroPasaporte)
        {
            try
            {
                Clientes c = FabricaLogica.GetLogicaClientes().BuscarClientesActivos(NroPasaporte);
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
        public ActionResult ModificarCliente(Clientes unC)
        {
            try
            {
                unC.Validar();

                FabricaLogica.GetLogicaClientes().ModificarClientes(unC);
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