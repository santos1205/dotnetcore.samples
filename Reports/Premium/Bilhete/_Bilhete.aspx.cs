using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Premium.Bilhete
{
    public partial class _Bilhete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["iframeControl"] == "opened")
            {
                Session["iframeControl"] = "closed";
                Response.Redirect("http://bilhete.travelace.com.br");
            }
            else
                Response.Redirect("~/Acesso/ManterUsuario.aspx", true);
        }
    }
}