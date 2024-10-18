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
    public interface IPAdquieren
    {
        void AltaAdquieren(Adquieren adq);
        List<Adquieren> ListadoAdquieren();
    }
}
