using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Seguros.Proseg.Domain.Entidades
{
    [Table("StatusAcesso", Schema = "dbo")]
    public class StatusAcesso
    {
        [Key]
        public int Sta_ID { get; set; }
        public string Sta_Nome { get; set; }
        public string Sta_Descricao { get; set; }
        public bool Sta_BoolAtivo { get; set; }

    }
}
