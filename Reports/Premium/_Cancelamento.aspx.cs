using Common;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Premium
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static Usuario VerificaSessionAsync()
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as Usuario;
            return Usuario;
        }

        [WebMethod]
        public static string SolicitacaoCancelamentoAsync(Solicitante objSolicitante)
        {
            var Email = new Email();
            try 
            {
                Email.EnviarEmailCancelamento(objSolicitante);
                var Usuario = HttpContext.Current.Session["Usuario"] as Usuario;
                var Log = new Log
                {
                    IdUsuario = Usuario.IdUsuario,
                };
                Log.Cancelamento();
                return "ok";
            }
            catch(Exception e)
            {
                return "Erro ao solicitar o cancelamento: " + e.Message;
            }
            
        }
    }
}