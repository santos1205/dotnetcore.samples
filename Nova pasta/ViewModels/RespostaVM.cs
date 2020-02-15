using QuestionarioCOrg.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuestionarioCOrg.ViewModels
{
    public class RespostaVM
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Descrição da resposta obrigatória.")]
        public string Descricao { get; set; }
        public string Valor { get; set; }
        [Required(ErrorMessage = "Selecione um questionário.")]
        public int IdQuestionario { get; set; }
        public ICollection<Valor> Valores { get; set; }

        public Resposta ToModel()
        {
            var model = new Resposta()
            {                
                rsp_descricao = Descricao,
                rsp_qst_id = IdQuestionario,
                rsp_ativo = true
            };

            return model;
        }

        public static RespostaVM ToViewModel(Resposta R)
        {

            var VM = new RespostaVM()
            {
                Id = R.rsp_id,
                Descricao = R.rsp_descricao,
                IdQuestionario = R.rsp_qst_id
            };

            if (R.Valor != null)
                if (R.Valor.Count > 0)
                    VM.Valor = R.Valor.FirstOrDefault().vlr_valor;

            

            return VM;
        }
    }
}