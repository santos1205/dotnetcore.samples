using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Viagem.Infrastructure.Models
{
    [Table("Usuario", Schema = "dbo")]
    public class Usuario
    {
        [Key]
        public int Usr_ID { get; set; }
        public int Usr_ClientId { get; set; }
        public string Usr_ClientSecret { get; set; }
        public string Usr_Username { get; set; }
        public string Usr_Password { get; set; }
        public int Usr_IdEstipulante { get; set; }
        public bool Usr_BoolAtivo { get; set; }
    }
}
