using QuestionarioCOrg.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuestionarioCOrg.ViewModels
{
    public class ResultadosVM
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Selecione o questionário.")]
        public int? IdQuestionario { get; set; }
        public string Questionario { get; set; }
        [Required(ErrorMessage = "Selecione a empresa.")]
        public int? IdEmpresa { get; set; }
        public int? IdDepartamento { get; set; }
        public int? IdUsuarioRespondente { get; set; }
        public DateTime DtPreenchimento { get; set; }
        public int? MesPreenchimento { get; set; }
        public string Exibicao { get; set; }        // detalhado | consolidado
        public int? AnoPreenchimento { get; set; }
        public IEnumerable<RespostaUsuario> Respostas { get; set; }
        public IEnumerable<ResultadosCOrgVM> RespostasCOrg { get; set; }
    }
}