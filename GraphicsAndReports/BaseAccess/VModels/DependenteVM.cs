using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAccess.VModels
{
    public class DependenteVM
    {
        public int IdDependente { get; set; }
        public string Cpf { get; set; }
        public string Crm { get; set; }
        public string Nome { get; set; }
        public string EstadoCivil { get; set; }
        public string Sexo { get; set; }
        public string Profissao { get; set; }
        public int IdProfissao { get; set; }
        public string Nacionalidade { get; set; }
        public string Pis { get; set; }
        public string Cns { get; set; }
        public string Dn { get; set; }
        public string DtInicioVigencia { get; set; }
        public string DtFinalVigencia { get; set; }
        public string NomeMae { get; set; }
        public string Parentesco { get; set; }
        public string TPParentesco { get; set; }
        public string DtNascimento { get; set; }
        public string Email { get; set; }
        public string PlanoOdonto { get; set; }
        public int IdPlanoOdonto { get; set; }
        public int? MaxCount { get; set; }
        public string Status { get; set; }
        public string NrCarteirinha { get; set; }
        public int? IdFormaPagamento { get; set; }
        public ICollection<ProfissaoVM> CmbProfissoes { get; set; }
        public ICollection<PlanoVM> Planos { get; set; }
        public Endereco Endereco { get; set; }
        public Contato Contato { get; set; }
        
        public DependenteVM()
        {
            Endereco = new Endereco();
            Contato = new Contato();
            CmbProfissoes = new List<ProfissaoVM>();
            Planos = new List<PlanoVM>();
        }
    }
}
