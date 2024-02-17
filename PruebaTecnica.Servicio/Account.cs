using Microsoft.EntityFrameworkCore;
using PruebaTecnica.DataAcces;
using PruebaTecnica.Models.Helpers;
using PruebaTecnica.Models.Model;
using PruebaTecnica.Models.ViewModel;
using PruebaTecnica.Servicio.Interfaces;

namespace PruebaTecnica.Servicio
{
    public class Account : IAccountLogin
    {
        private readonly PruebaDbContext _context;
        private readonly JwtSettings _jwtSettings;
        public Account(PruebaDbContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }
        public JwtSettings GetJwtSettings()
        {
            return _jwtSettings;
        }
        public async Task<User> GetUserAsync(string username, string password)
        {
            return await (from user in _context.UserLogin
                          where user.NombreUsuario == username && user.Contrasenia == password
                          select new User
                          {
                              Id = user.Id,
                              UserName = user.NombreUsuario,
                              Password = user.Contrasenia,
                              AccessToken = user.Token,
                          }).FirstOrDefaultAsync();            
        }
        public async Task<UserLogin> UpdateUserAsync(User user)
        {
            var _user = await _context.UserLogin.FirstAsync(f => f.Id == user.Id);
            _user.NombreUsuario = user.UserName;
            _user.Contrasenia = user.Password;
            _user.Token = user.AccessToken;
            //_user.RefreshToken = user.RefreshToken;
            await _context.SaveChangesAsync();
            return _user;
        }
        public async Task<UserLogin> GetUserFindByNameAsync(string username)
        {
            return await _context.UserLogin.FirstAsync(f => f.NombreUsuario == username);
        }
        public UsuarioTokens GetToken(User user)
        {
            var TOKEN = new UsuarioTokens();
            if (user != null)
            {
                TOKEN = JwtHelpers.GenTokenKey(new UsuarioTokens()
                {
                    Nombre = user.UserName,
                    UsuarioId = user.Id,
                    TypeOfNegotiation = "BussinessHamilton",
                    GuidId = Guid.NewGuid()
                }, _jwtSettings);
            }
            return TOKEN;
        }
        public async Task<bool> IsValid(string _userName, string _nit)
        {
            //var _user = await (from user in _context.Usuarios
            //                   where user.UserName == _userName && user.Nit == _nit
            //                   select user).FirstOrDefaultAsync();
            var _user = await (from user in _context.Usuarios
                               where user.Nombre == _userName
                               select user).FirstOrDefaultAsync();
            return (_user != null);
        }
    }
}
