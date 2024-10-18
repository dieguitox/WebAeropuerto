using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;

namespace Logica
{
    public interface ILCiudades
    {
        Ciudades BuscarCiudadesActivas(string cod);
        void AltaCiudades(Ciudades unaC);
        void BajaCiudades(Ciudades unaC);
        void ModificarCiudades(Ciudades unaC);
        List<Ciudades> ListarCiudad();
    }
}
