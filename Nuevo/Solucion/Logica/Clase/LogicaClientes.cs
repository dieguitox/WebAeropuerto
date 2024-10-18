using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;
using Persistencias;

namespace Logica
{
    class LogicaClientes : ILClientes
    {
        private static LogicaClientes instancia = null;
        private LogicaClientes() { }
        public static LogicaClientes GetInstancia()
        {
            if (instancia == null)
                instancia = new LogicaClientes();
            return instancia;
        }
        public Clientes BuscarClientesActivos(string pasaporte)
        {
            return FabricaPersistencia.GetPersistenciaCliente().BuscarClienteActivo(pasaporte);
        }
        public void AltaClientes(Clientes unC)
        {
             FabricaPersistencia.GetPersistenciaCliente().AltaClientes(unC);
        }
        public void BajaClientes(Clientes unC)
        {
             FabricaPersistencia.GetPersistenciaCliente().BajaClientes(unC);
        }
        public void ModificarClientes(Clientes unC)
        {
             FabricaPersistencia.GetPersistenciaCliente().ModificarClientes(unC);
        }
        public List<Clientes> ListadoClientes()
        {
            return FabricaPersistencia.GetPersistenciaCliente().ListarClientes();
        }
    }
}
