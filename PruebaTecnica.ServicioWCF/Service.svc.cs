using PruebaTecnica.ServicioWCF.DAL.Entities;
using PruebaTecnica.ServicioWCF.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnica.ServicioWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service.svc o Service.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service : IService
    {
        private readonly PruebaDbContext _context = new PruebaDbContext();

        public async Task<UsuarioDTO> Adicionar(UsuarioDTO usuario)
        {
            Usuario _usuario = new Usuario()
            {
                Nombre = usuario.Nombre,
                Sexo = usuario.Sexo,
                FechaNacimiento = usuario.FechaNacimiento
            };
            _context.Usuarios.Add(_usuario);
            await _context.SaveChangesAsync();
            usuario.UsuarioId = usuario.UsuarioId;
            return usuario;
        }

        public async Task<UsuarioDTO> Eliminar(int usuarioId)
        {
            Usuario user = await _context.Usuarios.FirstAsync(x => x.UsuarioId == usuarioId);
            UsuarioDTO usuarioDTO = new UsuarioDTO();
            if (user != null)
            {
                _context.Usuarios.Remove(user);
                await _context.SaveChangesAsync();
                usuarioDTO.Nombre = user.Nombre;
                usuarioDTO.UsuarioId=user.UsuarioId;
                usuarioDTO.Sexo = user.Sexo;
                usuarioDTO.FechaNacimiento = user.FechaNacimiento;
            }
            return usuarioDTO;
        }

        public async Task<bool> ExisteUsuario(UsuarioDTO usuario)
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

        public async Task<UsuarioDTO> GetUsuario(int usuarioId)
        {
            UsuarioDTO _user = await _context.Usuarios.Select(
                                u => new UsuarioDTO{
                                    Nombre = u.Nombre,
                                    Sexo = u.Sexo,
                                    FechaNacimiento = u.FechaNacimiento,
                                    UsuarioId = u.UsuarioId
                                }).FirstAsync(x => x.UsuarioId == usuarioId);
            return _user;
        }

        public async Task<List<UsuarioDTO>> GetUsuarios()
        {
            List<UsuarioDTO> usuarios = await _context.Usuarios.Select(
                                u => new UsuarioDTO
                                {
                                    Nombre = u.Nombre,
                                    Sexo = u.Sexo,
                                    FechaNacimiento = u.FechaNacimiento,
                                    UsuarioId = u.UsuarioId
                                }).ToListAsync();
            return usuarios;
        }

        public async Task<UsuarioDTO> Modificar(int usuarioId, UsuarioDTO usuario)
        {
            Usuario _user = await _context.Usuarios.FirstAsync(x => x.UsuarioId == usuarioId);
            _user.Sexo = usuario.Sexo;
            _user.Nombre = usuario.Nombre;
            _user.FechaNacimiento = usuario.FechaNacimiento;
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}
