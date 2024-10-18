using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EntidadesCompartidas
{
    public class Empleados
    {
        [Required]
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Pass { get; set; }
        public string Cargo { get; set; }


        public Empleados(string pUsu, string pNombre,string pPass,string pCargo)
        {
            Usuario = pUsu;
            Nombre = pNombre;
            Pass = pPass;
            Cargo = pCargo;
        }

        public void Validar()
        {
            if (this.Usuario.Trim().Length < 4 || this.Usuario.Trim().Length > 20)
                throw new Exception("El nombre de usuario debe tener entre 4 y 20 caracteres.");
            else if (this.Nombre.Trim().Length < 3 || this.Nombre.Trim().Length > 20)
                throw new Exception("El nombre debe contener entre 3 y 20 caracteres.");
            else if (this.Pass.Trim().Length < 6 || this.Pass.Trim().Length > 20)
                throw new Exception("La contraseña debe contener entre 6 y 20 caracteres.");
            else if (this.Cargo.Trim().ToLower() != "gerente" || this.Cargo.Trim().ToLower() != "vendedor" || this.Cargo.Trim().ToLower() != "admin")
                throw new Exception("El cargo solo puede ser gerente, vendedor o admin.");
        }

        public Empleados() { }
    }
}
