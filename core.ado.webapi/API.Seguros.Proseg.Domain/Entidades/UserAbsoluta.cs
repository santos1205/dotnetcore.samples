using System.ComponentModel.DataAnnotations;

namespace API.Seguros.Proseg.Domain.Entidades
{
    public class UserAbsoluta
    {
        public UserAbsoluta()
        {

        }

        [Key]
        public string pstrCliente { get; set; }
        public string pstrLogin { get; set; }
        public string pstrSenha { get; set; }
        
    }
}
