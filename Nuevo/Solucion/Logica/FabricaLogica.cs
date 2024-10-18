using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class FabricaLogica
    {
        public static ILAeropuertos GetLogicaAeropuertos()
        {
            return (LogicaAeropuertos.GetInstancia());
        }

        public static ILCiudades GetLogicaCiudades()
        {
            return LogicaCiudades.GetInstancia();
        }

        public static ILClientes GetLogicaClientes()
        {
            return LogicaClientes.GetInstancia();
        }

        public static ILEmpleados GetLogicaEmpleados()
        {
            return LogicaEmpleados.GetInstancia();
        }

        public static ILVenta GetLogicaVenta()
        {
            return LogicaVenta.GetInstancia();
        }

        public static ILVuelos GetLogicaVuelo()
        {
            return LogicaVuelos.GetInstancia();
        }

        public static ILAdquieren GetLogicaAdquieren()
        {
            return LogicaAdquieren.GetInstancia();
        }

    }
}
