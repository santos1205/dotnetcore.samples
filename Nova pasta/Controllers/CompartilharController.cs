using QuestionarioCOrg.Filters;
using QuestionarioCOrg.Service;
using QuestionarioCOrg.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace QuestionarioCOrg.Controllers
{
    [VerifySession, VerifyUserRoles]
    public class CompartilharController : Controller
    {
        // GET: Compartilhar
        public ActionResult Formularios()
        {
            #region Viewbags
            ViewBag.Success = TempData["Success"] ?? false;
            ViewBag.Error = TempData["Error"] ?? "";
            #endregion

            return View();
        }

        [HttpPost]
        public JsonResult Formularios(ICollection<CompartilhamentoVM> VM)
        {
            // Validação emails            
            if (VM.Count() > 0)
            {
                VM.FirstOrDefault().Emails = VM.FirstOrDefault().Emails.Replace(" ", "");
                var arrEmails = VM.FirstOrDefault().Emails.Split(';');
                foreach (string iemail in arrEmails)
                {
                    if (!CommonService.ValidarEmail(iemail))
                    {
                        string msgError = string.Format("Email inválido! {0}", iemail);
                        return Json(new { Error = msgError }, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            // Compartilhar emails
            try
            {
                AdminService.CompartilharFormularios(VM);
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao compartilhar o email: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                return Json(new { Error = msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {                
                if (ex.InnerException != null)
                    return Json(new { Error = ex.InnerException.InnerException.Message }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { Error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
            return Json(new { Success = "Os formulários foram compartilhados com sucesso!" }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _ListarFormularios()
        {
            ViewBag.Error = "";            
            ViewBag.Formularios = AdminService.ListarQuestionariosAtivos();

            return PartialView();
        }
    }
}