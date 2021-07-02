using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Seguros.Proseg.Domain.Entidades
{
    [Table("tblProdutos", Schema = "dbo")]
    public class Produto
    {
        [Key]
        public int idCodProduto { get; set; }
        public int idSeguradora { get; set; }
        public string strProduto { get; set; }
        public int idTipoSeguro { get; set; }
        public string transmiteManual { get; set; }
        public bool bolAtivo { get; set; }

    }
}
