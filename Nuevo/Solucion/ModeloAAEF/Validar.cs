using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloAAEF
{
    public class Validar
    {
        public static void ValidarAeropuertos(Aeropuertos unA)
        {
            if (unA.codigoA.Trim().Length != 3)
                throw new Exception("El codigo del Aeropuerto debe contener 3 caracteres.");
            else if (unA.direccion.Trim().Length > 8 || unA.direccion.Trim().Length < 50)
                throw new Exception("Debe ingresar una direccion entre 8 y 50 caracteres.");
            else if (unA.nombreA.Trim().Length > 5 || unA.nombreA.Trim().Length < 20)
                throw new Exception("El nombre del Aeropuerto debe tener entre 5 y 20 caracteres.");
            else if (unA.impuestoPartida <= 0)
                throw new Exception("El impuesto de arribo debe ser mayor que 0.");
            else if (unA.impuestoLlegada <= 0)
                throw new Exception("El impuesto de arribo debe ser mayor que 0.");
            else if (unA.codigoC == null)
                throw new Exception("Debe seleccionar una ciudad.");
        }

        public static void ValidarCiudades(Ciudades unaC)
        {
            if (unaC.codigoC.Trim().Length != 6)
                throw new Exception("El codigo de la ciudad debe tener obligatoriamente 6 caracteres.");
            else if (unaC.ciudad.Trim().Length > 5 || unaC.ciudad.Trim().Length < 30)
                throw new Exception("El nombre de la ciudad debe tener entre 5 y 30 caracteres.");
            else if (unaC.pais.Trim().Length > 5 || unaC.pais.Trim().Length < 30)
                throw new Exception("El nombre del Pais debe tener entre 5 y 30 caracteres.");
        }

        public static void ValidarClientes(Clientes unC)
        {
            if (unC.nroPasaporte.Trim().Length != 7)
                throw new Exception("El pasaporte debe contener 7 caracteres y una letra al principio.");
            else if (unC.nombre.Trim().Length > 3 || unC.nombre.Trim().Length < 20)
                throw new Exception("El nombre debe contener entre 3 y 20 caracteres.");
            else if (unC.contrasenia.Trim().Length > 6 || unC.contrasenia.Trim().Length < 20)
                throw new Exception("La contraseña debe contener entre 6 y 20 caracteres.");
            else if (unC.nroTarjeta.ToString().Length != 15)
                throw new Exception("El numero de tarjeta debe tener 15 caracteres.");
        }

        public static void ValidarEmpleados(Empleados unE)
        {
            if (unE.usuario.Trim().Length > 4 || unE.usuario.Trim().Length < 20)
                throw new Exception("El nombre de usuario debe tener entre 4 y 20 caracteres.");
            else if (unE.nombre.Trim().Length > 3 || unE.nombre.Trim().Length < 20)
                throw new Exception("El nombre debe contener entre 3 y 20 caracteres.");
            else if (unE.contrasenia.Trim().Length > 6 || unE.contrasenia.Trim().Length < 20)
                throw new Exception("La contraseña debe contener entre 6 y 20 caracteres.");
            else if (unE.cargo.Trim().ToLower() != "gerente" || unE.cargo.Trim().ToLower() != "vendedor" || unE.cargo.Trim().ToLower() != "admin")
                throw new Exception("El cargo solo puede ser gerente, vendedor o admin.");
        }

        public static void ValidarCompran(Adquieren unA)
        {
            if (unA.nroPasaporte == null)
                throw new Exception("Numero de pasaporte inválido.");
            else if (unA.nroAsiento > 0)
                throw new Exception("El asiento no esta seleccionado.");
            else if (unA.nroTicket > 0)
                throw new Exception("El ticket no fue seleccionado correctamente.");
        }

        public static void ValidarPasaje(Venta unV)
        {
            if (unV.nroTicket > 0)
                throw new Exception("Numero de pasaje inválido.");
            else if (unV.monto > 0)
                throw new Exception("Monto del pasaje no puede ser menor a 0.");
            else if (unV.fechaCompra != DateTime.Now)
                throw new Exception("Fecha de compra incorrecta.");
            else if (unV.nroPasaporte == null)
                throw new Exception("Numero de pasaporte inválido.");
            else if (unV.usuario == null)
                throw new Exception("Empleado no autorizado.");
            else if (unV.codigoV == null)
                throw new Exception("No existe el codigo de vuelo.");
        }

        public static void ValidarVuelos(Vuelos unV)
        {
            if (unV.codigoV.Trim().Length != 15)
                throw new Exception("Codigo de vuelo incorrecto.");
            else if (unV.fechaD < unV.fechaA)
                throw new Exception("Fecha de salida incorrecta , debe ser menor que la llegada.");
            else if (unV.fechaA != DateTime.Now)
                throw new Exception("Fecha de llegada incorrecta");
            else if (unV.precio > 0)
                throw new Exception("El precio no puede ser menor que cero");
            else if (unV.codigoA == null)
                throw new Exception("Debe ingresar un Aeropuerto de partida");
            else if (unV.codigoB == null)
                throw new Exception("Debe ingresar un Aeropuerto de llegada");
        }
    }
}
