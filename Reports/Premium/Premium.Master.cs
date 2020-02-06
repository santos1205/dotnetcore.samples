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
    public partial class Premium : System.Web.UI.MasterPage
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
    }
}