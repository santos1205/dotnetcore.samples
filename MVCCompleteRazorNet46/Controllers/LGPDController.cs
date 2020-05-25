using QuestionarioCOrg.DataAccess;
using QuestionarioCOrg.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
        public ActionResult Leads(lead_empresa_lgpd LEL)
        {
            var RamoFiltro = new List<SelectListItem>();

            ViewBag.Success = false;

            RamoFiltro.Add(new SelectListItem() { Text = "Alimentação", Value = "Financeiro" });
            RamoFiltro.Add(new SelectListItem() { Text = "Educação", Value = "Educacao" });
            RamoFiltro.Add(new SelectListItem() { Text = "Financeiro", Value = "Financeiro" });
            RamoFiltro.Add(new SelectListItem() { Text = "Marketing", Value = "Financeiro" });
            RamoFiltro.Add(new SelectListItem() { Text = "Seguros", Value = "Seguros" });
            RamoFiltro.Add(new SelectListItem() { Text = "Saúde", Value = "Saude" });
            RamoFiltro.Add(new SelectListItem() { Text = "Tecnologia da Informação", Value = "" });
            RamoFiltro.Add(new SelectListItem() { Text = "Turismo", Value = "Turismo" });
            RamoFiltro.Add(new SelectListItem() { Text = "Outros", Value = "Outros" });

            ViewBag.ramo = new SelectList(RamoFiltro, "Value", "Text");

            LEL.data_cadastro = DateTime.Now;
            LEL.telefone = Regex.Replace(LEL.telefone, @"[^0-9a-zA-Z]+", "");

            try
            {
                LEL.compartilha_dados = LEL.compartilha_dados ?? "";
                LEL.dados_cliente = LEL.dados_cliente ?? "";
                LEL.iniciou_adequacao = LEL.iniciou_adequacao ?? "";
                LEL.formulario = "lead";
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
            return RedirectToAction("Index", "Registro");
        }


        public ActionResult Leads()
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
    }
}