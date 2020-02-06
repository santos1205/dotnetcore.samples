using BaseAccess;
using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Common;
using BaseAccess.Services;

namespace slnSindicatoMedico.MasterPage
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        public static object PainelGerencial { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {            
        }

        [WebMethod]
        public static UsuarioVM VerificaSessionAsync()
        {
            var Usuario = UsuarioService.VerificarSession();
            try
            {
                return UsuarioService.Serialize(Usuario);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static GerencialVM CarregarConsolidadoSeguradoAsync()
        {
            return GerencialService.Serialize(GerencialService.ConsultaConsolidadoSegurado());
        }


        [WebMethod]
        public static GerencialVM CarregarConsolidadoPagamentoAsync()
        {            
            return GerencialService.Serialize(GerencialService.ConsultaConsolidadoPagamento());            
        }

        
        [WebMethod]
        public static GerencialVM CarregarConsolidadoFaturamentoAsync()
        {
            return GerencialService.Serialize(GerencialService.ConsultaConsolidadoFaturamento());
        }


        [WebMethod]
        public static ICollection<GerencialGraficoVM> CarregarDetalhadoGraficoAsync()
        {
            // Período anual
            return GerencialService.GraficoSerialize(GerencialService.ConsultaDetalhadoGraficoSegurados());
        }

		[WebMethod]
        public static ICollection<GerencialVM> CarregarConsolidadoPremioAsync()
        {
            return GerencialService.Serialize(GerencialService.ConsultaConsolidadoPremio());
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
        public static void RealizaLogoutAsync()
        {
            HttpContext.Current.Session.Clear();
        }

        [WebMethod]
        public static ICollection<UsuarioVM> ListarUsuariosPendentes(string NomeUsuario, string Status)
        {
            var Usuarios = UsuarioService.ConsultarPorParams(nome: NomeUsuario, status: Status);
            return UsuarioService.Serialize(Usuarios);
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
    }
}