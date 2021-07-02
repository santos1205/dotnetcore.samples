using System.ComponentModel.DataAnnotations.Schema;

namespace MovileWeb.DataAccess
{
    // View Model Endereço. Campos adicionais (não mapeados no banco. Apenas para as telas):
    public class Endereco
    {
        [NotMapped]
        public string Logradouro { get; set; }

        [NotMapped]
        public string Bairro { get; set; }
        [NotMapped]
        public string Estado { get; set; }
        [NotMapped]
        public string Cidade { get; set; }
        [NotMapped]
        public string Cep { get; set; }
        [NotMapped]
        public int? Numero { get; set; }
    }
}
