using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencias
{
    internal class Conexion
    {
        private static string cnn = "Data Source=(local); Initial Catalog = AeropuertosAmericanos; Integrated Security = true";

        internal static string Cnn
        {
            get { return cnn; }
        }
    }
}
