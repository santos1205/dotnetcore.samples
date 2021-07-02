using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Seguros.Proseg.Domain.Entidades
{

    [Table("UsuarioMultiCalculo", Schema = "dbo")]
    public class UsuarioMultiCalculo
    {
        [Key]
        public int Umc_ID { get; set; }
        public int Umc_Usr_ID { get; set; }
        public int Umc_IdUsuarioMultiCalc { get; set; }
        public string Umc_Cpf { get; set; }
        public string Umc_Nome { get; set; }
        public int Umc_Ponto_Venda { get; set; }
        public bool Umc_BoolAtivo { get; set; }

    }
    
}
