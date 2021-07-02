using Common;
using System;
using System.Collections.Generic;

namespace MovileWeb.DataAccess
{
    public partial class Cliente
    {
        public Cliente()
        {
            Pedido = new HashSet<Pedido>();
        }
        public int CliId { get; set; }        
        public string CliCpfCnpj { get; set; }
        public string CliNome { get; set; }
        public string CliEmail { get; set; }
        public string CliCep { get; set; }
        public string CliEndLogradouro { get; set; }
        public string CliEndNum { get; set; }
        public string CliEndComplemento { get; set; }
        public string CliEndBairro { get; set; }
        public string CliEndCidade { get; set; }
        public string CliEndUf { get; set; }
        public string CliTelefone { get; set; }
        public DateTime CliDate { get; set; }

        public ICollection<Pedido> Pedido { get; set; }
    }
}
