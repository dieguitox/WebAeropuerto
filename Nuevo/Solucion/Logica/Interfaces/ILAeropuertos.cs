using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;
using Persistencias;

namespace Logica
{
    public interface ILAeropuertos
    {
        Aeropuertos BuscarAeropuertoActivo(string cod);
        void AltaAeropuertos(Aeropuertos unA);
        void BajaAeropuertos(Aeropuertos unA);
        void ModificarAeropuertos(Aeropuertos unA);
        List<Aeropuertos> ListadoAeropuertos();
    }
}
