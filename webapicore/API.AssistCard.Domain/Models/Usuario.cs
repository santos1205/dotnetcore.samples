using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.AssistCard.Infrastructure.Models
{
    [Table("usuario", Schema = "dbo")]
    public class Usuario
    {
        [Key]
        public int  Usu_ID { get; set; }
        public string Usu_Nome { get; set; }      
        public bool Usu_Ativo { get; set; }  
    }
}
