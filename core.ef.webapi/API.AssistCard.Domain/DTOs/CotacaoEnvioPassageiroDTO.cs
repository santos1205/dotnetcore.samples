using System;
using System.Collections.Generic;
using System.Text;

namespace API.Viagem.Domain.DTOs
{
    public class CotacaoEnvioPassageiroDTO
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool PassageiroPrincipal { get; set; }
    }
}
