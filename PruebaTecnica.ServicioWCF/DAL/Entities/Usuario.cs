using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.ServicioWCF.DAL.Entities
{
    public partial class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [StringLength(100)]
        public string Nombre { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime FechaNacimiento { get; set; }

        [StringLength(1)]
        public string Sexo { get; set; }
    }
}
