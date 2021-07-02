using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Seguros.Proseg.Domain.Entidades
{
    [Table("tblResultados", Schema = "dbo")]
    public class Resultados
    {
        [Key]
        public int idResultado { get; set; }
        public int idCalculo { get; set; }
        public int idCodProduto { get; set; }
        public int idStatus { get; set; }
        public int nmEmissao { get; set; }
        public int nmItem { get; set; }
        public DateTime? dteResultado { get; set; }
        public byte[] binResultado { get; set; }

    }
}
