using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Models.Model
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [MaxLength(1)]
        public string Sexo { get; set; }

    }
}
