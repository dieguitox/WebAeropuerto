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
    public interface IPAeropuerto
    {
        Aeropuertos BuscarActivo(string cod);
        void AltaAeropuertos(Aeropuertos unA);
        void BajaAeropuertos(Aeropuertos unA);
        void ModificarAeropuertos(Aeropuertos unA);
        List<Aeropuertos> ListadoAeropuertos();
    }
}
