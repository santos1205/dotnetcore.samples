using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Seguros.Proseg.Domain.DTOs
{
    public class UsuarioDTO
    {
        public int ID { get; set; }
        public int Client_id { get; set; }
        public string Client_secret { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IdEstipulante { get; set; }
        public bool BoolAtivo { get; set; }
    }
}
