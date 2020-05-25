using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionarioCOrg.DataAccess
{
    public partial class Email
    {
        public string Titulo { get; set; }
        public string Corpo { get; set; }
        public bool? IsHtml { get; set; }
    }
}