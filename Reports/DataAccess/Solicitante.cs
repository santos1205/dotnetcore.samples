using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Solicitante
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Voucher { get; set; }
        public string Email { get; set; }
        public string Empresa { get; set; }
        public string Telefone { get; set; }
        public string Descricao { get; set; }
        
    }
}
