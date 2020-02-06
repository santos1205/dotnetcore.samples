using BaseAccess;
using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace slnSindicatoMedico.MasterPage
{
    public partial class WebForm1 : System.Web.UI.Page
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
        public static ICollection<AcaoVM> ListarAcoesLogAsync()
        {
            var Acoes = LogService.ListarAcoesLog();
            return AcaoService.Serialize(Acoes);
        }

        [WebMethod]
        public static ICollection<LogVM> ListarLogPorParams(int IdUsuario, int IdAcao, DateTime PInicio, DateTime PFim)
        {
            var Logs = LogService.ListarPorParams(IdUsuario, IdAcao, PInicio, PFim);

            // Caso retorne mais que 500 registros, limitar, para n dar erro na paginação js.
            var LogsSerilized = LogService.Serialize(Logs);

            
            if (Logs.Count() > 500)
                return LogsSerilized.Take(500).ToList();
            else
                return LogsSerilized.ToList();
        }


        [WebMethod]
        public static ICollection<UsuarioVM> ListarUsuariosAsync()
        {
            //var Usuarios = LogService.ListarUsuarios();
            var UsuariosOrdered = LogService.ListarUsuarios(orderByName: true);
            return UsuarioService.Serialize(UsuariosOrdered);
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