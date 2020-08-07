using Common;
using QuestionarioCOrg.DataAccess;
using QuestionarioCOrg.Service;
using QuestionarioCOrg.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace QuestionarioCOrg.Controllers
{
    public class LGPDController : Controller
    {

        private QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

        // GET: LGPD
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Leads(LeadLGPDVM LeadVM)
        {
            #region dummydata
            //LeadVM.Nome = "Mario Santos";
            //LeadVM.Email = "mariobrasilcanada@gmail.com";
            //LeadVM.Cargo = "Desenvolvedor";
            //LeadVM.Empresa = "Nike";
            //LeadVM.CNPJ = "04.211.559/0001-11";
            //LeadVM.CidadeEmpresa = "Brasília";
            //LeadVM.EstadoEmpresa = "DF";
            //LeadVM.Telefone = "(61) 999059055";
            //LeadVM.NrFuncionarios = 300;
            //LeadVM.Ramo = "Alimentação";
            #endregion

            if (!ModelState.IsValid)
            {
                ViewBag.Success = false;
                return View();
            }
            try
            {
                // Salva Lead
                LeadVM = LGPDService.SalvarLead(LeadVM);
                // Salva empresa do lead
                var EmpresaVM = new EmpresaVM 
                { 
                    Nome = LeadVM.Empresa,
                    CNPJ = LeadVM.CNPJ,
                    Cidade = LeadVM.CidadeEmpresa,
                    Estado = LeadVM.EstadoEmpresa,
                    Ramo = LeadVM.Ramo
                };
                EmpresaVM = LGPDService.SalvarEmpresa(EmpresaVM);
                LeadVM.IdEmpresa = EmpresaVM.Id;
                // Envia email
                string hashAcesso = EmailService.EnviarLGPDLead(LeadVM);
                // gerar e salvar hash de segurança
                LGPDService.SalvarLead(LeadVM, hashAcesso);

                ViewBag.Success = true;
                ViewBag.Email = LeadVM.Email;
            }
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao cadastrar empresa: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                ViewBag.Msg = msgError;                
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                if (ex.InnerException != null)
                    ViewBag.Msg = ex.InnerException.InnerException.Message;
                else
                    ViewBag.Msg = ex.Message;
            }                                    
            return View();
        }        

        [HttpPost]
        public JsonResult SalvarDeptosChat(int Id, string[] Departamentos, string Etapa)    // IdLead
        {
            try
            {
                LGPDService.SalvarDepartamentosChat(Id, Departamentos, int.Parse(Etapa));
            }
            catch (DbEntityValidationException e)
            {
                string msgError = e.Message;
                return Json(new { Error = "Erro: " + msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msgError = ex.Message;
                return Json(new { Error = "Erro: " + msgError }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Ok = "ok" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SalvarDadosChat(int Id, string Resposta, string Etapa)    // IdLead
        {
            try
            {
                LGPDService.SalvarDadosChat(Id, Resposta, int.Parse(Etapa));                
            }
            catch (DbEntityValidationException e)
            {
                string msgError = e.Message;
                return Json(new { Error = "Erro: " + msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msgError = ex.Message;
                return Json(new { Error = "Erro: " + msgError }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Ok = "ok" }, JsonRequestBehavior.AllowGet);            
        }

        [HttpGet]
        public ActionResult Questionario(string hash)
        {            
            try
            {
                // Validar hash no banco
                if (!LGPDService.VerificarAcessoLPGDLeads(hash))
                    throw new Exception();

                int IdLead = int.Parse(hash.Split('.')[1]);

                var objLead = LGPDService.ConsultarLead(IdLead);
                var VM = new LeadLGPDVM();
                if (objLead != null)
                {
                    VM = LeadLGPDVM.ToViewModel(objLead);
                }
                ViewBag.Visible = false;
                return View(VM);
            }            
            catch (DbEntityValidationException e)
            {
                string msgError = e.Message;
                return Json(new { Error = "Erro: " + msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                ViewBag.Visible = false;
                return View("Leads");
            }
        }

        [HttpGet]
        public JsonResult ConsultarDeptosLead(int Id)
        {
            try
            {
                var objDeptos = LGPDService.ConsultarDeptosLead(Id);
                var Deptos = new List<Object>();
                foreach (var dp in objDeptos)
                    Deptos.Add(new { nome = dp.dpt_nome });
                                

                return Json(new { Ok = "ok", Deptos }, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException e)
            {
                string msgError = e.Message;
                return Json(new { Error = "Erro: " + msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msgError = ex.Message;
                return Json(new { Error = "Erro: " + msgError }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Leads()
        {            
            ViewBag.Success = false;
            ViewBag.Visible = true;
            return View();
        }

        [HttpGet]
        public JsonResult DesativarHash(int Id)
        {
            try
            {
                LGPDService.DesativarHash(Id);

                return Json(new { Ok = "ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException e)
            {
                string msgError = e.Message;
                return Json(new { Error = "Erro: " + msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msgError = ex.Message;
                return Json(new { Error = "Erro: " + msgError }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult CarregarDadosEtapaLGPD(int Id)
        {
            try
            {
                // Carrega Etapa
                int Etapa = LGPDService.CarregarEtapa(Id);

                // Carrega se possui juridico
                var objDeptos = LGPDService.ConsultarDeptosLead(Id);                
                string PossuiJuridico = "não";
                foreach (var depto in objDeptos)
                    if (depto.dpt_nome.Equals("Jurídico"))
                    {
                        PossuiJuridico = "sim";
                        break;
                    }

                // Carrega dados Lead e sua empresa
                var objLead = LGPDService.ConsultarLead(Id);
                objLead.telefone = objLead.telefone != "" ? Formatacoes.FormatarTelefone(objLead.telefone)  : objLead.telefone;
                var objEmpresa = LGPDService.ConsultarEmpresaLead(Id);

                var Empresa = new Object();
                if (objEmpresa != null)
                {
                    Empresa = new
                    {
                        nome = objEmpresa.emp_nome,
                        CNPJ = Formatacoes.FormatarCNPJ(objEmpresa.emp_cnpj),
                        cidadeEmpresa = objEmpresa.emp_cidade,
                        estadoEmpresa = objEmpresa.emp_estado,
                    };
                }
                

                var Lead = objLead;
                Lead.lgpd_situacao_ti = Lead.lgpd_situacao_ti == "ambos" ? "própria e terceirizada" : Lead.lgpd_situacao_ti;
                Lead.lgpd_situacao_juridico = Lead.lgpd_situacao_juridico == "ambos" ? "própria e terceirizada" : Lead.lgpd_situacao_juridico;

                var Deptos = new List<Object>();
                foreach (var dp in objDeptos)
                    Deptos.Add(new { nome = dp.dpt_nome });


                return Json(new { Ok = "ok", Lead, Empresa, Deptos, Etapa, PossuiJuridico }, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException e)
            {
                string msgError = e.Message;
                return Json(new { Error = "Erro: " + msgError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msgError = ex.Message;
                return Json(new { Error = "Erro: " + msgError }, JsonRequestBehavior.AllowGet);
            }            
        }

        [HttpPost]
        public JsonResult FormEbook(LeadEbookVM VM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Capturando erros no ModelState.Isvalid
                    var errors = ModelState.Select(x => x.Value.Errors)
                               .Where(y => y.Count > 0)
                               .ToList();

                    return Json(new { Error = errors.FirstOrDefault().FirstOrDefault().ErrorMessage }, JsonRequestBehavior.AllowGet);
                }

                LGPDService.SalvarLead(VM);

                return Json(new { Return = "success" }, JsonRequestBehavior.AllowGet);
            }            
            catch (DbEntityValidationException e)
            {
                ViewBag.Success = false;

                string msgError = "Erro ao enviar ebook: ";
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
        public JsonResult FormContato(string name, string email, string telefone, string empresa, string subject, string message)
        {

            var Email = new Email();


            Email.Titulo = "Titulo - teste";
            Email.ema_remetente = "mariosantos1205@gmail.com";
            Email.ema_destinatario = "mariobrasilcanada@gmail.com";
            Email.ema_motivo_envio = "LGPD - Contato";
            Email.ema_remetente_alias = "Mario Santos";


            EmailService.EnviarLGPD(Email, name, email, telefone, empresa, subject, message);

            return Json(new { Success = "Success" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Dpo()
        {
            var RamoFiltro = new List<SelectListItem>();

            RamoFiltro.Add(new SelectListItem() { Text = "Alimentação", Value = "Financeiro" });
            RamoFiltro.Add(new SelectListItem() { Text = "Educação", Value = "Educacao" });
            RamoFiltro.Add(new SelectListItem() { Text = "Financeiro", Value = "Financeiro" });
            RamoFiltro.Add(new SelectListItem() { Text = "Marketing", Value = "Financeiro" });
            RamoFiltro.Add(new SelectListItem() { Text = "Seguros", Value = "Seguros" });
            RamoFiltro.Add(new SelectListItem() { Text = "Saúde", Value = "Saude" });
            RamoFiltro.Add(new SelectListItem() { Text = "Tecnologia da Informação", Value = "" });
            RamoFiltro.Add(new SelectListItem() { Text = "Turismo", Value = "Turismo" });
            RamoFiltro.Add(new SelectListItem() { Text = "Outros", Value = "Outros" });

            ViewBag.Ramo = new SelectList(RamoFiltro, "Value", "Text");
            ViewBag.Success = (bool?)TempData["Success"] ?? false;

            return View();
        }

        [HttpPost]
        public ActionResult Dpo(lead_empresa_lgpd LEL)
        {
            try
            {
                LEL.formulario = "treinamento";
                LEL.telefone = Regex.Replace(LEL.telefone, @"[^0-9a-zA-Z]+", "");
                LEL.data_cadastro = DateTime.Now;

                db.lead_empresa_lgpd.Add(LEL);
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string msgError = "Erro ao cadastrar o questionário: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ViewBag.Error = ex.InnerException.InnerException.Message;
                else
                    ViewBag.Error = ex.Message;
            }

            TempData["Success"] = true;
            return RedirectToAction("Dpo");
        }
        public PartialViewResult _ComboEstado(string Estado = null)
        {
            ViewBag.EstadoEmpresa = CommonService.GerarComboEstado(Estado);
            return PartialView();
        }
    }
}