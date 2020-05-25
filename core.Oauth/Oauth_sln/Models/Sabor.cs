using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Oauth_sln.Models
{
    public class Sabor
    {        
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar(180)")]
        public string Descricao { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string Valor { get; set; }
    }
}
