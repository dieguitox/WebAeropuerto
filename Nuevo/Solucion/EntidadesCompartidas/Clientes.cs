using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace EntidadesCompartidas
{
    public class Clientes
    {
        [Key]
        [Required]
        [Display(Name = "Usuario")]
        public string NroPasaporte { get; set; }
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Pass { get; set; }
        public long NroTarjeta { get; set; }

        public Clientes(string pPasaporte ,string pNombre,string pPass, long pTarjeta)
        {
            NroPasaporte = pPasaporte;
            Nombre = pNombre;
            Pass = pPass;
            NroTarjeta = pTarjeta;
        }
        
        public void Validar()
        {
            if (this.NroPasaporte.Trim().Length != 7)
                throw new Exception("El pasaporte debe contener 7 caracteres y una letra al principio.");
            else if (this.Nombre.Trim().Length < 3 || this.Nombre.Trim().Length > 20)
                throw new Exception("El nombre debe contener entre 3 y 20 caracteres.");
            else if (this.Pass.Trim().Length < 6 || this.Pass.Trim().Length > 20)
                throw new Exception("La contraseña debe contener entre 6 y 20 caracteres.");
            else if (this.NroTarjeta.ToString().Length != 15)
                throw new Exception("El numero de tarjeta debe tener 15 caracteres.");
        }
        
        public Clientes() { }

    }
}
