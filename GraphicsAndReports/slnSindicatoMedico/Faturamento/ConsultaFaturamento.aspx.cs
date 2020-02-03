using BaseAccess;
using BaseAccess.Services;
using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace slnSindicatoMedico.MasterPage
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // MOCKS DE TESTE

            //var pags = ListarFaturamentoPorParams(DateTime.Parse("05-06-2019"), DateTime.Parse("30-06-2019"), "", 1, null, null, "", "");
        }

        [WebMethod]
        public static ICollection<PagamentoVM> ListarFaturamentoPorParams(
            DateTime DtInicial,
            DateTime DtFinal,
            string NrCarteira,
            int? IdPlano,
            int? Crm,
            string Cpf,
            string Nome 
        )
        {
            try
            {
                var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
                var Faturamentos = FaturamentoService.ListarPorParams(DtInicial, DtFinal, NrCarteira, IdPlano, Crm, Cpf, Nome, Usuario);

                // Caso retorne mais que 500 registros, limitar, para n dar erro na paginação js.
                var FaturamentosSerilized = PagamentoService.Serialize(Faturamentos);


                if (Faturamentos.Count() > 500)
                    return FaturamentosSerilized.Take(500).ToList();
                else
                    return FaturamentosSerilized.ToList();
            }
            catch (Exception ex)
            {
                var pagVM = new PagamentoVM()
                {
                    MsgErro = "erro. " + ex.Message
                };

                var listaVM = new List<PagamentoVM>();
                listaVM.Add(pagVM);
                return listaVM;
            }
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
        public static int VerificarNivelAcessoUsuarioAsync()
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
            if (Usuario != null)
                return Usuario.usr_nvl_id;
            else
                return 0;
        }

        [WebMethod]
        public static ICollection<SeguradoReajusteVM> ListarMFSeguradosPorParams(
            DateTime DtAniversarioInicial,
            DateTime DtAniversarioFinal,
            int? IdPlano,
            int? Crm,
            string Cpf,
            string Nome
        )
        {
            try
            {
                var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
                
                var MFSegurados = MudancaFaixaService.ListarSeguradosMFPorParams(DtAniversarioInicial, DtAniversarioFinal, Crm, IdPlano, Cpf, Nome, Usuario);
                var MFSeguradosOrdenados = MudancaFaixaService.OrdernarTitularDependentesMF(MFSegurados);

                return MFSeguradosOrdenados;
            }
            catch (Exception ex)
            {
                var msErr = ex.Message;
            }

            return null;
        }

        [WebMethod]
        public static ICollection<UsuarioVM> ListarUsuariosPendentes(string NomeUsuario, string Status)
        {
            var Usuarios = UsuarioService.ConsultarPorParams(nome: NomeUsuario, status: Status);
            return UsuarioService.Serialize(Usuarios);
        }

        [WebMethod]
        public static void RealizaLogoutAsync()
        {
            HttpContext.Current.Session.Clear();
        }
    }
}