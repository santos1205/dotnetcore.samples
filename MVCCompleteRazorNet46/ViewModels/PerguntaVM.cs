using QuestionarioCOrg.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuestionarioCOrg.ViewModels
{
    public class PerguntaVM
    {
        [Key]
        public int Id { get; set; }        
        public int? Numero { get; set; }
        public string Item { get; set; }
        [Required(ErrorMessage = "Descrição da pergunta obrigatória.")]
        public string Descricao { get; set; }        
        [Required(ErrorMessage = "Selecione um questionário.")]
        public int IdQuestionario { get; set; }
        public int? IdClassificacao { get; set; }

        public Pergunta ToModel()
        {
            var model = new Pergunta()
            {
                prg_numero = Numero,
                prg_item = Item,
                prg_descricao = Descricao,
                prg_cls_id = IdClassificacao,
                prg_qst_id = IdQuestionario,
                prg_ativo = true
            };

            return model;
        }

        public static PerguntaVM ToViewModel(Pergunta P)
        {


            var VM = new PerguntaVM()
            {
                Id = P.prg_id,
                Descricao = P.prg_descricao,                
                IdQuestionario = P.prg_qst_id,
                IdClassificacao = P.prg_cls_id
            };

            return VM;
        }
    }
}