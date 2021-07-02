using API.Seguros.Proseg.Domain.Entidades;

namespace API.Seguros.Proseg.Domain.Interfaces.Repository
{
    public interface IUsuarioMultiCalculoRepository : IRepository<UsuarioMultiCalculo>
    {
        UsuarioMultiCalculo GetUsuarioOperador(int idUsuarioMultiCalc);
    }
}
