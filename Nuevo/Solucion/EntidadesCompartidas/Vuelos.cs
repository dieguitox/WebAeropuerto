using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EntidadesCompartidas
{
    public class Vuelos
    {
        [DisplayName("Viaje")]
        public string CodigoV { get; set; }
        [Display(Name = "Fecha y Hora de Partida")]
        public DateTime FechaD { get; set; }
        [Display(Name = "Fecha y Hora de Llegada")]
        public DateTime FechaA { get; set; }
        public double Precio { get; set; }

        [Display(Name = "Cantidad de Asientos")]
        public int CantAsientos { get; set; }

        public Aeropuertos CodA { get; set; }
        
        public Aeropuertos CodB { get; set; }

        public Vuelos(string pCod, DateTime pFechaS, DateTime pFechaL, double pPrecio, int pAsientos, Aeropuertos pCodA, Aeropuertos pCodB)
        {
            CodigoV = pCod;
            FechaD = pFechaS;
            FechaA = pFechaL;
            Precio = pPrecio;
            CantAsientos = pAsientos;
            CodA = pCodA;
            CodB = pCodB;
        }

        public void Validar()
        {
            if (this.CodigoV.Trim().Length != 15)
                throw new Exception("Codigo de vuelo incorrecto.");
            else if (this.FechaD < this.FechaA)
                throw new Exception("Fecha de salida incorrecta , debe ser menor que la llegada.");
            else if (this.FechaA >= DateTime.Now)
                throw new Exception("Fecha de llegada incorrecta");
            else if(this.CantAsientos < 0 || this.CantAsientos > 301)
                throw new Exception("La cantidad de asientos debe ser mayor que 0 y menor que 300");
            else if (this.Precio > 0) 
                throw new Exception("El precio no puede ser menor que cero");
            else if (this.CodA == null)
                throw new Exception("Debe ingresar un Aeropuerto de partida");
            else if (this.CodB == null)
                throw new Exception("Debe ingresar un Aeropuerto de llegada");
        }

        public Vuelos() { }
    }
}
