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
    public interface IPCliente
    {
        Clientes BuscarClienteActivo(string pasaporte);
        void AltaClientes(Clientes unC);
        void BajaClientes(Clientes unC);
        void ModificarClientes(Clientes unC);
        List<Clientes> ListarClientes();
    }
}
