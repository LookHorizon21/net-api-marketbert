using ApiMarketBert.Models.Dtos;
using ApiMarketBert.Models;

namespace ApiMarketBert.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        ICollection<UsuarioMd> GetUsuarios();
        UsuarioMd GetUsuario(int usuarioId);
        bool IsUniqueUser(string usuario);
        Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);
        Task<UsuarioMd> Registro(UsuarioRegistroDto usuarioRegistroDto);
    }
}
