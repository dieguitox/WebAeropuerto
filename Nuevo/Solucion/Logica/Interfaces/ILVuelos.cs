using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;
using Persistencias;

namespace Logica
{
    public interface ILVuelos
    {
        void AltaVuelos(Vuelos unV);
        Vuelos BuscarVuelo(string cod);
        List<Vuelos> ListadoVuelos();
    }
}
