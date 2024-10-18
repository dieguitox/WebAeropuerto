using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;
using Persistencias;

namespace Logica
{
    internal class LogicaEmpleados : ILEmpleados
    {
        private static LogicaEmpleados instancia = null;
        private LogicaEmpleados() { }
        public static LogicaEmpleados GetInstancia()
        {
            if (instancia == null)
                instancia = new LogicaEmpleados();
            return instancia;
            
        }
        public Empleados Logueo(string usuario,string pass)
        {
            return FabricaPersistencia.GetPersistenciaEmpleado().Logueo(usuario, pass);
        }

        public Empleados BuscarE(string unE)
        {
            return FabricaPersistencia.GetPersistenciaEmpleado().BuscarE(unE);
        }

    }
}
