using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proseg.calculo.ModuloViagem
{
    public partial class DefaultTravelAceIFrame : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //BugId 5917 - Guilherme Almeida
            if (!IsPostBack)
            {
                string strUrlViagem = string.Empty;

				//#7667
				//Mário Santos
                strUrlViagem = ConfigurationSettings.AppSettings["url"] + "ModuloViagem/TravelAcordoProseg.aspx";                
                this.iframeGTA.Attributes["src"] = strUrlViagem;

                this.timerIFrameGTA.Enabled = false;
            }
        }
    }
}