using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Seguros.Proseg.Domain.Entidades
{
    [Table("tblVeiculosPlacas", Schema = "dbo")]
    public class VeiculosPlaca
    {
        [Key]
        public DateTime DteConsulta { get; set; }
        public string StrPlaca { get; set; }
        public string StrChassi { get; set; }
        public string StrFabricante { get; set; }
        public int IntAnoFabricaca { get; set; }
        public int IntAnoModelo { get; set; }
        public string StrCombustivel { get; set; }
        public string StrMarca { get; set; }
        public string StrModelo { get; set; }
        public string StrVeiculo { get; set; }
        public string StrVersao { get; set; }
        public int IntPortas { get; set; }
        public string StrFipe { get; set; }
        public string StrCor { get; set; }
        public string StrMotor { get; set; }
        public string StrIPdoComputad { get; set; }
        public string StrNomeDoComput { get; set; }
        public int StrCodigoDeCont { get; set; }
        public string StrSeveridade { get; set; }
        public string StrDescricao { get; set; }
        public string StrAcaoAdotada { get; set; }
        public string StrResultadoDaP { get; set; }
        public string StrGravacaoDoLo { get; set; }
        public string StrNrTransacao { get; set; }

    }
}
