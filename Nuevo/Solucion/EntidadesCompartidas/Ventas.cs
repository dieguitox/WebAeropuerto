using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EntidadesCompartidas
{
    public class Ventas
    {
        [Display(Name = "Número de Ticket")]
        public int NroTicket { get; set; }
        [Display(Name = "Precio final")]
        public double Monto { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Compra")]
        public DateTime FechaCompra { get; set; }
        public  Clientes Cli { get; set; }
        public  Empleados Emp { get; set; }
        public  Vuelos Vue { get; set; }

        public Ventas(int pTicket,double pMonto,DateTime pFecha,Clientes pCliente,Empleados pEmp, Vuelos pVuelo)
        {
            NroTicket = pTicket;
            Monto = pMonto;
            FechaCompra = pFecha;
            Cli = pCliente;
            Emp = pEmp;
            Vue = pVuelo;
        }

        public void Validar()
        {
            if (this.NroTicket > 0)
                throw new Exception("Numero de pasaje inválido.");
            else if (this.Monto < 0)
                throw new Exception("Monto del pasaje no puede ser menor a 0.");
            else if (this.Cli == null)
                throw new Exception("Numero de pasaporte inválido.");
            else if (this.Emp == null)
                throw new Exception("Empleado no autorizado.");
            else if (this.Vue == null)
                throw new Exception("No existe el codigo de vuelo.");
        }

        public Ventas() { }
    }
}
