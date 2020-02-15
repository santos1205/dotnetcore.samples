using QuestionarioCOrg.DataAccess;
using QuestionarioCOrg.Enums;
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
    public class CommonController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult VerificarSessionAsync()
        {
            var objU = Session["Usuario"] as Usuario;
            if (objU == null)                            
                return Json(new { Error = "Sessão expirada!" }, JsonRequestBehavior.AllowGet);

            return Json(new { Ok = "ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExpirarSession()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        public JsonResult SalvarQuestionario(ICollection<COrgVM> Medias)
        {
            var ctx = new QuestionarioOrgDBEntities();

            try
            {
                // Verifica a unicidade do formulário CORG
                int IdUsu = Medias.FirstOrDefault().IdUsuario;
                var ObjQ = ctx.Questionario.Where(x => x.qst_nome.Equals("Cultura Organizacional")).FirstOrDefault();
                if (FormularioService.VerificaUnicidadeQuestionario(IdUsu, ObjQ.qst_id, true))
                    FormularioService.SalvarCOrg(Medias);

                var Rtn = new { Return = "ok" };
                return Json(Rtn, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException e)
            {               

                string msgError = "Erro ao cadastrar o questionário: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                var RError = new { Error = msgError };
                return Json(RError, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msgError = string.Empty;
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    msgError = string.Format("Erro: {0}", ex.InnerException.InnerException.Message);
                else
                    msgError = string.Format("Erro: {0}", ex.Message);

                var RError = new { Error = msgError };

                return Json(RError, JsonRequestBehavior.AllowGet);
            }
        }


        #region PARTIALS
        public PartialViewResult _Toggle(string TName = null, string TNumber = null, bool? TChecked = null, string TColor = null, string TMethod = null)
        {
            ViewBag.TName = TName ?? "";
            ViewBag.TNumber = TNumber ?? "1";
            ViewBag.TColor = TColor ?? "#335D91";
            ViewBag.TChecked = TChecked ?? false;
            if (!string.IsNullOrEmpty(TMethod))
                ViewBag.TMethod = string.Format("javascript:{0};", TMethod);
            else
                ViewBag.TMethod = "";
            
            return PartialView();
        }

        public PartialViewResult _SuccessMessage(string Titulo = "Parabéns!", string Msg = "")
        {
            ViewData["Titulo"] = Titulo;
            ViewData["Msg"] = Msg;
            return PartialView();
        }


        public PartialViewResult _AcessoNegado(string Titulo = "Acesso Negado!", string Msg = "")
        {
            ViewData["Titulo"] = Titulo;
            ViewData["Msg"] = Msg;
            return PartialView();
        }

        public PartialViewResult _Notification()
        {
            var objU = Session["Usuario"] as Usuario;
            if (objU != null)            
                ViewBag.Acesso = objU.NivelAcesso.nvl_nome;

            // Consolida total de usuários sem avaliação            
            int NrUsuSemAvalicacao = AdminService.ConsolidarUsuariosSemAvaliacao(objU);
            int NrNovasRespostas = AdminService.ConsolidarNovasRespostas(objU);
            int NrNovosLeads = AdminService.ConsolidarNovosLeads(objU);

            ViewBag.NrUsuSemAvalicacao = NrUsuSemAvalicacao;
            ViewBag.NrNovasRespostas = NrNovasRespostas;
            ViewBag.NrNovosLeads = NrNovosLeads;

            ViewBag.NrTotalNotifs = NrUsuSemAvalicacao + NrNovasRespostas + NrNovosLeads;
            
            return PartialView();
        }
        public PartialViewResult _SideMenu()
        {
            var objU = Session["Usuario"] as Usuario;

            ViewBag.UserName = objU.usu_nome;
            ViewBag.MenuQuestionarios = CommonService.CarregarQuestionarios();

            return PartialView();
        }

        public PartialViewResult _InputCNPJ()
        {
            return PartialView();
        }

        public JsonResult CnpjValido(string CNPJ)
        {
            var CnpjValido = CommonService.CnpjValido(CNPJ);

            if (CnpjValido)
                return Json(new { Result = CnpjValido }, JsonRequestBehavior.AllowGet);

            return Json(new { Error = "CNPJ inválido" }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _UserBox()
        {
            var objU = Session["Usuario"] as Usuario;
            if (objU != null)
                ViewBag.UserName = objU.usu_nome;
            else
                ViewBag.UserName = "";
            ViewBag.MenuQuestionarios = CommonService.CarregarQuestionarios();

            return PartialView();
        }

        public PartialViewResult _MainMenu()
        {
            var objU = Session["Usuario"] as Usuario;
            if (objU != null)
            {
                ViewBag.Acesso = objU.NivelAcesso.nvl_nome;
                if (objU.NivelAcesso.nvl_nome.Equals(EnumHelper.GetDescription(NivelAcessoEnum.Respondente)))                                    
                    ViewBag.MenuQuestionarios = CommonService.CarregarQuestionariosPorEmpresa(objU.usu_emp_id);                
                if ((objU.NivelAcesso.nvl_nome.Equals(EnumHelper.GetDescription(NivelAcessoEnum.Gestor)) || (objU.NivelAcesso.nvl_nome.Equals(EnumHelper.GetDescription(NivelAcessoEnum.Administrativo)))))                
                    ViewBag.MenuQuestionarios = AdminService.ListarQuestionariosAtivos();                
            }
            else
                ViewBag.MenuQuestionarios = null;


            return PartialView();
        }
        #endregion        
    }
}