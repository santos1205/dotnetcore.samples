using System.ComponentModel.DataAnnotations;

namespace QuestionarioCOrg.ViewModels
{
    public class ValorRespostaVM
    {
        [Key]
        public int Id { get; set; }
        public string Valor { get; set; }
    }
}