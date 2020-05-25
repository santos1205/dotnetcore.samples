using QuestionarioCOrg.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuestionarioCOrg.ViewModels
{
    public class UsuarioVM
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Selecione a Empresa")]
        public int IdEmpresa { get; set; }
        public int IdAcesso { get; set; }
        [Required(ErrorMessage = "Selecione o Departamento")]
        public int IdDepartamento { get; set; }
        [Required(ErrorMessage = "Nome obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "CPF obrigatório")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "Email obrigatório"), EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Telefone obrigatório")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Número de telefone inválido")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "Senha obrigatória")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Confirmação de senha obrigatória")]
        public string SenhaConfirmacao { get; set; }
        [Required(ErrorMessage = "Data de Nascimento obrigatória")]
        public string DtNascimento { get; set; }
        public string Aprovado { get; set; }
        public string Cargo { get; set; }
        public string Departamento { get; set; }
        public string Empresa { get; set; }
        public bool Consentimento { get; set; }

        public Usuario ToModel()
        {            
            var model = new Usuario()
            {
                usu_nome = Nome,                
                usu_nvl_id = IdAcesso,
                usu_emp_id = IdEmpresa == 0 ? 1 : IdEmpresa,
                usu_dpt_id = IdDepartamento == 0 ? 1 : IdDepartamento,
                usu_cargo = Cargo,
                usu_dt_cadastro = DateTime.Now,
                usu_dt_nascimento = Convert.ToDateTime(DtNascimento),
                usu_cpf = CPF,
                usu_email = Email,
                usu_telefone = Telefone,
                usu_senha = Senha,
                usu_ativo = true
            };

            return model;
        }

        public static UsuarioVM ToViewModel(Usuario U)
        {
            var VM = new UsuarioVM();

            VM.Id = U.usu_id;
            VM.IdEmpresa = U.usu_emp_id;
            VM.IdDepartamento = U.usu_dpt_id;
            VM.Nome = U.usu_nome;
            VM.CPF = U.usu_cpf;
            VM.Email = U.usu_email;
            VM.Telefone = U.usu_telefone;
            VM.Senha = U.usu_senha;
            VM.DtNascimento = U.usu_dt_nascimento.ToString();
            VM.Cargo = U.usu_cargo;
            VM.Empresa = U.Empresa.emp_nome;
            VM.Departamento = U.Departamento.dpt_nome;

            return VM;
        }
    }
}