using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Models.ViewModel
{
    public class User
    {
        public string Id { get; set; } 
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
