using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;
using Persistencias;

namespace Logica
{
    internal class LogicaAeropuertos : ILAeropuertos
    {
        private static LogicaAeropuertos _instancia = null;
        public LogicaAeropuertos() { }
        public static LogicaAeropuertos GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new LogicaAeropuertos();
            }
            return _instancia;
        }
        public Aeropuertos BuscarAeropuertoActivo(string cod)
        {
            return FabricaPersistencia.GetPersistenciaAeropuerto().BuscarActivo(cod);
        }
        public void AltaAeropuertos(Aeropuertos unA)
        {
            FabricaPersistencia.GetPersistenciaAeropuerto().AltaAeropuertos(unA);
        }
        public void BajaAeropuertos(Aeropuertos unA)
        {
            FabricaPersistencia.GetPersistenciaAeropuerto().BajaAeropuertos(unA);
        }
        public void ModificarAeropuertos(Aeropuertos unA)
        {
            FabricaPersistencia.GetPersistenciaAeropuerto().ModificarAeropuertos(unA);
        }
        public List<Aeropuertos> ListadoAeropuertos()
        {
            return FabricaPersistencia.GetPersistenciaAeropuerto().ListadoAeropuertos();
        }
    }
}
