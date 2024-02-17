using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Models.ViewModel
{
    public class UsuarioTokens
    {
        public string UsuarioId { get; set; }
        public string Token { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string TypeOfNegotiation { get; set; }
        public TimeSpan Validity { get; set; }
        public string RefreshToken { get; set; }
        public string EmailId { get; set; }
        public Guid GuidId { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
