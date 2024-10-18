using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;
using Persistencias;

namespace Logica
{
    internal class LogicaAdquieren :ILAdquieren
    {
        private static LogicaAdquieren _instancia = null;
        public LogicaAdquieren() { }
        public static LogicaAdquieren GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new LogicaAdquieren();
            }
            return _instancia;
        }

        public void AltaAdquieren(Adquieren unA)
        {
            FabricaPersistencia.GetPersistenciaAdquieren().AltaAdquieren(unA);
        }

        public List<Adquieren> ListadoAdquieren()
        {
            return FabricaPersistencia.GetPersistenciaAdquieren().ListadoAdquieren();
        }
    }
}
