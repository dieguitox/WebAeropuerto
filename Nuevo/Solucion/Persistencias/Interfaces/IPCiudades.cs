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
    public interface IPCiudades
    {
        Ciudades BuscarActivas(string cod);
        void AltaCiudades(Ciudades unaC);
        void BajaCiudades(Ciudades unaC);
        void ModificarCiudades(Ciudades unaC);
        List<Ciudades> ListarCiudad();
    }
}