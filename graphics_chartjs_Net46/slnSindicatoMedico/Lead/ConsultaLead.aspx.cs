using BaseAccess;
using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace slnSindicatoMedico.MasterPage
{
    public partial class WebForm3 : System.Web.UI.Page
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
        public static ICollection<LeadVM> ListarLeadsPorParams(DateTime PInicio, DateTime PFim)
        {
            var Leads = LeadService.ListarPorParams(PInicio, PFim);

            // Caso retorne mais que 500 registros, limitar, para n dar erro na paginação js.
            var LeadsSerilized = LeadService.Serialize(Leads);

            if (Leads.Count() > 500)
                return LeadsSerilized.Take(500).OrderByDescending(x => x.DtCadastro).ToList();
            else
                return LeadsSerilized.OrderByDescending(x => x.DtCadastro).ToList();
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