using QuestionarioCOrg.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuestionarioCOrg.ViewModels
{
    public class ClassificacaoVM
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome da classificação obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Selecione um questionário.")]
        public int IdQuestionario { get; set; }


        public Classificacao ToModel()
        {
            var model = new Classificacao()
            {
                cls_nome = this.Nome,
                cls_qst_id = this.IdQuestionario,
                cls_dt_cadastro = DateTime.Now,
                cls_ativo = true
            };

            return model;
        }        
        public static ClassificacaoVM ToViewModel(Classificacao C)
        {
            var VM = new ClassificacaoVM()
            {
                Id = C.cls_id,
                Nome = C.cls_nome,
                IdQuestionario = C.cls_qst_id                
            };
            return VM;
        }
    }
}