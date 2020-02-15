using QuestionarioCOrg.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuestionarioCOrg.ViewModels
{
    public class QuestionarioVM
    {
        [Key]
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Selecione uma empresa")]
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "Nome do questionário obrigatório")]
        public string Nome { get; set; }
        public IEnumerable<Pergunta> Perguntas { get; set; }
        public IEnumerable<Resposta> Respostas { get; set; }
        public IEnumerable<Classificacao> Classificacoes { get; set; }
        public IEnumerable<RespostaUsuario> RespostasUsuario { get; set; }
        public bool Publicado { get; set; }        
        public string MsgErro { get; set; }

        public Questionario ToModel()
        {
            var model = new Questionario()
            {
                qst_nome = this.Nome,
                qst_datacadastro = DateTime.Now,
                qst_ativo = true,
                qst_publicado = Publicado
            };

            return model;
        }

        public static QuestionarioVM ToViewModel(Questionario Q)
        {
            var VM = new QuestionarioVM();
            
            VM.Nome = Q.qst_nome;
            VM.Perguntas = Q.Pergunta.Where(x => x.prg_ativo);
            VM.Respostas = Q.Resposta.Where(x => x.rsp_ativo);
            VM.Classificacoes = Q.Classificacao.Where(x => x.cls_ativo);
            VM.Publicado = Q.qst_publicado;

            // se formulário n tiver classificações nas perguntas. colocar uma flag de 'sem classificação', para controle da view.
            if (VM.Classificacoes.Count() == 0)
                if (VM.Perguntas.Count() > 0)
                {
                    var ListCls = new List<Classificacao>();
                    
                    var ObjCls = new Classificacao
                    {
                        cls_nome = "sem classificação",
                        cls_ativo = true
                    };

                    ListCls.Add(ObjCls);

                    VM.Classificacoes = ListCls;                    
                }

            return VM;
        }
    }
}