using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;

namespace Logica
{
    public interface ILEmpleados
    {
        Empleados Logueo(string usuario, string pass);

        Empleados BuscarE(string unE);
    }
}
