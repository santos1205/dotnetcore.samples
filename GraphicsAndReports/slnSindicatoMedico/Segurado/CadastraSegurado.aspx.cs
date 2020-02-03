using BaseAccess;
using BaseAccess.Services;
using BaseAccess.VModels;
using slnSindicatoMedico.VModels;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace slnSindicatoMedico.Segurado
{
    public partial class CadastraSegurado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]        
        public static string CadastrarTitularAsync(BaseAccess.Segurado Segurado, DadosCobranca DadosCobranca, Endereco Endereco, Contato Contato, IEnumerable<PlanoSegurado> PlanosSegurado)
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;

            if (Usuario == null)
                return "";

            try
            {
                bool cadastrou = SeguradoService.CadastrarTitular(Usuario, Segurado);

                if (cadastrou)
                {
                    SeguradoService.SalvarFormaPagamento(Usuario, Segurado, true);
                    // Caso f. pagamento seja débito, gravar dados cobrança.
                    if (Segurado.seg_for_id == 3)
                    {
                        DadosCobranca.dco_seg_id = Segurado.seg_id;
                        //SeguradoService.SalvarDadosCobranca(DadosCobranca, Usuario);
                    }

                    // Após o cadastro do segurado, carregar o id na List PlanosSegurado
                    foreach (var planoSeg in PlanosSegurado)
                        planoSeg.pls_seg_id = Segurado.seg_id;
                    Endereco.end_seg_id = Segurado.seg_id;
                    Contato.cnt_seg_id = Segurado.seg_id;

                    SeguradoService.SalvarPlanoTitular(PlanosSegurado, Usuario, CadastroTitular: true);
                    SeguradoService.SalvarEnderecoSegurado(Endereco);
                    SeguradoService.SalvarContatoSegurado(Contato);
                    return "ok";
                }
                else
                    throw new Exception("erro.");
            }
            catch (Exception e)
            {
                return "erro: " + e.Message;
            }
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
        public static void CadastrarPlanoSegurado(PlanoSegurado PlanoSegurado)
        {
            PlanoService.CadastrarPlanoSegurado(PlanoSegurado);
        }

        [WebMethod]
        public static string CadastrarDependenteAsync(Dependente Dependente, Endereco Endereco, Contato Contato, PlanoSegurado PlanoDependente)
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;

            try
            {
                bool cadastrou = SeguradoService.CadastrarDependente(Usuario, Dependente);
                if (cadastrou)
                {
                    // Após o cadastro do dependente, é pego o id do plano odontologico do dependete para ser salvo. 

                    PlanoDependente.pls_seg_id = Dependente.dep_id;
                    PlanoDependente.pls_par_id = Dependente.dep_par_id;

                    Endereco.end_dep_id = Dependente.dep_id;
                    Contato.cnt_dep_id = Dependente.dep_id;

                    SeguradoService.SalvarPlanoDependente(PlanoDependente, Usuario);
                    SeguradoService.CadastrarEnderecoDependente(Endereco);
                    SeguradoService.CadastrarContatoDependente(Contato);
                    return "ok";
                }
                else return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            
        }

        [WebMethod]
        public static ICollection<UsuarioVM> ListarUsuariosPendentes(string NomeUsuario, string Status)
        {
            var Usuarios = UsuarioService.ConsultarPorParams(nome: NomeUsuario, status: Status);
            return UsuarioService.Serialize(Usuarios);
        }

        [WebMethod]
        public static ICollection<ProfissaoVM> ListarProfissoes()
        {
            var Prfs = SeguradoService.ListarProfissoes();
            return ProfissaoService.Serialize(Prfs);
        }
   
        [WebMethod]
        public static ICollection<EstadoCivilVM> CarregarComboEstadoCivilAsync()
        {
            var Esps = EstadoCivilService.ListarEstadoCivil();
            return EstadoCivilService.Serialize(Esps);
        }


        [WebMethod]
        public static ICollection<MelhorDiaPagVM> CarregarComboMelhorDiaAsync()
        {
            var listaMelhorDia = SeguradoService.ListarMelhorDiaPagamento();
            var listaOutPut = MelhorDiaPagamentoServices.Serialize(listaMelhorDia);

            return listaOutPut;
        }

        [WebMethod]
        public static ICollection<EspecialidadeVM> ListarEspecialidades()
        {
            var Esps = SeguradoService.ListarEspecialidade();
            return EspecialidadeService.Serialize(Esps);
        }

        [WebMethod]
        public static ICollection<SeguradoraVM> ListarSeguradoraAnterior()
        {
            var Seguradoras = SeguradoService.ListarSeguradoras();
            return SeguradoraService.Serialize(Seguradoras);
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
        public static void RealizaLogoutAsync()
        {
            HttpContext.Current.Session.Clear();
        }

        [WebMethod]
        public static IEnumerable<DependenteVM> ConsultaDependentesPorCpfTitularAsync(string cpf)
        {
            var Deps = SeguradoService.ConsultarDependentesPorCpfTitular(cpf);
            return DependenteService.Serialize(Deps);
        }

        [WebMethod]
        public static SeguradoVM ConsultaSeguradoPorCpfAsync(string cpf)
        {
            var Segurado = SeguradoService.ConsultarSeguradoPorCpf(cpf);
            return SeguradoService.Serialize(Segurado);
        }

        [WebMethod]
        public static void ExcluirDependentePorIdAsync(int Id)
        {
            SeguradoService.RemoverDependentePorId(Id);
        }

        [WebMethod]
        public static bool VerificarUnicidadeCPF(string cpf)
        {
            return SeguradoService.VerificarUnicidadeCPF(cpf);
        }


        [WebMethod]
        public static ICollection<PlanoVM> ConsultarPlanoSeguradoPorId(string idSegurado)
        {
            List<PlanoSeguradoVM> listaPlanos = PlanoService.ConsultarPlanoSeguradoPorId(int.Parse(idSegurado));
            return PlanoService.SerializarPlanosSegurado(listaPlanos);
        }

    }
}