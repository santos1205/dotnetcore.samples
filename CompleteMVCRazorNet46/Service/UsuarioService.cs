using QuestionarioCOrg.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace QuestionarioCOrg.Service
{
    public class UsuarioService
    {
        private static QuestionarioOrgDBEntities _dbcontext = new QuestionarioOrgDBEntities();
        public static IEnumerable<Usuario> ConsultarUsuario(string Nome = null, bool? Aprovado = null)
        {
            IEnumerable<Usuario> lsUsuarios = _dbcontext.Usuario;
            
            if (Aprovado == true)
                lsUsuarios = lsUsuarios.Where(x => x.usu_aprovado != null);
            if (Aprovado == false)
                lsUsuarios = lsUsuarios.Where(x => x.usu_aprovado == null);
            if (Nome != null)
                lsUsuarios = lsUsuarios.Where(x => x.usu_nome.Contains(Nome));

            return lsUsuarios;
        }

        public static void SalvarNivelAcesso(string Acesso, int IdUsuario)
        {   
            var objU = _dbcontext.Usuario.Find(IdUsuario);
            var objAcesso = _dbcontext.NivelAcesso.Where(x => x.nvl_nome.Equals(Acesso)).FirstOrDefault();
            objU.NivelAcesso = objAcesso;

            _dbcontext.SaveChanges();
        }
    }
}