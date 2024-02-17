using PruebaTecnica.ServicioWCF.DTOs;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PruebaTecnica.ServicioWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService" en el código y en el archivo de configuración a la vez.
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public interface IService
    {
        [OperationContract]
        Task<UsuarioDTO> Adicionar(UsuarioDTO usuario);
        Task<List<UsuarioDTO>> GetUsuarios();
        Task<UsuarioDTO> GetUsuario(int usuarioId);
        Task<UsuarioDTO> Modificar(int usuarioId, UsuarioDTO usuario);
        Task<UsuarioDTO> Eliminar(int usuarioId);
        Task<bool> ExisteUsuario(UsuarioDTO usuario);
    }
}
