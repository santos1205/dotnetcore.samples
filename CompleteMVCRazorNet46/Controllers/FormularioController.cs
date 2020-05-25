using QuestionarioCOrg.DataAccess;
using QuestionarioCOrg.Enums;
using QuestionarioCOrg.Filters;
using QuestionarioCOrg.Service;
using QuestionarioCOrg.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionarioCOrg.Controllers
{
    [VerifySession]
    public class FormularioController : Controller
    {

        private QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

        public ActionResult Painel()
        {
            #region ViewBags
            ViewBag.MenuQuestionarios = CommonService.CarregarQuestionarios();
            ViewBag.Acesso = TempData["Acesso"] ?? true;
            #endregion

            Usuario Usuario = HttpContext.Session.Contents["Usuario"] as Usuario;
            if (Usuario == null)
                return RedirectToAction("Index", "Login");

            if (Usuario.NivelAcesso.nvl_nome.Equals(EnumHelper.GetDescription(NivelAcessoEnum.Respondente)))
                return RedirectToAction("Index", new { Id = 0 });
            else
                return RedirectToAction("Index", "Dashboard");
        }


        // GET: Id Formulario
        [HttpGet, Route("~/Questionarios/{id}")]
        public ActionResult Index(int? Id = 0, bool success = false)
        {
            #region ViewBags
            ViewBag.MenuQuestionarios = CommonService.CarregarQuestionarios();
            ViewBag.Success = success;            
            ViewBag.Acesso = true;
            ViewBag.Error = "";
            #endregion

            var ObjU = Session["Usuario"] as Usuario;
            var Questionario = db.Questionario.Find(Id);
            var VM = QuestionarioVM.ToViewModel(Questionario);

            try
            {
                // Se Id = 0, abre o primeiro questionario q o usuário tem acesso
                if (Id == 0)
                {
                    var ObjQ = ObjU.Empresa.EmpresaQuestionario.Where(x => x.Questionario.qst_nome != "Não Informado" && x.eqt_ativo);
                    if (ObjQ != null && ObjQ.Count() > 0)
                    {
                        var Q = ObjQ.FirstOrDefault().Questionario;
                        return RedirectToAction("Index", new { Id = Q.qst_id });
                    }
                }
                
                // Verifica Unicidade de preenchimento de formulário - cada respondente só pode responder o formulário uma única vez
                if (ObjU != null)
                    if (!FormularioService.VerificaUnicidadeQuestionario(ObjU.usu_id, (int)Id))
                        ViewBag.Acesso = false;
                                
                // Verifica pelo id, se o questionário existe para a empresa ao qual o usuário está logado.            
                if (!AdminService.ValidaQuestionarioPorUsuario(ObjU, (int)Id))
                    return RedirectToAction("Painel", "Formulario");
                

                VM.IdUsuario = ObjU.usu_id;                

                return View(VM);
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao cadastrar o questionário: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                ViewBag.Error = msgError;
                return View(new QuestionarioVM());
            }
            catch (Exception ex)
            {
                string MsgError = string.Empty;
                ViewBag.Success = false;

                if (ex.InnerException != null)
                    MsgError = ex.InnerException.InnerException.Message;
                else
                    MsgError = ex.Message;

                ViewBag.Error = MsgError;
                return View(new QuestionarioVM());
            }
        }

        public JsonResult ListarQuestionarios()
        {
            var Questionarios = db.Questionario.Where(x => x.qst_ativo);
            List<QuestionarioVM> Quests = new List<QuestionarioVM>();

            foreach(var qst in Questionarios)
            {
                var VM = new QuestionarioVM();
                VM.Nome = qst.qst_nome;
                VM.Id = qst.qst_id;
                Quests.Add(VM);
            }

            var objr = new { Resultado = "meus resultados" };
            return  Json(Quests, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Salvar(IEnumerable<RespostaUsuario> Respostas)
        {
            try
            {
                FormularioService.Salvar(Respostas);

                return Json(new { Ok = "ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao cadastrar o questionário: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                return Json("Error: " + msgError, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string MsgError = string.Empty;

                if (ex.InnerException != null)
                    MsgError = ex.InnerException.InnerException.Message;
                else
                    MsgError  = ex.Message;

                return Json("Error: " + MsgError, JsonRequestBehavior.AllowGet);
            }
        }
    }
}