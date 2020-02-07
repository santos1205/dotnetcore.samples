using BaseAccess;
using BaseAccess.Services;
using BaseAccess.VModels;
using slnSindicatoMedico.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Common;

namespace slnSindicatoMedico.Segurado
{
    public partial class ConsultaSegurado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
        }

        [WebMethod]
        public static bool? VerificaPadraoSenha()
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
            if (Usuario != null)
                return UsuarioService.VerificaPadraoSegurancaSenha(Usuario);
            else
                return null;
        }

        [WebMethod]
        public static string RedefinirPadraoSenha()
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
            string senhaHash = Cripto.GerarHash32(Usuario.usr_senha);
            return senhaHash;
        }


        [WebMethod]
        public static void RealizaLogoutAsync()
        {
            HttpContext.Current.Session.Clear();
        }

        [WebMethod]
        public static int VerificarNivelAcessoUsuarioAsync()
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
            if (Usuario != null)
                return Usuario.usr_nvl_id;
            else
                return 0;
        }

        [WebMethod]
        public static UsuarioVM VerificaSessionAsync()
        {
            var Usuario = UsuarioService.VerificarSession();
            try
            {
                return UsuarioService.Serialize(Usuario);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static ICollection<MelhorDiaPagVM> CarregarComboMelhorDiaAsync()
        {
            var listaMelhorDia = SeguradoService.ListarMelhorDiaPagamento();
            var listaOutPut = MelhorDiaPagamentoServices.Serialize(listaMelhorDia);

            return listaOutPut;
        }

        [WebMethod]
        public static ICollection<PlanoVM> CarregarComboPlanoAsync()
        {
            ICollection<Plano> listaPlanos = SeguradoService.ListarPlanos();
            var listaOutPut = PlanoService.Serialize(listaPlanos);

            return listaOutPut;
        }

        public static ICollection<PlanoVM> ConsultarPlanosSeguradoPorCpfAsync()
        {
            ICollection<Plano> listaPlanos = SeguradoService.ListarPlanos();
            var listaOutPut = PlanoService.Serialize(listaPlanos);

            return listaOutPut;
        }

        [WebMethod]
        public static SeguradoVM ConsultaSeguradoPorIdAsync(int Id)
        {
            var Segurado = SeguradoService.ConsultarSeguradoPorId(Id);            
            return SeguradoService.Serialize(Segurado);
        }


        [WebMethod]
        public static EnderecoVM ConsultaEnderecoBaseCep(string cep)
        {
            var Endereco = SeguradoService.ConsultarEnderecoBaseCEP(cep);
            return EnderecoService.Serialize(Endereco);
        }

        [WebMethod]
        public static List<PlanoSeguradoVM> ConsultarPlanoSeguradoPorId(string IdSegurado)
        {
            return PlanoService.ConsultarPlanoSeguradoPorId(int.Parse(IdSegurado));
        }

        [WebMethod]
        public static IEnumerable<ProfissaoVM> ListarProfissoesAsync()
        {
            var Profissoes = SeguradoService.ListarProfissoes();
            return ProfissaoService.Serialize(Profissoes); 
        }

        [WebMethod]
        public static SeguradoVM ConsultaDependentePorIdAsync(int Id)
        {
            var Dependente = SeguradoService.ConsultarDependentePorId(Id);
            var Segurado = new SeguradoVM();
            if(Dependente.EstadoCivil != null)
            {
                Segurado.SetEstadoCivil(Dependente.EstadoCivil.civ_descricao);
            }

            if (Dependente.dep_sexo == "M")
                Segurado.SetSexo("Masculino");
            if (Dependente.dep_sexo == "F")
                Segurado.SetSexo("Feminino");
            //Segurado.Sexo = Dependente.dep_sexo == "M" ? "Masculino" : "Feminino";
            Segurado.NomeMae = Dependente.dep_nome_mae;
            Segurado.DataNasc = Dependente.dep_data_nascimento.ToShortDateString();
            Segurado.Especialidade = "Médico";
            Segurado.SetNacionalidade(Dependente.dep_nacionalidade);
            Segurado.SetCpf(Dependente.dep_cpf);
            if(Dependente.Profissao != null)
            {
                Segurado.SetProfissao(Dependente.Profissao.prf_descricao);
                Segurado.IdProfissao = Dependente.dep_prf_id;
            }
                
            Segurado.SetPisPasep(Dependente.dep_pispasep);
            Segurado.SetCns(Dependente.dep_cns);
            Segurado.SetDn(Dependente.dep_dn);
            Segurado.NrCarteirinha = Dependente.dep_numero_carteira;
            Segurado.Email = Dependente.dep_email;

            if(Dependente.dep_fim_vigencia != null)
                Segurado.FimVigencia = DateTime.Parse(Dependente.dep_fim_vigencia.ToString()).ToShortDateString(); ;
            if (Dependente.dep_inicio_vigencia != null)
                Segurado.InicioVigencia = DateTime.Parse(Dependente.dep_inicio_vigencia.ToString()).ToShortDateString();
                        
            var end = SeguradoService.ConsultarEnderecoPorIdDependente(Dependente.dep_id);
            if (end != null)
            {
                var endOutPut = new Endereco
                {
                    end_endereco = end.end_endereco,
                    end_bairro = end.end_bairro,
                    end_cidade = end.end_cidade,
                    end_estado = end.end_estado,
                    end_cep = end.end_cep
                };
                Segurado.Enderecos.Add(endOutPut);

            }

            var plano = SeguradoService.ConsultarPlanoOdontoPorIdDependente(Dependente.dep_id);
            if (plano != null)
            {
                Segurado.IdPlanoOdonto = plano.pls_pla_id;
            }

            var cnt = SeguradoService.ConsultarContatoPorIdDependente(Dependente.dep_id);
            if(cnt != null)
            {
                var CntOutPut = new Contato
                {
                    cnt_cpf = Dependente.dep_cpf,
                    cnt_ddd = cnt.cnt_ddd,
                    cnt_ddd_celular = cnt.cnt_ddd_celular,
                    cnt_fone = cnt.cnt_fone,
                    cnt_celular = cnt.cnt_celular
                };
                Segurado.Contatos.Add(CntOutPut);

            }

            return Segurado;
        }

        [WebMethod]
        public static void SalvarStatusSeguradoAsync(SeguradoVM Segurado)
        {
            //Atualiza status segurados
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
            SeguradoService.AtualizarStatus(Segurado, Usuario);            
        }

        [WebMethod]
        public static string SalvarDadosDependentesAsync(Dependente Dependente, Endereco Endereco, Contato Contato, PlanoSegurado Plano)
        {
            try
            {
                var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
                if (Usuario == null)
                    return "error";
                SeguradoService.SalvarDadosDependente(Usuario, Dependente);

                Plano.pls_seg_id = Dependente.dep_id;
                Plano.pls_par_id = "D";

                SeguradoService.AtualizarPlanoOdontoDependente(Plano);

                SeguradoService.AtualizarEnderecoSegurado(Endereco);
                SeguradoService.SalvarContato(Contato);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return "success";
        }


        [WebMethod]
        public static string SalvarDadosTitularAsync(BaseAccess.Segurado Segurado, Endereco Endereco)
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;

            try
            {                
                if (Usuario == null)
                    throw new Exception("Sua sessão expirou!");

                SeguradoService.SalvarDadosTitular(Usuario, Segurado);
                SeguradoService.AtualizarEnderecoSegurado(Endereco);
            }
            catch(Exception e)
            {
                return e.Message;
            }

            return "";
        }


        [WebMethod]
        public static void SalvarFormaPagamentoAsync(BaseAccess.Segurado Segurado, DadosCobranca DadosCobranca)
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;

            if (Usuario == null)
                return;

            SeguradoService.SalvarMelhorDiaPagamento(Usuario, Segurado);
        }

        [WebMethod]
        public static void SalvarMelhorDiaPagamentoAsync(BaseAccess.Segurado Segurado)
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;

            if (Usuario == null)
                return;

            SeguradoService.SalvarMelhorDiaPagamento(Usuario, Segurado);
        }
            

        [WebMethod]
        public static void SalvarPlanoTitularAsync(IEnumerable<PlanoSegurado> PlanosSegurado)
        {            
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;

            if (Usuario == null)
                return;

            SeguradoService.SalvarPlanoTitular(PlanosSegurado, Usuario);            
        }

        [WebMethod]
        public static void SalvarContatosSeguradoAsync(ContatoVM Contato)
        {

            //Atualiza status segurados
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;


            if (Usuario == null)
                return;


            SeguradoService.AtualizarContatosSegurado(Usuario, Contato);
        }

        [WebMethod]
        public static ICollection<UsuarioVM> ListarUsuariosPendentes(string NomeUsuario, string Status)
        {
            var Usuarios = UsuarioService.ConsultarPorParams(nome: NomeUsuario, status: Status);

            
            return UsuarioService.Serialize(Usuarios);
        }

        [WebMethod]
        public static ICollection<BancoVM> ListarBancos()
        {
            var Bancos = SeguradoService.ListarBancos().OrderBy(x => x.ban_id).ToList();
            return BancoService.Serialize(Bancos); 
        }


        [WebMethod]
        public static ICollection<SeguradoVM> ListarSeguradoPorParams(int crm, string nome, string cpf, string ativo, string email, string telefone)
        {            

            var Segurados = SeguradoService.ListarPorParams(crm, nome, cpf, ativo, email, telefone);

            // Caso retorne mais que 500 registros, limitar, para n dar erro na paginação js.
            var SeguradoSerialize = SeguradoService.Serialize(Segurados);            

            if (Segurados.Count() > 500)
                return SeguradoSerialize.Take(500).ToList();
            else
                return SeguradoSerialize.ToList();
        }
    }
}