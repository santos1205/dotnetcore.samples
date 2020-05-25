using QuestionarioCOrg.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionarioCOrg.Service
{
    public class ClassificacaoService
    {
        public static void Salvar(int Id, string Nome)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            var objC = db.Classificacao.Find(Id);

            objC.cls_nome = Nome;

            db.SaveChanges();
        }
    }
}