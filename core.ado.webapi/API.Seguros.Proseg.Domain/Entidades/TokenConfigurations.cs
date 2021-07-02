using System.ComponentModel.DataAnnotations;

namespace API.Seguros.Proseg.Domain.Entidades
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}
