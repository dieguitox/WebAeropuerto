using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;
using Persistencias;

namespace Logica
{
    public interface ILAdquieren
    {
        void AltaAdquieren(Adquieren unA);
        List<Adquieren> ListadoAdquieren();
    }
}
