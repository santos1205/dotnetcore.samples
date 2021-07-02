using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Seguros.Proseg.Domain.Entidades
{
    [Table("tblMarcas", Schema = "dbo")]
    public class Marca
    {
        [Key]
        public float IdcodMarca { get; set; }
        public string StrMarca { get; set; }
        public string CodMo { get; set; }
    }

}


