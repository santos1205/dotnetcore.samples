using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionarioCOrg.ViewModels
{
    public class EmpresaQuestionarioVM
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public int IdQuestionario { get; set; }
        public string DtCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}