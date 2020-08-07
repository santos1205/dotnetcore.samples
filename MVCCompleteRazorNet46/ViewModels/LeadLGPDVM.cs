using QuestionarioCOrg.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace QuestionarioCOrg.ViewModels
{
    public class LeadLGPDVM
    {
        [Key]
        public int Id { get; set; }        
        [Required(ErrorMessage = "Nome obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Email obrigatório"), EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Telefone obrigatório")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Número de telefone inválido")]
        public string Telefone { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "Nome da Empresa obrigatório")]
        public string Empresa { get; set; }
        [Required(ErrorMessage = "CNPJ da Empresa obrigatório")]
        public string CNPJ { get; set; }
        [Required(ErrorMessage = "Cidade da Empresa obrigatório")]
        public string CidadeEmpresa { get; set; }
        [Required(ErrorMessage = "Estado da Empresa obrigatório")]
        public string EstadoEmpresa { get; set; }
        [Required(ErrorMessage = "Cargo obrigatório")]
        public string Cargo { get; set; }
        //[Required(ErrorMessage = "Número de funcionários obrigatório")]
        public int NrFuncionarios { get; set; }
        [Required(ErrorMessage = "Ramo obrigatório")]
        public string Ramo { get; set; }

        public lead_empresa_lgpd ToModel()
        {
            string Telefone = Regex.Replace(this.Telefone, "[^0-9,]", "");
            var model = new lead_empresa_lgpd()
            {
                id = this.Id,
                nome_completo = this.Nome,
                email = this.Email,                
                telefone = Telefone ?? "",
                lgpd_id_empresa = IdEmpresa,
                nome_empresa = Empresa ?? "",
                qnt_colaborador = NrFuncionarios,
                cargo = Ramo ?? "",
                ramo = Ramo ?? "",
                dados_cliente = "",
                compartilha_dados = "",
                iniciou_adequacao = "",
                data_cadastro = DateTime.Now,
                cidade = CidadeEmpresa,
                estado = EstadoEmpresa,
                formulario = "leads LGPD"
            };

            return model;
        }

        public static LeadLGPDVM ToViewModel(lead_empresa_lgpd Lead)
        {
            var VM = new LeadLGPDVM();

            VM.Id = Lead.id;
            VM.Nome = Lead.nome_completo;
            VM.Email = Lead.email;
            VM.Telefone = Lead.telefone;
            VM.Empresa = Lead.nome_empresa;
            VM.Cargo = Lead.cargo;
            VM.NrFuncionarios = Lead.qnt_colaborador;
            VM.Ramo = Lead.ramo;
            
            return VM;
        }
    }
}