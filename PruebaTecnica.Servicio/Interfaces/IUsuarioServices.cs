using PruebaTecnica.Models.Model;

namespace PruebaTecnica.Servicio.Interfaces
{
    public interface IUsuarioServices
    {
        Task<List<Usuario>> Execute(string typeOperation, Usuario usuario = null);
        Task<Usuario> CreteUsuario(Usuario usuario);
        Task<List<Usuario>> GetUsuarios();
        Task<Usuario> GetUsuario(int usuarioId);
        Task<Usuario> UpdateUsuario(int usuarioId, Usuario usuario);
        Task<Usuario> DeleteUsuario(int usuarioId);
        Task<bool> ExisteUsuario(Usuario usuario);
    }
}
