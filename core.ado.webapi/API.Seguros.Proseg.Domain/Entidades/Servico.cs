using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Seguros.Proseg.Domain.Entidades
{

    [Table("Servico", Schema = "dbo")]
    public class Servico
    {
        [Key]
        public int Svc_ID { get; set; }
        public string Svc_Nome{ get; set; }
        public string Svc_Descricao { get; set; }
        public bool Svc_BoolAtivo { get; set; }
    }
}
