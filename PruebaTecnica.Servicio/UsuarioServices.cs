using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PruebaTecnica.DataAcces;
using PruebaTecnica.Models.Model;
using PruebaTecnica.Servicio.Interfaces;
using System.Data;

namespace PruebaTecnica.Servicio
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly PruebaDbContext _context;
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        public UsuarioServices(PruebaDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<List<Usuario>> Execute(string typeOperation, Usuario usuario = null)
        {
            SqlConnection connection = new(_configuration.GetConnectionString("PruebaTecnica"));
            SqlCommand command = new("SP_CrudUser", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            DataTable dtResult = new DataTable();
            List<Usuario> _users = new();
            try
            {
                command.Parameters.Add(new SqlParameter("TypeOperation", typeOperation));
                if(usuario == null)
                {
                    usuario = new Usuario()
                    {
                        UsuarioId = 0,
                        Nombre = "",
                        FechaNacimiento = DateTime.Now,
                        Sexo = ""
                    };
                }
                if(usuario  != null)
                {
                    command.Parameters.Add(new SqlParameter("idUsuario", usuario.UsuarioId));
                    command.Parameters.Add(new SqlParameter("Nombre", usuario.Nombre));
                    command.Parameters.Add(new SqlParameter("Fecha", usuario.FechaNacimiento.ToString("yyy-MM-dd")));
                    command.Parameters.Add(new SqlParameter("Sexo", usuario.Sexo));
                }
                await connection.OpenAsync();
                dtResult.Load(command.ExecuteReader());
                await connection.CloseAsync();
            }
            catch (Exception ex)
            {
                await connection.CloseAsync();
            }
            if(dtResult != null)
            {
                if(dtResult.Rows.Count > 0)
                {
                    _users = (from d in dtResult.AsEnumerable()
                              select new Usuario
                              {
                                  UsuarioId = d.Field<int>("UsuarioId"),
                                  Nombre = d.Field<string>("Nombre"),
                                  FechaNacimiento = d.Field<DateTime>("FechaNacimiento"),
                                  Sexo = d.Field<string>("Sexo"),
                              }).ToList();
                }
            }
            return _users;
        }
        public async Task<List<Usuario>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }
        public async Task<Usuario> GetUsuario(int usuarioId)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.UsuarioId == usuarioId);
        }
        public async Task<Usuario> CreteUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
        public async Task<Usuario> UpdateUsuario(int usuarioId, Usuario usuario)
        {
            Usuario _user = await _context.Usuarios.FirstAsync(x => x.UsuarioId == usuarioId);
            _user.Nombre = usuario.Nombre;
            _user.FechaNacimiento = usuario.FechaNacimiento;
            _user.Sexo = usuario.Sexo;
            await _context.SaveChangesAsync();
            return _user;
        }
        public async Task<Usuario> DeleteUsuario(int usuarioId)
        {
            Usuario _user = await _context.Usuarios.FirstAsync(x => x.UsuarioId == usuarioId);
            _context.Usuarios.Remove(_user);
            await _context.SaveChangesAsync();
            return _user;
        }
        public async Task<bool> ExisteUsuario(Usuario usuario)
        {
            bool _existe = false;
            try
            {
                Usuario user = await _context.Usuarios.FindAsync(usuario.UsuarioId);
                _existe = (user != null);
            }
            catch (Exception ex)
            {

            }
            return _existe;
        }
    }
}
