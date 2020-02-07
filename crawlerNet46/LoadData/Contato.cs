using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadData
{
    [Table("Contatos")]
    public class Contato
    {
        [Key]
        public int Id { get; set; }
        public int IdMedico { get; set; }
        public string TpContato { get; set; }
        public string NrContato { get; set; }
        public string Permite { get; set; }
    }
}
