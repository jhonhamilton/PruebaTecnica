using Microsoft.IdentityModel.Tokens;
using PruebaTecnica.Models.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PruebaTecnica.Models.Helpers
{
    public static class JwtHelpers
    {
        private static string Get_Data_Convert(string dato)
        {
            var _dato = Encoding.UTF8.GetBytes(dato);
            return Convert.ToBase64String(_dato);
        }
        public static IEnumerable<Claim> GetClaims(this UsuarioTokens userAccounts, Guid Id)
        {
            List<Claim> claims = new()
            {
                new Claim("Id", userAccounts.UsuarioId.ToString()),
                new Claim(ClaimTypes.Name, userAccounts.Nombre.ToString()),                
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
            if (userAccounts.TypeOfNegotiation == "BussinessHamilton")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Business Hamilton"));
                claims.Add(new Claim(ClaimTypes.Hash, Get_Data_Convert(userAccounts.TypeOfNegotiation)));
            }
            return claims;
        }

        public static IEnumerable<Claim> GetClaims(this UsuarioTokens userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userAccounts, Id);
        }

        public static UsuarioTokens GenTokenKey(UsuarioTokens model, JwtSettings jwtSettings)
        {
            try
            {
                var userToken = new UsuarioTokens();
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }

                // Generar SECRET KEY
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);

                Guid Id;

                // Expira en un día
                DateTime expireTime = DateTime.UtcNow.AddDays(1);

                // Validar TOKEN
                userToken.Validity = expireTime.TimeOfDay;

                // Generar JWT                
                var jwToken = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));

                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);
                userToken.Nombre = model.Nombre;
                userToken.TypeOfNegotiation = model.TypeOfNegotiation;
                userToken.UsuarioId = model.UsuarioId;
                userToken.GuidId = Id;
                userToken.ExpiredTime = expireTime;
                userToken.EmailId = model.EmailId;
                return userToken;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Generating the JWT", ex);
            }
        }
    }
}