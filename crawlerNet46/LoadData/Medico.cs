using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadData
{
    [Table("Medicos")]
    public class Medico
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }        
        public string Cpf { get; set; }
        public string DataNascimento { get; set; }
        public string Rg { get; set; }                
    }
}
