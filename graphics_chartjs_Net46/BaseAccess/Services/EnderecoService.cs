using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAccess.Services
{
    public class EnderecoService
    {
        public static EnderecoVM Serialize(Endereco Endereco)
        {
            var EndVM = new EnderecoVM()
            {
                Endereco = Endereco.end_endereco,
                Bairro = Endereco.end_bairro,
                Cidade = Endereco.end_cidade,
                Estado = Endereco.end_estado
            };
            return EndVM;
        }
    }
}
