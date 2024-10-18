using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using EntidadesCompartidas;
using Persistencias;

namespace Persistencias
{
    public class FabricaPersistencia
    {
        public static IPAeropuerto GetPersistenciaAeropuerto()
        {
            return (PersistenciaAeropuerto.GetInstancia());
        }
        public static IPCliente GetPersistenciaCliente()
        {
            return (PersistenciaCliente.GetInstancia());
        }
        public static IPCiudades GetPersistenciaCiudades()
        {
            return (PersistenciaCiudades.GetInstancia());
        }
        public static IPEmpleados GetPersistenciaEmpleado()
        {
            return (PersistenciaEmpleado.GetInstancia());
        }
        public static IPVenta GetPersistenciaVenta()
        {
            return (PersistenciaVenta.GetInstancia());
        }
        public static IPVuelo GetPersistenciaVuelo()
        {
            return (PersistenciaVuelo.GetInstancia());
        }
        public static IPAdquieren GetPersistenciaAdquieren()
        {
            return (PersistenciaAdquieren.GetInstancia());
        }
    }
}
