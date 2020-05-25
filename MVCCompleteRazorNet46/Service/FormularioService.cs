using QuestionarioCOrg.DataAccess;
using QuestionarioCOrg.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionarioCOrg.Service
{
    public class FormularioService
    {
        public static void Salvar(IEnumerable<RespostaUsuario> Respostas)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            // Carrega user info
            int IdUsuario = Respostas.FirstOrDefault().rpu_usu_id;
            var objU = db.Usuario.Find(IdUsuario);

            foreach (var rsps in Respostas)
            {

                rsps.rpu_dpt_id = objU.usu_dpt_id;
                rsps.rpu_emp_id = objU.usu_emp_id;
                rsps.rpu_datacadastro = DateTime.Now;
                db.RespostaUsuario.Add(rsps);
            }

            db.SaveChanges();
        }

        public static bool VerificaUnicidadeQuestionario(int IdU, int IdQ = 0, bool QCOrg = false)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            if (!QCOrg)
            {
                var ObjRU = db.RespostaUsuario.Where(x => x.rpu_usu_id == IdU && x.rpu_qst_id == IdQ);

                // Se existe resposta para o usuário no respectivo questionário, retorna unicidade falsa.
                if (ObjRU.Count() > 0)
                    return false;
            }else
            {
                var ObjRU = db.Media.Where(x => x.mda_usu_id == IdU);

                // Se existe resposta para o usuário no respectivo questionário, retorna unicidade falsa.
                if (ObjRU.Count() > 0)
                    return false;
            }
            
            return true;
        }
        public static void SalvarCOrg(IEnumerable<COrgVM> Medias)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();
            foreach (var md in Medias)
            {
                db.Media.Add(md.ToModel());
            }

            db.SaveChanges();
        }
    }
}