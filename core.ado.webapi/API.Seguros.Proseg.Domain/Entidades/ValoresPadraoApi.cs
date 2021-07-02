using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Seguros.Proseg.Domain.Entidades
{

    [Table("ValoresPadraoApi", Schema = "dbo")]
    public class ValoresPadraoApi
    {
        [Key]
        public int IdValorPadrao { get; set; }
        public string JsonValorPadrao { get; set; }
        public bool BlnAtivo { get; set; }

    }
}
