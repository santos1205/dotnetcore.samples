using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Seguros.Proseg.Domain.Entidades
{
    [Table("tblVeiculos", Schema = "dbo")]
    public class Veiculo
    {
        [Key]
        public int IdVeiculo { get; set; }
        public double IdMarca { get; set; }
        public string StrVeiculo { get; set; }
        public double IntAnoInicio { get; set; }
        public double IntAnoFim { get; set; }
        public double IntPortas { get; set; }
        public string StrCombustivel { get; set; }
        public double IntPassageiros { get; set; }
        public string StrFIPE { get; set; }
        public string StrMOLICAR { get; set; }
        public double BolAtivo { get; set; }
        public string StrModelo { get; set; }
        public string StrOrigem { get; set; }
        public string Strversaomodelo { get; set; }
        public string Strcomplemento { get; set; }
        public double IdCataut { get; set; }

    }
}
