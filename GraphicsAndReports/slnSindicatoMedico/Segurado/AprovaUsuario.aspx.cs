using BaseAccess;
using BaseAccess.Enums;
using BaseAccess.VModels;
using Common;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace slnSindicatoMedico.MasterPage
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        [WebMethod]
        public static void RealizaLogoutAsync()
        {
            HttpContext.Current.Session.Clear();
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
        public static string AlteraPerfilUsuarioAsync(string TpAcesso, int IdUsuario)
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
            if (Usuario == null)
                return null;

            try
            {
                UsuarioService.AtualizarNivelAcessoUsuario(TpAcesso, IdUsuario, Usuario);
                return null;
            }
            catch (Exception e)
            {
                return "Erro durante a atualização do perfil " + e.Message;
            }
        }

        [WebMethod]
        public static ICollection<UsuarioVM> ListarUsuariosPendentes(string NomeUsuario, string Status)
        {
            var Usuarios = UsuarioService.ConsultarPorParams(nome: NomeUsuario, status: Status);            
            return UsuarioService.Serialize(Usuarios);
        }

        [WebMethod]
        public static string AprovarUsuarioAsync(int IdUsuario)
        {
            var Usuario = UsuarioService.ConsultarPorId(IdUsuario);
            UsuarioService.AprovarUsuario(Usuario.usr_id);

            // Envia email ao usuário aprovado.                
            var email = new Email();
            email.EnviarAprovacaoUsuario(Usuario.usr_nome, Usuario.usr_cpf, Usuario.usr_senha, Usuario.usr_email);
            var SessionUser = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
            LogService.CapturaOpcaoLog(SessionUser, AcaoEnum.AprovacaoUsuario.ToString(), Usuario.usr_id);
            return "";
        }

        [WebMethod]
        public static string ReprovarUsuarioAsync(int IdUsuario)
        {
            var Usuario = UsuarioService.ConsultarPorId(IdUsuario);

            var UsuarioSession = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;


            UsuarioService.ReprovarUsuario(UsuarioSession, Usuario.usr_id);

            // Envia email ao usuário aprovado.                
            var Email = new Email();
            Email.EnviarAprovacaoUsuario(Usuario.usr_nome, Usuario.usr_cpf, Usuario.usr_senha, Usuario.usr_email);
            //Log.AprovarUsuario(UsuarioLogado, usr_id);                               //usr_id é o do usuário aprovado
            return "";
        }
    }
}