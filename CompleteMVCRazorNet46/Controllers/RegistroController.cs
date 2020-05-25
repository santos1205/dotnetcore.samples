using QuestionarioCOrg.Service;
using QuestionarioCOrg.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace QuestionarioCOrg.Controllers
{
    public class RegistroController : Controller
    {
        // GET: Registro
        public ActionResult Index(int? RediQ = null)
        {
            // Recebe o id do questionário para redirecionamento automático (link do email);
            ViewBag.RediQ = RediQ ?? 0;
            return View();
        }

        [HttpPost]
        public JsonResult Index(UsuarioVM VMU, EmpresaVM VME)
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
                return Json(new { Error = errors.FirstOrDefault().FirstOrDefault().ErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            // Se usuário não concordou com as políticas de consentimento, não cadastra
            if (!VMU.Consentimento)
            {
                ViewBag.Success = false;
                return Json(new { Error = "Consentimento obrigatório." }, JsonRequestBehavior.AllowGet);
            }

            // Validações CPF e CNPJ
            if (!CommonService.CpfValido(VMU.CPF))
            {
                ViewBag.Success = false;
                return Json(new { Error = "CPF inválido." }, JsonRequestBehavior.AllowGet);
            }
            if (!CommonService.CnpjValido(VME.CNPJ))
            {
                ViewBag.Success = false;
                return Json(new { Error = "CNPJ inválido." }, JsonRequestBehavior.AllowGet);
            }

            // Verifica a confirmação da senha
            if (VMU.Senha != VMU.SenhaConfirmacao)
            {
                ViewBag.Success = false;
                return Json(new { Error = "Erro na confirmação de senha. Senhas não conferem." }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                // Se registro novo, verifica unicidade por cpf.
                if (VMU.Id == 0)
                {
                    // Verifica se cpf já existe, se sim, n cadastra
                    if (AdminService.ConsultarUsuarioPorCPF(VMU.CPF) != null)
                    {
                        ViewBag.Success = false;
                        return Json(new { Error = "CPF já existe no sistema." }, JsonRequestBehavior.AllowGet);
                    }
                    // Verifica se email já existe, se sim, n cadastra
                    if (AdminService.ConsultarUsuarioPorEmail(VMU.Email) != null)
                    {
                        ViewBag.Success = false;
                        return Json(new { Error = "Email já existe no sistema." }, JsonRequestBehavior.AllowGet);
                    }                    
                }

                // Se registro novo, verifica unicidade por cnpj.
                if (VME.Id == 0)
                {
                    // Verifica se cnpj já existe, se sim, n cadastra
                    if (AdminService.ConsultarEmpresaPorCnpj(VME.CNPJ) != null)
                    {
                        ViewBag.Success = false;
                        return Json(new { Error = "CNPJ já existe no sistema." }, JsonRequestBehavior.AllowGet);
                    }
                }

                AdminService.SalvarRegistroUsuarioEmpresa(VME, VMU);
                
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao cadastrar usuário: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                ViewBag.Success = false;
                return Json(new { Error = msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                if (ex.InnerException != null)
                {
                    ViewBag.Success = false;
                    return Json(new { Error = ex.InnerException.InnerException.Message }, JsonRequestBehavior.AllowGet);
                }                    
                else
                {
                    ViewBag.Success = false;
                    return Json(new { Error = ex.Message }, JsonRequestBehavior.AllowGet);
                }                    
            }

            ViewBag.Success = true;
            ViewBag.Error = "";
            return Json(new { Success = "Success" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerificarUnicidadeUsuarioPorCpf(string CPF)
        {
            try
            {
                var ObjU = AdminService.ConsultarUsuarioPorCPF(CPF);
                if (ObjU != null)
                    throw new Exception("CPF já cadastrado no sistema.");
            }
            catch (DbEntityValidationException e)
            {
                string msgError = "Erro: ";
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

        public JsonResult ValidarEmail(string Email)
        {
            // Verifica se email é válido
            if (!CommonService.ValidarEmail(Email))
            {
                ViewBag.Success = false;
                return Json(new { Error = "Email inválido." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Success = "Success" }, JsonRequestBehavior.AllowGet);
        }


#region PARTIALS
public PartialViewResult _ComboRamo(string Ramo = null)
        {

            // Carrega as empresas para filtro
            var OptRamoList = new List<SelectListItem>();
            
            OptRamoList.Add(new SelectListItem() { Text = "Alimentação", Value = "Alimentação" });
            OptRamoList.Add(new SelectListItem() { Text = "Educação", Value = "Educação" });
            OptRamoList.Add(new SelectListItem() { Text = "Financeiro", Value = "Financeiro" });
            OptRamoList.Add(new SelectListItem() { Text = "Marketing", Value = "Marketing" });
            OptRamoList.Add(new SelectListItem() { Text = "Seguros", Value = "Seguros" });
            OptRamoList.Add(new SelectListItem() { Text = "Saude", Value = "Saude" });
            OptRamoList.Add(new SelectListItem() { Text = "Tecnologia", Value = "Tecnologia" });
            OptRamoList.Add(new SelectListItem() { Text = "Turismo", Value = "Turismo" });
            OptRamoList.Add(new SelectListItem() { Text = "Outros", Value = "Outros" });

            if (!string.IsNullOrEmpty(Ramo))
                ViewBag.Ramo = new SelectList(OptRamoList, "Value", "Text", Ramo);
            else
                ViewBag.Ramo = new SelectList(OptRamoList, "Value", "Text");

            return PartialView();
        }
        public PartialViewResult _ComboEstado(string Estado = null)
        {
            ViewBag.Estado = CommonService.GerarComboEstado(Estado);
            
            return PartialView();
        }
        #endregion        
    }
}