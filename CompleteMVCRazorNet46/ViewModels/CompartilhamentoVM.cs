using QuestionarioCOrg.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionarioCOrg.ViewModels
{
    public class CompartilhamentoVM
    {
        public int IdFormulario { get; set; }
        public bool Selecionado { get; set; }
        public string Emails { get; set; }        
    }
}