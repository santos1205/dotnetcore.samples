using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace core.migrationsSample.Dominio
{
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Descricao { get; set; }
        public int? Pedido_IdCliente { get; set; } 

        public virtual Cliente Cliente { get; set; }
    }
}
