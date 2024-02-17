using Microsoft.AspNetCore.Identity;

namespace PruebaTecnica.Models.Model
{
    public class UserLogin : IdentityUser
    {
        //public int UserLoginId { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        public string Token { get; set; }
    }
}
