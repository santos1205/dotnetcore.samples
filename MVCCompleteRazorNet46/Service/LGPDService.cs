using QuestionarioCOrg.DataAccess;
using QuestionarioCOrg.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionarioCOrg.Service
{
    public class LGPDService
    {
        readonly private static QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();
        public static void SalvarLead(LeadEbookVM VM)
        {
            db.lead_empresa_lgpd.Add(VM.ToModel());
            db.SaveChanges();
        }
    }
}