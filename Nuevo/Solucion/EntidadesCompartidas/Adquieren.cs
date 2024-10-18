using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EntidadesCompartidas
{
    public class Adquieren
    {
        public Clientes NroPasaporte { get;  set ; } 
        public Ventas NroTicket { get; set; }
        [Display(Name = "Numero de Asiento")]
        public int NroAsiento { get; set; }

        public Adquieren(Clientes pPasaporte, Ventas pNroTicket , int pAsiento)
        {
            NroPasaporte = pPasaporte;
            NroTicket = pNroTicket;
            NroAsiento = pAsiento;
        }

        public void Validar()
        {
            if (this.NroPasaporte == null)
                throw new Exception("Numero de pasaporte inválido.");
            else if (this.NroAsiento < 0)
                throw new Exception("El asiento no esta seleccionado.");
            else if (this.NroTicket == null || this.NroTicket.NroTicket <= 0)
                throw new Exception("Número de ticket inválido.");
        }

        public Adquieren() { }
    }
}
