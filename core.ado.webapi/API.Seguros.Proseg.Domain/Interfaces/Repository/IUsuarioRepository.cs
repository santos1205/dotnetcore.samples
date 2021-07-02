using API.Seguros.Proseg.Domain.Entidades;

namespace API.Seguros.Proseg.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario GetUsuarioAutenticado(int usrClientID);
    }
}
