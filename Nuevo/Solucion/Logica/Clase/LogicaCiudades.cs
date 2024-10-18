using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;
using Persistencias;

namespace Logica
{
    internal class LogicaCiudades: ILCiudades
    {
        private static LogicaCiudades instancia = null;
        private LogicaCiudades() { }
        public static LogicaCiudades GetInstancia()
        {
            if (instancia == null)
                instancia = new LogicaCiudades();
            return instancia;
        }
        public Ciudades BuscarCiudadesActivas(string cod)
        {
            return FabricaPersistencia.GetPersistenciaCiudades().BuscarActivas(cod);
        }
        public void AltaCiudades(Ciudades unaC)
        {
            FabricaPersistencia.GetPersistenciaCiudades().AltaCiudades(unaC);
        }
        public void BajaCiudades(Ciudades unaC)
        {
            FabricaPersistencia.GetPersistenciaCiudades().BajaCiudades(unaC);
        }
        public void ModificarCiudades(Ciudades unaC)
        {
            FabricaPersistencia.GetPersistenciaCiudades().ModificarCiudades(unaC);
        }
        public List<Ciudades> ListarCiudad()
        {
            return FabricaPersistencia.GetPersistenciaCiudades().ListarCiudad();
        }
    }
}
