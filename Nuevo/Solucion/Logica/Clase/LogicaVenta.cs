using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;
using Persistencias;

namespace Logica
{
     internal class LogicaVenta : ILVenta
    {
        private static LogicaVenta instancia = null;
        private LogicaVenta() { }
        public static LogicaVenta GetInstancia()
        {
            if (instancia == null)
                instancia = new LogicaVenta();
            return instancia;
        }

        public int AltaVenta(Ventas unV)
        {
            if (unV.Monto == 0)
            {
                double llegada = unV.Vue.CodB.ImpuestoLle;
                double salida = unV.Vue.CodA.ImpuestoPar;
                double precio = unV.Vue.Precio;
                double monto = llegada + salida + precio;

                unV.Monto = monto;
            }

            return FabricaPersistencia.GetPersistenciaVenta().AltaVenta(unV);
        }

        public List<Ventas> ListadoVenta()
        {
            return FabricaPersistencia.GetPersistenciaVenta().ListadoVenta();
        }

        public Ventas BuscarVenta(int factura)
        {
            return FabricaPersistencia.GetPersistenciaVenta().BuscarVenta(factura);
        }
    }
}
