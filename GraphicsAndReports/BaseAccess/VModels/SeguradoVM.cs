using BaseAccess;
using BaseAccess.VModels;
using System.Collections.Generic;

namespace slnSindicatoMedico.VModels
{
    public class SeguradoVM
    {
        public int IdSegurado { get; set; }
        public string Cpf { get; set; }
        public string Crm { get; set; }
        public string CrmEstado { get; set; }
        public string PisPasep { get; set; }
        public string Cns { get; set; }
        public string Dn { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public int? IdEstadoCivil { get; set; }
        public string EstadoCivil { get; set; }
        public string Parentesco { get; set; }
        public string Nacionalidade { get; set; }
        public string Profissao { get; set; }
        public int? IdProfissao { get; set; }
        public string MelhorDiaPagamento { get; set; }
        public int? IdMelhorDiaPagamento { get; set; }

        public string PlanoOdonto { get; set; }
        public int? IdPlanoOdonto { get; set; }
        public int? IdSeguradora { get; set; }
        public string Especialidade { get; set; }
        public int? IdEspecialidade { get; set; }
        public string TPParentesco { get; set; }
        public string NomeMae { get; set; }
        public string Email { get; set; }        
        public int? MaxCount { get; set; }
        public string DataFiliacao { get; set; }
        public string DataNasc { get; set; }
        public string Status { get; set; }
        public string InicioVigencia { get; set; }
        public string FimVigencia { get; set; }
        public int? IdFormaPagamento { get; set; }
        public string NrFiliacao { get; set; }
        public string NrProposta { get; set; }
        public string NrCarteirinha { get; set; }

        public ICollection<Endereco> Enderecos { get; set; }        
        public ICollection<Contato> Contatos { get; set; }
        public ICollection<DadosCobranca> DadosCobranca { get; set; }
        public ICollection<DependenteVM> Dependentes { get; set; }
        public ICollection<Dependente> ModelDependentes { get; set; }
        public ICollection<PlanoVM> Planos { get; set; }

        public SeguradoVM()
        {
            this.Enderecos = new List<Endereco>();
            this.Contatos = new List<Contato>();
            this.DadosCobranca = new List<DadosCobranca>();
            this.Dependentes = new List<DependenteVM>();
            this.Planos = new List<PlanoVM>();
        }

        // Setters - atribuição aos valores privados
        public void SetCpf(string Cpf) => this.Cpf = Cpf == null ? "" : Cpf;
        public void SetCrm(int? Crm)
        {
            if (Crm == 0 || Crm == null)
                this.Crm = "";
            else
                this.Crm = Crm.ToString();
        }            
        public void SetCrmEstado(string CrmEstado) => this.CrmEstado = CrmEstado == null ? "" : CrmEstado;
        public void SetPisPasep(string PisPasep) => this.PisPasep = PisPasep == null ? "" : PisPasep;
        public void SetCns(string Cns) => this.Cns = Cns == null ? "" : Cns;
        public void SetDn(string Dn) => this.Dn = Dn == null ? "" : Dn;
        public void SetNome(string Nome) => this.Nome = Nome == null ? "" : Nome;
        public void SetSexo(string Sexo) => this.Sexo = Sexo == null ? "" : Sexo;
        public void SetEstadoCivil(string EstadoCivil) => this.EstadoCivil = EstadoCivil == null ? "" : EstadoCivil;
        public void SetParentesco(string Parentesco) => this.Parentesco = Parentesco == null ? "" : Parentesco;
        public void SetNacionalidade(string Nacionalidade) => this.Nacionalidade = Nacionalidade == null ? "" : Nacionalidade;
        public void SetProfissao(string Profissao) => this.Profissao = Profissao == null ? "" : Profissao;
        public void SetMelhorDiaPagamento(string MelhorDiaPagamento) => this.MelhorDiaPagamento = MelhorDiaPagamento == null ? "" : MelhorDiaPagamento;
        public void SetPlanoOdonto(string PlanoOdonto) => this.PlanoOdonto = PlanoOdonto == null ? "" : PlanoOdonto;

    }
}