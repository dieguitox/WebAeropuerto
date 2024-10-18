using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;
using Persistencias;

namespace Logica
{
    public interface ILVenta
    {
        int AltaVenta(Ventas unaV);
        Ventas BuscarVenta(int factura);

        List<Ventas> ListadoVenta();
    }
}
