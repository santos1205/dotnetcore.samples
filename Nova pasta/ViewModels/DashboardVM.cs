using QuestionarioCOrg.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionarioCOrg.ViewModels
{
    public class DashboardVM
    {
        public int QtdeUsuarios { get; set; }
        public int QtdeFormularios { get; set; }
        public int QtdeEmpresas { get; set; }
        public int QtdeFormulariosRespondidos { get; set; }

        public IEnumerable<Empresa> Empresas { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }
    }
}