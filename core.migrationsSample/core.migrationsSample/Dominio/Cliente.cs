using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace core.migrationsSample.Dominio
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Nome { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Endereco { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
