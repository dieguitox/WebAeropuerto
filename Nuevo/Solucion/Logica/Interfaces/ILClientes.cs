using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;

namespace Logica
{
    public interface ILClientes
    {
        Clientes BuscarClientesActivos(string pasaporte);
        void AltaClientes(Clientes unC);
        void BajaClientes(Clientes unC);
        void ModificarClientes(Clientes unC);
        List<Clientes> ListadoClientes();
    }
}
