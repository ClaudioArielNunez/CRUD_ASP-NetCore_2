using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CRUD_2.Models
{
    public class EmpleadoViewModel
    {               
        public int Id { get; set; }        
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [DisplayName("Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        [DisplayName("E-mail")]
        public string Email { get; set; }
        public double Salario { get; set; }
        [DisplayName("Nombre completo")]
        public string NombreCompleto
        {
            get { return Nombre + " " + Apellido; }
        }
    }
}
