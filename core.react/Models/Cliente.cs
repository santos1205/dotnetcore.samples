using Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovileWeb.DataAccess
{
    // View Model Cliente. Campos adicionais (não mapeados no banco. Apenas para as telas):
    public partial class Cliente
    {
        [NotMapped]
        [CPFValidation(ErrorMessage = "Insira um CPF válido")]
        public string Cpf { get; set; }
        [NotMapped]
        [CNPJValidation(ErrorMessage = "Insira um CNPJ válido")]
        public string Cnpj { get; set; }
        [NotMapped]
        public int TpPagamento { get; set; }
        [NotMapped]
        public int IdPlano { get; set; }
        [NotMapped]
        public string Plano { get; set; }
        [NotMapped]
        public int Banco { get; set; }
        [NotMapped]
        public string Agencia { get; set; }
        [NotMapped]
        public string Conta { get; set; }
    }
}
