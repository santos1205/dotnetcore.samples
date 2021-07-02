
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Seguros.Proseg.Domain.Entidades
{
    [Table("tblRespostasInterface", Schema = "dbo")]
    public class AtividadePrincipal
    {
        [Key]
        public int IdResposta { get; set; }
        public string StrDescricaoResposta { get; set; }
        public int IntIdPergunta { get; set; }
        public short IntIdTipoResposta { get; set; }
        public bool BolHabilitaPerguntaFilha { get; set; }
    }
}
