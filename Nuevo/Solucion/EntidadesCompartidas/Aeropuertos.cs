using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EntidadesCompartidas
{
    public class Aeropuertos
    {
        [Display(Name = "Aeropuerto")]
        public string CodigoA { get;  set; }
        [Display(Name = "Nombre")]
        public string NombreA { get; set; }
        public string Direccion { get; set; }
        [Display(Name = "Impuesto de Partida")]
        public double ImpuestoPar { get; set; }
        [Display(Name = "Impuesto de Llegada")]
        public double ImpuestoLle { get; set; }
        public Ciudades Ciudad { get; set; }

            public Aeropuertos(string pCodA, string pNombre, string pDire, double pImpuestoP, double pImpuestoL, Ciudades pCiu)
        {
            CodigoA = pCodA;
            NombreA = pNombre;
            Direccion = pDire;
            ImpuestoPar = pImpuestoP;
            ImpuestoLle = pImpuestoL;
            Ciudad = pCiu;
        }

        public void Validar()
        {
            if (this.CodigoA.Trim().Length != 3)
                throw new Exception("El codigo del Aeropuerto debe contener 3 caracteres.");
            else if (this.Direccion.Trim().Length < 8 || this.Direccion.Trim().Length > 50)
                throw new Exception("Debe ingresar una direccion entre 8 y 50 caracteres.");
            else if (this.NombreA.Trim().Length < 5 || this.NombreA.Trim().Length > 20)
                throw new Exception("El nombre del Aeropuerto debe tener entre 5 y 20 caracteres.");
            else if (this.ImpuestoPar < 0)
                throw new Exception("El impuesto de arribo debe ser mayor o igual que 0.");
            else if (this.ImpuestoLle < 0)
                throw new Exception("El impuesto de arribo debe ser mayor o igual que 0.");
            else if (this.Ciudad.CodigoC == null)
                throw new Exception("Debe seleccionar una ciudad.");
        }

        public Aeropuertos() { }
    }
}
