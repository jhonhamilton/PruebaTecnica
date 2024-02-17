using PruebaTecnica.Models.Model;
using PruebaTecnica.Models.ViewModel;

namespace PruebaTecnica.Servicio.Interfaces
{
    public interface IAccountLogin
    {
        Task<User> GetUserAsync(string username, string password);
        Task<UserLogin> GetUserFindByNameAsync(string username);
        Task<UserLogin> UpdateUserAsync(User user);
        JwtSettings GetJwtSettings();
        UsuarioTokens GetToken(User user);
        Task<bool> IsValid(string _userName, string _password);
    }
}
