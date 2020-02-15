using QuestionarioCOrg.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionarioCOrg.Service
{
    public class RespostaService
    {
        public static void InserirValorResposta(int IdResposta, string Valor)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            var objR = db.Resposta.Find(IdResposta);
            objR.Valor.Add(new Valor()
            {
                vlr_valor = Valor,
                vlr_rsp_id = IdResposta,
                vlr_ativo = true,
                vlr_dt_cadastro = DateTime.Now
            });
            db.SaveChanges();
        }

        public static void ExcluirValorResposta(int Id)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            var objV = db.Valor.Find(Id);
            objV.vlr_ativo = false;
            db.SaveChanges();
        }
    }
}