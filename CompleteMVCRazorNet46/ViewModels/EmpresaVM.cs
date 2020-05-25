using QuestionarioCOrg.DataAccess;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace QuestionarioCOrg.ViewModels
{
    public class EmpresaVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o cnpj da empresa")]
        public string CNPJ { get; set; }
        [Required(ErrorMessage = "Informe o nome da empresa")]
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Ramo { get; set; }

        public static EmpresaVM ToViewModel(Empresa Model)
        {
            var VM = new EmpresaVM();

            VM.Id = Model.emp_id;
            VM.Nome = Model.emp_nome;
            VM.CNPJ = Convert.ToUInt64(Model.emp_cnpj).ToString(@"00\.000\.000\/0000\-00");            
            VM.Cidade = Model.emp_cidade;
            VM.Estado = Model.emp_estado;
            VM.Ramo = Model.emp_ramo;

            return VM;
        }

        public Empresa ToModel()
        {
            var model = new Empresa()
            {
                emp_cnpj = Regex.Replace(CNPJ, "[^0-9,]", ""),
                emp_nome = Nome,
                emp_cidade = Cidade,
                emp_estado = Estado,
                emp_ativo = true
            };

            return model;
        }
    }
}