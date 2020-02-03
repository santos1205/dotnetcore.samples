using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAccess.VModels
{
    public class EnderecoVM
    {
        public string Id { get; set; }
        public string Endereco { get; set; }
        public string Cpf { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
