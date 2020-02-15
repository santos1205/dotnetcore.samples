using QuestionarioCOrg.DataAccess;
using QuestionarioCOrg.Filters;
using QuestionarioCOrg.Service;
using QuestionarioCOrg.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionarioCOrg.Controllers
{
    [VerifySession]
    public class QuestionarioController : Controller
    {

        private QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

        // GET: Questionario
        public ActionResult Index()
        {
            ViewBag.MenuQuestionarios = CommonService.CarregarQuestionarios();
            return View();
        }

        public ActionResult Form()
        {
            ViewBag.Acesso = true;
            var ObjU = Session["Usuario"] as Usuario;
            if (ObjU != null)
            {
                ViewBag.IdUser = ObjU.usu_id;
                // Carrega o id do Cultura Org.
                var ObjQ = db.Questionario.Where(x => x.qst_nome.Equals("Cultura Organizacional")).FirstOrDefault();
                if (!AdminService.ValidaQuestionarioPorUsuario(ObjU, 0, true))
                    return RedirectToAction("Painel", "Formulario");
            }
                
            else
                ViewBag.IdUser = 0;
            
            ViewBag.MenuQuestionarios = CommonService.CarregarQuestionarios();
            return View();
        }

        public ActionResult Success(COrgVM VM)
        {
            return View();
        }
    }
}