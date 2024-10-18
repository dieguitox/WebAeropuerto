using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;
using Persistencias;

namespace Logica
{
    internal class LogicaVuelos : ILVuelos
    {
        private static LogicaVuelos instancia = null;
        private LogicaVuelos() { }
        public static LogicaVuelos GetInstancia()
        {
            if (instancia == null)
                instancia = new LogicaVuelos();
            return instancia;
        }
        public void AltaVuelos(Vuelos unV)
        {
            FabricaPersistencia.GetPersistenciaVuelo().AltaVuelos(unV);
        }
        public Vuelos BuscarVuelo(string cod)
        {
            return FabricaPersistencia.GetPersistenciaVuelo().BuscarVuelo(cod);
        }

        public List<Vuelos> ListadoVuelos()
        {
            return FabricaPersistencia.GetPersistenciaVuelo().ListadoVuelos();
        }
    }
}
