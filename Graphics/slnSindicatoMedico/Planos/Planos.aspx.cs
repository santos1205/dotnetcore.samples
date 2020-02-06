using BaseAccess;
using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace slnSindicatoMedico.Planos
{
    public partial class Planos : System.Web.UI.Page
    {
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

    }
}