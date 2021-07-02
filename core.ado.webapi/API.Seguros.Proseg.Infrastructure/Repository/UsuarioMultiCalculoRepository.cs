using API.Seguros.Proseg.Domain.Entidades;
using API.Seguros.Proseg.Domain.Interfaces.Repository;
using API.Seguros.Proseg.Infrastructure.Data;
using System;
using System.Linq;

namespace API.Seguros.Proseg.Infrastructure.Repository
{
    public class UsuarioMultiCalculoRepository : EF_ApiRepository<UsuarioMultiCalculo>, IUsuarioMultiCalculoRepository
    {
        public UsuarioMultiCalculoRepository(ApiMultiCalculoContext dbAPIContext) : base(dbAPIContext)
        {
        }

        public UsuarioMultiCalculo GetUsuarioOperador(int idUsuarioMultiCalc)
        {
            try
            {
                UsuarioMultiCalculo usuarioMultiCalculo = GetAll(x => x.Umc_IdUsuarioMultiCalc == idUsuarioMultiCalc).FirstOrDefault();
                return usuarioMultiCalculo;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
