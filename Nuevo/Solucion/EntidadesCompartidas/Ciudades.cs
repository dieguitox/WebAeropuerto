using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EntidadesCompartidas
{
    public class Ciudades
    {
        [Display(Name = "Codigo")]
        public string CodigoC { get ;set ; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }

        public Ciudades(string pCodigoC, string pPais, string pCiudad)
        {
            CodigoC = pCodigoC;
            Pais = pPais;
            Ciudad = pCiudad;
        }

        public void Validar()
        {
            if (this.CodigoC.Trim().Length != 6)
                throw new Exception("El codigo de la ciudad debe tener obligatoriamente 6 caracteres.");
            else if (this.Ciudad.Trim().Length < 5 || this.Ciudad.Trim().Length > 30)
                throw new Exception("El nombre de la ciudad debe tener entre 5 y 30 caracteres.");
            else if (this.Pais.Trim().Length < 5 || this.Pais.Trim().Length > 30)
                throw new Exception("El nombre del Pais debe tener entre 5 y 30 caracteres.");
        }

        public Ciudades() { }
    }
}
