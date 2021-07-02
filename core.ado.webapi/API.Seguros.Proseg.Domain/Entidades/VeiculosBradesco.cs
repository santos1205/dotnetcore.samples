using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Seguros.Proseg.Domain.Entidades
{
    [Table("TblVeiculosBradesco", Schema = "dbo")]
    public class VeiculosBradesco
    {
        [Key]
        public int COD_VEIC { get; set; }
        public string DESCR_VEIC { get; set; }
        public int COD_FABR { get; set; }
        public string DESCR_FABR { get; set; }
        public int COD_COMB { get; set; }
        public string DESCR_COMB { get; set; }
        public int COD_USO { get; set; }
        public string DESCR_USO { get; set; }
        public int COD_FIPE { get; set; }
        public int PORTAS { get; set; }
        public string DESCR_PORTAS { get; set; }
        public int EIXOS { get; set; }
        public string DESCR_EIXOS { get; set; }
        public int COD_CAMBIO { get; set; }
        public string DESCR_CAMBIO { get; set; }
        public int PROCEDENCIA { get; set; }
        public string DESCR_PROC { get; set; }
        public string INIC_VIGENCIA { get; set; }
        public string DT_INI_FABR { get; set; }
        public string DT_FIM_FABR { get; set; }
        public int VIGENCIA_FIPE { get; set; }

    }
}
