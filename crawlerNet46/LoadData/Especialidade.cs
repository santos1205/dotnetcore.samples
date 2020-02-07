using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadData
{
    [Table("Especialidades")]
    public class Especialidade
    {
        [Key]
        public int Id { get; set; }
        public int IdMedico { get; set; }
        public string Descricao { get; set; }
    }
}
