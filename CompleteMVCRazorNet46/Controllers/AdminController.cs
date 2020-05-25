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
    [VerifySession, VerifyUserRoles]
    public class AdminController : Controller
    {

        private QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

        public ActionResult Index()
        {
            return RedirectToAction("Questionario");
        }
        public ActionResult EditarQuestionario(int Id)
        {
            #region ViewBags            
            var objQs = db.Questionario.Where(x => x.qst_ativo && x.qst_nome != "Cultura Organizacional").OrderByDescending(x => x.qst_datacadastro);
            ViewBag.LsQuestionarios = objQs.Count() > 0 ? objQs : null;
            ViewBag.Success = false;
            #endregion

            var Q = db.Questionario.Find(Id);


            return View(QuestionarioVM.ToViewModel(Q));
        }


        public JsonResult RemoverQuestionario(int Id)
        {
            try
            {
                var ObjU = Session["Usuario"] as Usuario;
                AdminService.RemoverRegistro(EnumHelper.GetDescription(CrudEnum.Questionario), Id, ObjU.NivelAcesso.nvl_nome);
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao remover o registro: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                return Json(new { Error = msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    return Json(new { Error = ex.InnerException.InnerException.Message }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { Error = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            var Q = db.Questionario.Find(Id);
            Q.qst_ativo = false;
            db.SaveChanges();

            var Rtn = new { Return = "ok" };
            return Json(Rtn, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RemoverClassificacao(int IdC, int IdQ = 0)
        {
            try
            {
                var ObjU = Session["Usuario"] as Usuario;
                AdminService.RemoverRegistro("Classificacao", IdC, ObjU.NivelAcesso.nvl_nome);

                bool Empty = db.Classificacao.Where(x => x.cls_ativo && x.cls_qst_id == IdQ).Count() == 0 ? true : false;

                var Rtn = new { Return = "ok", Empty, Removed_id = IdC };
                return Json(Rtn, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao remover o registro: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                return Json(new { Error = msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    return Json(new { Error = ex.InnerException.InnerException.Message }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { Error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AtualizarNotificacoes()
        {
            var ObjU = Session["Usuario"] as Usuario;

            try
            {
                AdminService.AtualizarNotificacoes(ObjU);
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao atualizar as notificações: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                return Json(new { Error = msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    return Json(new { Error = ex.InnerException.InnerException.Message }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { Error = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Success = "Success" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RemoverEmpresa(int IdE)
        {
            try
            {
                var ObjU = Session["Usuario"] as Usuario;
                AdminService.RemoverRegistro("Empresa", IdE, ObjU.NivelAcesso.nvl_nome);

                var Rtn = new { Return = "ok", Removed_id = IdE };
                return Json(Rtn, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao remover o registro: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                return Json(new { Error = msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    return Json(new { Error = ex.InnerException.InnerException.Message }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { Error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult RemoverPergunta(int IdP, int IdQ = 0)
        {
            try
            {
                var ObjU = Session["Usuario"] as Usuario;
                AdminService.RemoverRegistro("Pergunta", IdP, ObjU.NivelAcesso.nvl_nome);

                bool Empty = db.Pergunta.Where(x => x.prg_ativo && x.prg_qst_id == IdQ).Count() == 0 ? true : false;

                var Rtn = new { Return = "ok", Empty, Removed_id = IdP };
                return Json(Rtn, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao remover o registro: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                return Json(new { Error = msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    return Json(new { Error = ex.InnerException.InnerException.Message }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { Error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult RemoverResposta(int IdR, int IdQ = 0)
        {
            try
            {
                var ObjU = Session["Usuario"] as Usuario;
                AdminService.RemoverRegistro("Resposta", IdR, ObjU.NivelAcesso.nvl_nome);
                bool Empty = db.Resposta.Where(x => x.rsp_ativo && x.rsp_qst_id == IdQ).Count() == 0 ? true : false;
                var Rtn = new { Return = "ok", Empty, Removed_id = IdR };
                return Json(Rtn, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao remover o registro: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                return Json(new { Error = msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    return Json(new { Error = ex.InnerException.InnerException.Message }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { Error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarQuestionario(QuestionarioVM VM)
        {
            #region ViewBags            
            ViewBag.LsQuestionarios = db.Questionario.Where(x => x.qst_ativo && x.qst_nome != "Cultura Organizacional").OrderByDescending(x => x.qst_datacadastro);
            ViewBag.Success = true;
            ViewBag.Error = string.Empty;
            #endregion
            if (!ModelState.IsValid)
            {
                ViewBag.Success = false;
            }

            try
            {
                AdminService.SalvarQuestionario(VM);
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao cadastrar o questionário: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                ViewBag.Error = msgError;
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    ViewBag.Error = ex.InnerException.InnerException.Message;
                else
                    ViewBag.Error = ex.Message;
            }

            return RedirectToAction("Questionario", "Admin");
        }

        public ActionResult Questionario()
        {
            #region ViewBags            
            // var objQs = db.Questionario.Where(x => x.qst_ativo && x.qst_nome != "Cultura Organizacional").OrderByDescending(x => x.qst_datacadastro);
            var objQs = AdminService.ListarQuestionariosAtivos().OrderByDescending(x => x.qst_datacadastro);
            ViewBag.LsQuestionarios = objQs.Count() > 0 ? objQs : null;
            ViewBag.MenuQuestionariosPublicados = CommonService.CarregarQuestionariosPublicados();
            ViewBag.Success = false;
            #endregion
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Questionario(QuestionarioVM VM)
        {
            #region ViewBags            
            ViewBag.LsQuestionarios = db.Questionario.Where(x => x.qst_ativo && x.qst_nome != "Cultura Organizacional").OrderByDescending(x => x.qst_datacadastro);
            ViewBag.MenuQuestionariosPublicados = CommonService.CarregarQuestionariosPublicados();
            ViewBag.Success = true;
            ViewBag.Error = string.Empty;
            #endregion
            if (!ModelState.IsValid)
            {
                ViewBag.Success = false;
                return View(VM);
            }

            try
            {
                AdminService.SalvarQuestionario(VM);
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao cadastrar o questionário: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                ViewBag.Error = msgError;
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    ViewBag.Error = ex.InnerException.InnerException.Message;
                else
                    ViewBag.Error = ex.Message;
            }


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Classificacao(ClassificacaoVM VM)
        {
            #region ViewBags

            ViewBag.Success = true;
            ViewBag.Error = string.Empty;
            #endregion
            if (!ModelState.IsValid)
            {
                // Capturando erros no ModelState.Isvalid
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();


                ViewBag.Success = false;
                VM.Nome = VM.Nome ?? string.Empty;
                return View(VM);
            }

            try
            {
                if (VM.Id != 0)
                {
                    var objC = db.Classificacao.Find(VM.Id);
                    objC.cls_nome = VM.Nome;
                    db.SaveChanges();
                }
                else
                {
                    var objC = VM.ToModel();
                    db.Classificacao.Add(objC);
                    db.SaveChanges();
                    VM.Id = objC.cls_id;
                }
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao cadastrar classificação: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                ViewBag.Error = msgError;
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    ViewBag.Error = ex.InnerException.InnerException.Message;
                else
                    ViewBag.Error = ex.Message;
            }

            return RedirectToAction("Classificacao", "Admin", new { success = true, IdQ = VM.IdQuestionario });
        }
        public ActionResult Classificacao(bool success = false, int IdQ = 0, int IdC = 0)
        {
            #region ViewBags

            ViewBag.Success = success;
            #endregion

            var VM = new ClassificacaoVM();
            if (IdC != 0)
            {
                var objC = db.Classificacao.Find(IdC);
                VM = ClassificacaoVM.ToViewModel(objC);
            }
            else
                VM.IdQuestionario = IdQ;


            return View(VM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Empresa(EmpresaVM VM)
        {
            #region ViewBags

            ViewBag.Success = true;
            ViewBag.Error = string.Empty;
            #endregion
            if (!ModelState.IsValid)
            {
                // Capturando erros no ModelState.Isvalid
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();


                ViewBag.Success = false;
                return View(VM);
            }

            if (!CommonService.CnpjValido(VM.CNPJ))
            {
                ViewBag.Success = false;
                ViewBag.Error = "CNPJ inválido";
                return View(VM);
            }

            try
            {
                // Se registro novo, verifica unicidade por cnpj.
                if (VM.Id == 0)
                {
                    // Verifica se cnpj já existe, se sim, n cadastra
                    if (AdminService.ConsultarEmpresaPorCnpj(VM.CNPJ) != null)
                    {
                        ViewBag.Success = false;
                        ViewBag.Error = "CNPJ já existe no sistema.";
                        return View(VM);
                    }
                }

                VM = AdminService.SalvarEmpresa(VM);
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao cadastrar empresa: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                TempData["Error"] = msgError;
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    TempData["Error"] = ex.InnerException.InnerException.Message;
                else
                    TempData["Error"] = ex.Message;
            }

            TempData["Success"] = true;
            // TempData["IdEmpresa"] = VM.Id;
            return RedirectToAction("Empresa", "Admin", new { });
        }
        public ActionResult Empresa(int IdE = 0)
        {
            #region ViewBags
            ViewBag.Success = (bool?)TempData["Success"] ?? false;
            ViewBag.Error = TempData["Error"] ?? "";
            IdE = (int?)TempData["IdEmpresa"] ?? IdE;
            #endregion
            var VM = new EmpresaVM();
            if (IdE != 0)
            {
                var objE = db.Empresa.Find(IdE);
                VM = EmpresaVM.ToViewModel(objE);
            }

            return View(VM);
        }

        [HttpPost]
        public JsonResult SalvarPermissaoFormulario(EmpresaQuestionarioVM VM)
        {
            try
            {
                AdminService.SalvarPermissaoFormulario(VM.Id, VM.Ativo);
                return Json(new { Ok = "ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao salvar o permissão de formulário: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                return Json(new { Error = msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msgError = "Erro ao salvar o permissão de formulário: ";
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    msgError += ex.InnerException.InnerException.Message;
                else
                    msgError += ex.Message;

                return Json(new { Error = msgError }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult PermissaoFormularios()
        {
            #region ViewBags
            ViewBag.IdEmpresa = new SelectList(db.Empresa.Where(x => x.emp_ativo).OrderBy(x => x.emp_nome), "emp_id", "emp_nome");
            #endregion

            return View();
        }

        public ActionResult Lead()
        {
            #region ViewBags
            var OptsTpLeads = new List<SelectListItem>();
            OptsTpLeads.Add(new SelectListItem() { Text = "Lead", Value = "Lead" });
            OptsTpLeads.Add(new SelectListItem() { Text = "DPO", Value = "treinamento" });
            ViewBag.TpLead = new SelectList(OptsTpLeads, "Value", "Text");
            #endregion

            return View();
        }


        public ActionResult Usuario()
        {
            #region ViewBags

            var OptsSituacao = new List<SelectListItem>();
            OptsSituacao.Add(new SelectListItem() { Text = "Aprovado", Value = "1" });
            OptsSituacao.Add(new SelectListItem() { Text = "Sem Avaliação", Value = "2" });
            ViewBag.Situacao = new SelectList(OptsSituacao, "Value", "Text");
            ViewBag.Success = false;
            #endregion

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Usuario(UsuarioVM VM)
        {
            #region ViewBags

            ViewBag.IdEmpresa = new SelectList(db.Empresa, "emp_id", "emp_nome");
            ViewBag.IdDepartamento = new SelectList(db.Departamento, "dpt_id", "dpt_nome");
            ViewBag.Success = true;
            #endregion

            if (!ModelState.IsValid)
            {
                ViewBag.Success = false;
                return View(VM);
            }

            try
            {

                VM.Departamento = VM.IdDepartamento != 0 ? db.Departamento.Find(VM.IdDepartamento).dpt_nome : "";
                VM.Empresa = VM.IdEmpresa != 0 ? db.Empresa.Find(VM.IdEmpresa).emp_nome : "";

                db.Usuario.Add(VM.ToModel());
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao cadastrar usuário: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                ViewBag.Error = msgError;
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    ViewBag.Error = ex.InnerException.InnerException.Message;
                else
                    ViewBag.Error = ex.Message;
            }

            return View(VM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pergunta(PerguntaVM VM)
        {
            #region ViewBags

            ViewBag.Success = true;
            ViewBag.Error = string.Empty;
            #endregion

            if (!ModelState.IsValid)
            {
                // Capturando erros no ModelState.Isvalid
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();


                ViewBag.Success = false;
                return View(VM);
            }

            try
            {
                if (VM.Id != 0)
                {
                    var objP = db.Pergunta.Find(VM.Id);
                    objP.prg_descricao = VM.Descricao;
                    objP.prg_cls_id = VM.IdClassificacao;
                    db.SaveChanges();
                }
                else
                {
                    var objP = VM.ToModel();
                    db.Pergunta.Add(objP);
                    db.SaveChanges();
                    VM.Id = objP.prg_id;
                }
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao cadastrar pergunta: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                return RedirectToAction("Pergunta", "Admin", new { ErrorMsg = msgError, IdQ = VM.IdQuestionario, IdC = VM.IdClassificacao });
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    return RedirectToAction("Pergunta", "Admin", new { ErrorMsg = ex.InnerException.InnerException.Message, IdQ = VM.IdQuestionario, IdC = VM.IdClassificacao });

                else
                    return RedirectToAction("Pergunta", "Admin", new { ErrorMsg = ex.Message, IdQ = VM.IdQuestionario, IdC = VM.IdClassificacao });
            }

            return RedirectToAction("Pergunta", "Admin", new { success = true, IdQ = VM.IdQuestionario, IdC = VM.IdClassificacao });
        }
        public ActionResult Pergunta(bool success = false, int IdQ = 0, int IdC = 0, int IdP = 0, string ErrorMsg = null)
        {
            #region ViewBags

            ViewBag.Success = success;
            ViewBag.Error = ErrorMsg ?? "";

            #endregion

            var VM = new PerguntaVM();
            if (IdP != 0)
            {
                var objP = db.Pergunta.Find(IdP);
                VM = PerguntaVM.ToViewModel(objP);
            }
            else
            {
                VM.IdQuestionario = IdQ;
                VM.IdClassificacao = IdC;
            }

            return View(VM);
        }
        public ActionResult Resposta(bool success = false, int IdQ = 0, int IdR = 0)      // IdQ = IdQeustionario - IdR = IdResposta
        {
            #region ViewBags

            ViewBag.Success = success;
            ViewBag.IdClassificacao = new SelectList(db.Classificacao, "cls_id", "cls_nome");
            #endregion

            var VM = new RespostaVM();
            if (IdR != 0)
            {
                var objR = db.Resposta.Find(IdR);
                VM = RespostaVM.ToViewModel(objR);
            }
            else
                VM.IdQuestionario = IdQ;

            return View(VM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Resposta(RespostaVM VM)
        {
            #region ViewBags

            ViewBag.Success = true;
            ViewBag.Error = string.Empty;
            #endregion

            if (!ModelState.IsValid)
            {
                ViewBag.Success = false;
                return View(VM);
            }

            try
            {
                if (VM.Id != 0)
                {
                    var objR = db.Resposta.Find(VM.Id);
                    objR.rsp_descricao = VM.Descricao;
                    if (!string.IsNullOrEmpty(VM.Valor))
                    {
                        var objV = objR.Valor.FirstOrDefault();
                        if (objV != null)
                            objV.vlr_valor = VM.Valor;
                        else
                            objR.Valor.Add(new Valor()
                            {
                                vlr_valor = VM.Valor,
                                vlr_rsp_id = VM.Id,
                                vlr_dt_cadastro = DateTime.Now,
                                vlr_ativo = true
                            });
                    }

                    db.SaveChanges();
                }
                else
                {
                    var objR = VM.ToModel();
                    // Se tiver valor, grava na tbl valor
                    objR.Valor.Add(new Valor()
                    {
                        vlr_valor = VM.Valor ?? "",
                        vlr_rsp_id = VM.Id,
                        vlr_dt_cadastro = DateTime.Now,
                        vlr_ativo = true
                    });
                    db.Resposta.Add(objR);
                    db.SaveChanges();
                    VM.Id = objR.rsp_id;
                }
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao cadastrar Resposta: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                ViewBag.Error = msgError;
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    ViewBag.Error = ex.InnerException.InnerException.Message;
                else
                    ViewBag.Error = ex.Message;
            }

            return RedirectToAction("Resposta", "Admin", new { success = true, IdQ = VM.IdQuestionario });
        }


        public JsonResult SalvarClassificacao(int Id, string Nome)
        {
            try
            {
                ClassificacaoService.Salvar(Id, Nome);

                return Json(new { Ok = "ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao editar classificação: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                return Json(new { Error = msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msgError = "Erro ao editar classificação: ";
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    msgError += ex.InnerException.InnerException.Message;
                else
                    msgError += ex.Message;

                return Json(new { Error = msgError }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult SalvarAprovacaoUsuario(int Id, bool Checked)
        {

            try
            {

                var objU = db.Usuario.Find(Id);

                if (Checked)
                    objU.usu_aprovado = DateTime.Now;
                else
                    objU.usu_aprovado = null;

                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao aprovar usuário: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                ViewBag.Error = msgError;

                return Json(new { Error = "Erro: " + msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msgError = "";

                if (ex.InnerException != null)
                    msgError = ex.InnerException.InnerException.Message;
                else
                    msgError = ex.Message;

                return Json(new { Error = "Erro: " + msgError }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Ok = "ok", Id = Id }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Resultados()
        {
            #region ViewBags
            // Carregando os meses de preenchimento para combo.
            var objMeses = db.RespostaUsuario.ToList();
            var mesVerificador = 0;
            var anoVerificador = 0;
            var MesesParaFiltro = new List<SelectListItem>();
            var AnosParaFiltro = new List<SelectListItem>();
            var ExibicaoParaFiltro = new List<SelectListItem>();
            int auxContMes = 1;

            // Carrega os anos para filtro
            foreach (var item_ano in objMeses.OrderBy(x => x.rpu_datacadastro))
            {
                if (anoVerificador != item_ano.rpu_datacadastro.Year)
                {
                    string strAno = item_ano.rpu_datacadastro.Year.ToString();
                    AnosParaFiltro.Add(new SelectListItem() { Text = strAno, Value = strAno });
                    anoVerificador = item_ano.rpu_datacadastro.Year;
                    // Carrega os meses para filtro
                    foreach (var item_mes in objMeses.OrderBy(x => x.rpu_datacadastro))
                    {
                        bool ExisteMesFiltro = false;
                        if (mesVerificador != item_ano.rpu_datacadastro.Month || auxContMes == objMeses.Count)
                        {
                            // Verifica se o mes já n foi adicionado de outros anos.
                            foreach (var itemMFiltro in MesesParaFiltro)
                                if (item_mes.rpu_datacadastro.Month.ToString() == itemMFiltro.Value)
                                    ExisteMesFiltro = true;
                            if (ExisteMesFiltro)
                                continue;
                            string strMes = string.Empty;
                            strMes = item_mes.rpu_datacadastro.Month == 1 ? "janeiro" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 2 ? "fevereiro" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 3 ? "março" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 4 ? "abril" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 5 ? "maio" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 6 ? "junho" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 7 ? "julho" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 8 ? "agosto" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 9 ? "setembro" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 10 ? "outubro" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 11 ? "novembro" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 12 ? "dezembro" : strMes;

                            MesesParaFiltro.Add(new SelectListItem() { Text = strMes, Value = item_mes.rpu_datacadastro.Month.ToString() });
                            mesVerificador = item_mes.rpu_datacadastro.Month;
                        }
                        auxContMes++;
                    }
                }
            }

            ExibicaoParaFiltro.Add(new SelectListItem() { Text = "Detalhado", Value = "detalhado" });
            ExibicaoParaFiltro.Add(new SelectListItem() { Text = "Consolidado", Value = "consolidado" });

            ViewBag.IdUsResp = 0;
            ViewBag.MesPreenchimento = new SelectList(MesesParaFiltro, "Value", "Text");
            ViewBag.AnoPreenchimento = new SelectList(AnosParaFiltro, "Value", "Text");
            ViewBag.Exibicao = new SelectList(ExibicaoParaFiltro, "Value", "Text");
            // Carrega os questionarios para filtro
            var QuestParaFiltro = new List<SelectListItem>();
            var ObjCOrg = db.Questionario.Where(x => x.qst_nome.Contains("Cultura Organizacional")).FirstOrDefault();
            int IdQCOrg = 0;
            if (ObjCOrg != null)
                IdQCOrg = ObjCOrg.qst_id;

            foreach (var itemRU in db.RespostaUsuario)
            {
                bool ExisteQuestFiltro = false;
                // Verifica se o questionário já n foi adicionado.
                foreach (var itemQFiltro in QuestParaFiltro)
                    if (itemRU.Questionario.qst_nome == itemQFiltro.Text)
                        ExisteQuestFiltro = true;
                if (ExisteQuestFiltro)
                    continue;
                
                QuestParaFiltro.Add(new SelectListItem() { Text = itemRU.Questionario.qst_nome, Value = itemRU.Questionario.qst_id.ToString() });
                // Verifica o form. C. Org
                var ObjMedias = db.Media;
                if (ObjMedias.Count() > 0)                
                    QuestParaFiltro.Add(new SelectListItem() { Text = "Cultura Organizacional", Value = IdQCOrg.ToString() });                
                
            }
            ViewBag.IdQuestionario = new SelectList(QuestParaFiltro, "Value", "Text");

            // Carrega os respondentes para filtro - por padrão vem vazio
            var RespondentesParaFiltro = new List<SelectListItem>();
            ViewBag.IdUsuarioRespondente = new SelectList(RespondentesParaFiltro, "Value", "Text");

            // Carrega as empresas para filtro
            var EmpresaParaFiltro = new List<SelectListItem>();

            foreach (var itemRU in db.RespostaUsuario)
            {
                bool ExisteEmpFiltro = false;
                // Verifica se o questionário já n foi adicionado.
                foreach (var itemEmpFiltro in EmpresaParaFiltro)
                    if (itemRU.Empresa.emp_nome == itemEmpFiltro.Text)
                        ExisteEmpFiltro = true;
                if (ExisteEmpFiltro)
                    continue;

                EmpresaParaFiltro.Add(new SelectListItem() { Text = itemRU.Empresa.emp_nome, Value = itemRU.Empresa.emp_id.ToString() });
            }

            ViewBag.IdEmpresa = new SelectList(EmpresaParaFiltro, "Value", "Text");
            ViewBag.IdDepartamento = new SelectList(db.Departamento, "dpt_id", "dpt_nome");
            #endregion

            return View();
        }

        [HttpPost]
        public ActionResult Resultados(ResultadosVM VM)
        {   
            #region ViewBags
            // Carregando os meses de preenchimento para combo.
            var objMeses = db.RespostaUsuario.ToList();
            var mesVerificador = 0;
            var anoVerificador = 0;
            var MesesParaFiltro = new List<SelectListItem>();
            var AnosParaFiltro = new List<SelectListItem>();
            int auxContMes = 1;

            // Carrega os anos para filtro
            foreach (var item_ano in objMeses.OrderBy(x => x.rpu_datacadastro))
            {
                if (anoVerificador != item_ano.rpu_datacadastro.Year)
                {
                    string strAno = item_ano.rpu_datacadastro.Year.ToString();
                    AnosParaFiltro.Add(new SelectListItem() { Text = strAno, Value = strAno });
                    anoVerificador = item_ano.rpu_datacadastro.Year;
                    // Carrega os meses para filtro
                    foreach (var item_mes in objMeses.OrderBy(x => x.rpu_datacadastro))
                    {
                        bool ExisteMesFiltro = false;
                        if (mesVerificador != item_ano.rpu_datacadastro.Month || auxContMes == objMeses.Count)
                        {
                            // Verifica se o mes já n foi adicionado de outros anos.
                            foreach (var itemMFiltro in MesesParaFiltro)
                                if (item_mes.rpu_datacadastro.Month.ToString() == itemMFiltro.Value)
                                    ExisteMesFiltro = true;
                            if (ExisteMesFiltro)
                                continue;
                            string strMes = string.Empty;
                            strMes = item_mes.rpu_datacadastro.Month == 1 ? "janeiro" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 2 ? "fevereiro" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 3 ? "março" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 4 ? "abril" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 5 ? "maio" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 6 ? "junho" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 7 ? "julho" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 8 ? "agosto" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 9 ? "setembro" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 10 ? "outubro" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 11 ? "novembro" : strMes;
                            strMes = item_mes.rpu_datacadastro.Month == 12 ? "dezembro" : strMes;

                            MesesParaFiltro.Add(new SelectListItem() { Text = strMes, Value = item_mes.rpu_datacadastro.Month.ToString() });
                            mesVerificador = item_mes.rpu_datacadastro.Month;
                        }
                        auxContMes++;
                    }
                }
            }

            // Carrega os respondentes para filtro - por padrão vem vazio
            var RespondentesParaFiltro = new List<SelectListItem>();
            ViewBag.IdUsuarioRespondente = new SelectList(RespondentesParaFiltro, "Value", "Text", VM.IdUsuarioRespondente);
            ViewBag.IdUsResp = VM.IdUsuarioRespondente ?? 0;
            VM.IdUsuarioRespondente = VM.IdUsuarioRespondente ?? 0;
            ViewBag.MesPreenchimento = new SelectList(MesesParaFiltro, "Value", "Text");
            ViewBag.AnoPreenchimento = new SelectList(AnosParaFiltro, "Value", "Text");
            // Carrega os questionarios para filtro
            var QuestParaFiltro = new List<SelectListItem>();
            // Carrega o nome do questionario para a view
            if (VM.IdQuestionario != null)
                VM.Questionario = db.Questionario.Find(VM.IdQuestionario).qst_nome;

            foreach (var itemRU in db.RespostaUsuario)
            {
                bool ExisteQuestFiltro = false;
                // Verifica se o questionário já n foi adicionado.
                foreach (var itemQFiltro in QuestParaFiltro)
                    if (itemRU.Questionario.qst_nome == itemQFiltro.Text)
                        ExisteQuestFiltro = true;
                if (ExisteQuestFiltro)
                    continue;

                QuestParaFiltro.Add(new SelectListItem() { Text = itemRU.Questionario.qst_nome, Value = itemRU.Questionario.qst_id.ToString() });                
            }

            var ExibicaoParaFiltro = new List<SelectListItem>();
            ExibicaoParaFiltro.Add(new SelectListItem() { Text = "Detalhado", Value = "detalhado" });
            ExibicaoParaFiltro.Add(new SelectListItem() { Text = "Consolidado", Value = "consolidado" });

            ViewBag.Exibicao = new SelectList(ExibicaoParaFiltro, "Value", "Text");

            var ObjCOrg = db.Questionario.Where(x => x.qst_nome.Contains("Cultura Organizacional")).FirstOrDefault();
            int IdQCOrg = 0;
            if (ObjCOrg != null)
                IdQCOrg = ObjCOrg.qst_id;

            // Verifica o form. C. Org
            var ObjMedias = db.Media;
            if (ObjMedias.Count() > 0)
                QuestParaFiltro.Add(new SelectListItem() { Text = "Cultura Organizacional", Value = IdQCOrg.ToString() });

            ViewBag.IdQuestionario = new SelectList(QuestParaFiltro, "Value", "Text", VM.IdQuestionario);

            // Carrega as empresas para filtro
            var EmpresaParaFiltro = new List<SelectListItem>();

            foreach (var itemRU in db.RespostaUsuario)
            {
                bool ExisteEmpFiltro = false;
                // Verifica se o questionário já n foi adicionado.
                foreach (var itemEmpFiltro in EmpresaParaFiltro)
                    if (itemRU.Empresa.emp_nome == itemEmpFiltro.Text)
                        ExisteEmpFiltro = true;
                if (ExisteEmpFiltro)
                    continue;

                EmpresaParaFiltro.Add(new SelectListItem() { Text = itemRU.Empresa.emp_nome, Value = itemRU.Empresa.emp_id.ToString() });
            }

            ViewBag.IdEmpresa = new SelectList(EmpresaParaFiltro, "Value", "Text");
            ViewBag.IdDepartamento = new SelectList(db.Departamento, "dpt_id", "dpt_nome");
            #endregion

            
            if (!ModelState.IsValid)
                return View(VM);

            var Respostas = AdminService.ConsultarRespostasUsuarios((int)VM.IdEmpresa, (int)VM.IdQuestionario, (int)VM.IdUsuarioRespondente).ToList();
            VM.Respostas = Respostas;

            // tipo de exibição
            bool Consolidado = false;
            if (VM.Exibicao == "consolidado")
                Consolidado = true;

            if (VM.Questionario.Contains("Cultura Organizacional"))
                return View(ResultadoService.ConsolidarResultadoCOrg(VM, Consolidado));
            else
                return View(ResultadoService.ConsolidarResultadoBasico(VM, ModelState.IsValid));
        }

        public JsonResult ExcluirValorResposta(int Id)
        {
            try
            {
                RespostaService.ExcluirValorResposta(Id);

                return Json(new { Ok = "ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao excluir valor resposta: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                return Json(new { Error = msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msgError = "Erro ao add. valor resposta: ";
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    msgError += ex.InnerException.InnerException.Message;
                else
                    msgError += ex.Message;

                return Json(new { Error = msgError }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AdicionarValorResposta(int IdResposta, string Valor)
        {

            try
            {
                RespostaService.InserirValorResposta(IdResposta, Valor);

                return Json(new { Ok = "ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao adicionar valor resposta: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                return Json(new { Error = msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msgError = "Erro ao add. valor resposta: ";
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    msgError += ex.InnerException.InnerException.Message;
                else
                    msgError += ex.Message;

                return Json(new { Error = msgError }, JsonRequestBehavior.AllowGet);
            }
        }




        public JsonResult SalvarNivelAcesso(string Acesso, int IdUsuario)
        {
            try
            {
                UsuarioService.SalvarNivelAcesso(Acesso, IdUsuario);

                return Json(new { Ok = "ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao salvar o nivel de acesso: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                return Json(new { Error = msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msgError = "Erro ao salvar o nivel de acesso: ";
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    msgError += ex.InnerException.InnerException.Message;
                else
                    msgError += ex.Message;

                return Json(new { Error = msgError }, JsonRequestBehavior.AllowGet);
            }
        }
        #region PARTIALS
        public PartialViewResult _GridResultadosBasico(ResultadosVM VM)
        {
            return PartialView(VM);
        }

        public PartialViewResult _GridCOrgResultados(ResultadosVM VM)
        {
            return PartialView(VM);
        }


        public PartialViewResult _ComboDepartamento(int? Id)   // id empresa para filtrar a classificação
        {
            ViewBag.IdDepartamento = new SelectList(db.Departamento.Where(x => x.dpt_emp_id == Id).OrderBy(x => x.dpt_nome), "dpt_id", "dpt_nome");
            return PartialView();
        }
        public PartialViewResult _ComboQuestionario(string View, int? Id = null)
        {
            if (Id == null)
                ViewBag.IdQuestionario = new SelectList(CommonService.CarregarQuestionariosPublicados().Where(x => x.qst_nome != "Cultura Organizacional").OrderBy(x => x.qst_nome), "qst_id", "qst_nome");
            else
                ViewBag.IdQuestionario = new SelectList(CommonService.CarregarQuestionariosPublicados().Where(x => x.qst_nome != "Cultura Organizacional").OrderBy(x => x.qst_nome), "qst_id", "qst_nome", Id);
            ViewBag.View = View;
            return PartialView();
        }

        public PartialViewResult _ComboEmpresa()   // id empresa, caso exista, seleciona
        {
            ViewBag.IdEmpresa = new SelectList(db.Empresa.Where(x => x.emp_ativo).OrderBy(x => x.emp_nome), "emp_id", "emp_nome");
            return PartialView();
        }
        public PartialViewResult _ComboRespondente(int? IdQ, int? IdE)   // id questionario para filtro
        {
            var ObjUs = AdminService.ConsultarRespondentes(IdQ, IdE);
            ViewBag.IdUsuarioRespondente = new SelectList(ObjUs.OrderBy(x => x.usu_nome), "usu_id", "usu_nome");
            return PartialView();
        }
        public PartialViewResult _ComboRamo(string Ramo = null)
        {

            ViewBag.Ramo = CommonService.GerarComboRamo(Ramo);

            return PartialView();
        }
        public PartialViewResult _ComboEstado(string Estado = null)
        {
            ViewBag.Estado = CommonService.GerarComboEstado(Estado);
            return PartialView();
        }
        public PartialViewResult _ComboClassificacao(int? Id)   // id questionario para filtrar a classificação
        {

            ViewBag.IdClassificacao = new SelectList(db.Classificacao.Where(x => x.cls_qst_id == Id && x.cls_ativo).OrderBy(x => x.cls_nome), "cls_id", "cls_nome");
            return PartialView();
        }

        public PartialViewResult _ListarPermissaoFormularios(int Id = 0)
        {
            try
            {
                var ObjQs = AdminService.ConsultarQuestionariosPorEmpresa(Id);
                ViewBag.EQuestionarios = ObjQs;
                ViewBag.Error = "";

                return PartialView();
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao aprovar usuário: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                ViewBag.Error = msgError;
                return PartialView();
            }
            catch (Exception ex)
            {
                string msgError = "";

                if (ex.InnerException != null)
                    msgError = ex.InnerException.InnerException.Message;
                else
                    msgError = ex.Message;

                ViewBag.Error = msgError;
                return PartialView();
            }
        }

        public PartialViewResult _ListarUsuarios(UsuarioVM VM)
        {
            try
            {
                bool? Aprovado = null;
                if (VM.Aprovado == "1")
                    Aprovado = true;

                if (VM.Aprovado == "2")
                    Aprovado = false;
                var Usuarios = UsuarioService.ConsultarUsuario(VM.Nome, Aprovado);
                ViewBag.Usuarios = Usuarios.OrderBy(x => x.usu_nome).OrderByDescending(x => x.usu_dt_cadastro);
                ViewBag.Error = "";

                return PartialView();
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao aprovar usuário: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                ViewBag.Error = msgError;
                return PartialView();
            }
            catch (Exception ex)
            {
                string msgError = "";

                if (ex.InnerException != null)
                    msgError = ex.InnerException.InnerException.Message;
                else
                    msgError = ex.Message;

                ViewBag.Error = msgError;
                return PartialView();
            }
        }

        public PartialViewResult _ListarLeads(string TpLead)
        {
            try
            {
                var ObsLs = AdminService.ConsultarLeads(TpLead);
                ViewBag.Leads = ObsLs.OrderByDescending(x => x.data_cadastro);
                ViewBag.TpLead = TpLead == "treinamento" ? "DPO" : "";
                ViewBag.Error = "";

                return PartialView();
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao aprovar usuário: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                ViewBag.Error = msgError;
                return PartialView();
            }
            catch (Exception ex)
            {
                string msgError = "";

                if (ex.InnerException != null)
                    msgError = ex.InnerException.InnerException.Message;
                else
                    msgError = ex.Message;

                ViewBag.Error = msgError;
                return PartialView();
            }
        }
        public PartialViewResult _ListarValorResposta(int? Id)   // id resposta
        {
            var VM = Id == null ? new RespostaVM() : RespostaVM.ToViewModel(db.Resposta.Find(Id));
            return PartialView(VM);
        }
        public PartialViewResult _ListarClassificacao(int? Id)   // id questionario para filtrar a classificação
        {
            var Clss = AdminService.ConsultarClassificacoesAtivas((int)Id);
            ViewBag.Classificacoes = Clss.Count() > 0 ? Clss : null;
            return PartialView();
        }
        public PartialViewResult _ListarEmpresa()
        {
            ViewBag.Error = "";
            var Emps = db.Empresa.Where(x => x.emp_ativo);
            ViewBag.Empresas = Emps;
            return PartialView();
        }

        public PartialViewResult _ListarPerguntas(int? Id)   // id questionario para filtrar a classificação
        {
            var objPs = AdminService.ConsultarPerguntas((int)Id);
            ViewBag.Perguntas = objPs.Count() > 0 ? objPs : null;
            return PartialView();
        }
        public PartialViewResult _ListarRespostas(int? Id)   // id questionario para filtrar a classificação
        {
            var objRs = AdminService.ConsultarRespostasAtivas((int)Id);
            ViewBag.Respostas = objRs.Count() > 0 ? objRs : null;
            return PartialView();
        }
        public PartialViewResult _PageNavigation(string Nav)
        {
            var objU = Session["Usuario"] as Usuario;

            var arrNav = Nav.Split(';');
            List<string> lsNav = new List<string>();
            foreach (var i in arrNav)
            {
                lsNav.Add(i);
            }

            ViewBag.Nav = lsNav;

            ViewBag.Modulo = lsNav.LastOrDefault();

            return PartialView();
        }
        #endregion
    }
}