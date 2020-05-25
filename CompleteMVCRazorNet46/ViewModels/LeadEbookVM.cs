using QuestionarioCOrg.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuestionarioCOrg.ViewModels
{
    public class LeadEbookVM
    {        
        public string Nome { get; set; }
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Número de telefone inválido")]
        public string Telefone { get; set; }
        public string Empresa { get; set; }
        public string Ramo { get; set; }
        public int? NRFuncionario { get; set; }
        public string Cidade { get; set; }


        public lead_empresa_lgpd ToModel()
        {
            var model = new lead_empresa_lgpd()
            {
                nome_completo = this.Nome,
                email = this.Email,
                telefone = Telefone ?? "",
                nome_empresa = Empresa ?? "",
                qnt_colaborador = NRFuncionario == null ? 0 : (int)NRFuncionario,
                cargo = Ramo ?? "",
                ramo = Ramo ?? "",
                dados_cliente = "",
                compartilha_dados = "",
                iniciou_adequacao = "",
                data_cadastro = DateTime.Now,
                cidade = Cidade,
                estado = "",
                formulario = "ebook"
            };

            return model;
        }
    }
}