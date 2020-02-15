using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuestionarioCOrg.ViewModels
{
    public class LoginVM
    {        
        [Required(ErrorMessage = "Insira o e-mail para logar")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Insira a senha para logar")]
        public string Senha { get; set; }
        public int? RedirectQ { get; set; }
    }
}