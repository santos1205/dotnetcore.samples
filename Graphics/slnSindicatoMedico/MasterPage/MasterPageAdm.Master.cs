using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace slnEnergiaSolar.MasterPage
{
    public partial class MasterPageAdm : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hddnrandom.Value = new Random().Next(1, 1000).ToString(); 
        }
    }
}