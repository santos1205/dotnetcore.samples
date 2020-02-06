﻿using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Premium
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["iframeControl"] = "opened";
        }
        [WebMethod]
        public static Usuario VerificaSessionAsync()
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as Usuario;
            return Usuario;
        }
    }
}