using QuestionarioCOrg.DataAccess;
using QuestionarioCOrg.Service;
using QuestionarioCOrg.ViewModels;
using System;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace QuestionarioCOrg.Controllers
{
    public class LoginController : Controller
    {

        private QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

        // GET: Login
        [HttpGet, Route("~/Login/AcessoRestrito")]
        public ActionResult Index(int? RediQ = null)
        {
            #region ViewBags
            ViewBag.Error = "";
            ViewBag.Success = (bool?)TempData["Success"] ?? false;
            #endregion

            // Caso tenha redi. automatico de formulário, a VM recebe o id do questionário
            if (RediQ != null)
            {
                var VM = new LoginVM();
                VM.RedirectQ = RediQ;
                return View(VM);
            }

            // Caso esteja logado, redireciona para tela de cadastro de questionário
            var objU = Session["Usuario"] as Usuario;
            if (objU != null)
                return RedirectToAction("Questionario", "Admin");

            return View();
        }
        [HttpPost]
        public ActionResult SalvarSenha(UsuarioVM VM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Success = false;
                return View(VM);
            }
            // Verifica se a confirmação de senha é valida.
            if (VM.Senha != VM.SenhaConfirmacao)
            {
                TempData["Success"] = false;
                TempData["Error"] = "Confirmação de senha inválida - senhas divergentes";
                var ObjU = db.Usuario.Find(VM.Id);
                string senhaTemp = ObjU.usu_senha.Substring(0, 15);
                return RedirectToAction("RedefinicaoSenha", "Login", new { solicitRec = senhaTemp });                
            }
            
            try
            {                
                AdminService.SalvarUsuario(VM);
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao salvar senha: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                TempData["Error"] = msgError;
                TempData["Success"] = false;
                // Obter senha temporária para retorno da tela de confirmação
                var ObjU = db.Usuario.Find(VM.Id);
                string senhaTemp = ObjU.usu_senha.Substring(0, 11);
                return RedirectToAction("RedefinicaoSenha", "Login", new { solicitRec = senhaTemp });
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    ViewBag.Error = ex.InnerException.InnerException.Message;
                else
                    ViewBag.Error = ex.Message;

                ViewBag.Success = false;
                var ObjU = db.Usuario.Find(VM.Id);
                string senhaTemp = ObjU.usu_senha.Substring(0, 11);
                return RedirectToAction("RedefinicaoSenha", "Login", new { solicitRec = senhaTemp });
            }

            TempData["Success"] = true;
            return RedirectToAction("Index", "Login");
        }

        public ActionResult RedefinicaoSenha(string solicitRec = null)
        {
            #region ViewBags
            ViewBag.Error = TempData["Error"] ?? "";
            ViewBag.Success = (bool?)TempData["Success"] ?? false;
            ViewBag.Solicitacao = solicitRec ?? "";
            #endregion

            // Caso exista o parametro hash (solicitRec), valida
            if (!LoginService.ValidarSolicitacaoSenha(solicitRec))
                ViewBag.Solicitacao = "";
            else
            {
                string strCpf = string.Empty;
                if (!string.IsNullOrEmpty(solicitRec))
                {
                    strCpf = solicitRec.Substring(0, 11);
                    // Carregar o usuário, no caso de zerar a senha
                    var VM = UsuarioVM.ToViewModel(AdminService.ConsultarUsuarioPorCPF(strCpf));
                    VM.Senha = string.Empty;
                    VM.SenhaConfirmacao = string.Empty;
                    return View(VM);
                }
            }            

            return View();
        }
        [HttpPost]
        public ActionResult RedefinicaoSenha(UsuarioVM VM)
        {
            try
            {                
                // Caso email não esteja cadastrado, critica
                var ObjU = AdminService.ConsultarUsuarioPorEmail(VM.Email);

                if (ObjU == null)
                {
                    ViewBag.Success = false;
                    ViewBag.Error = "Email não cadastrado.";                    
                    return View("RedefinicaoSenha", VM);
                }                

                EmailService.RedefinicaoSenha(ObjU);
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
            return RedirectToAction("RedefinicaoSenha", "Login");
        }
                
        [HttpPost, Route("~/Login/AcessoRestrito")]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginVM VM)
        {
            ViewBag.Error = "";
            ViewBag.Success = TempData["Success"] ?? false;
            
            try
            {
                if (!ModelState.IsValid)
                    return View();

                var objU = LoginService.VerificarLogin(VM);
                if (objU == null)
                    throw new Exception("Login ou Senha inválidos.");

                Session.Contents["Usuario"] = objU;
                // Caso tenha redirecionamento automatico para formulário.
                if (VM.RedirectQ != null)
                    return RedirectToAction("Index", "Formulario", new { Id = VM.RedirectQ });
            }
            catch(Exception ex)
            {                
                if (ex.InnerException != null)
                    ViewBag.Error = ex.InnerException.InnerException.Message;
                else
                    ViewBag.Error = ex.Message;

                return View();
            }

            return RedirectToAction("Painel", "Formulario");
        }        
    }
}