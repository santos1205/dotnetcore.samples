using Common;
using QuestionarioCOrg.DataAccess;
using QuestionarioCOrg.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;

namespace QuestionarioCOrg.Service
{
    public class EmailService
    {

        public static void EnviarCompartilhamentoFormularios(ICollection<CompartilhamentoVM> VM)
        {
            var db = new QuestionarioOrgDBEntities();

            var Email = new Email();
            var DbEmail = db.Email.Where(x => x.ema_remetente.Contains("gmail")).FirstOrDefault();
            
            if (DbEmail == null)
                throw new System.Exception("Base de e-mails desconfigurada. Registro não encontrado.");

            if (HttpContext.Current.Request.Url.Host.Contains("localhost"))
            {
                Email.Titulo = "Formulário - Smart Forms";
                Email.ema_remetente = "mariosantos1205@gmail.com";
                Email.ema_destinatario = VM.FirstOrDefault().Emails;
                Email.ema_motivo_envio = "Formulário";
                Email.ema_remetente_alias = "Smart Forms";
            }
            else
            {
                Email.Titulo = "Formulário - Smart Forms";
                Email.ema_remetente = DbEmail.ema_remetente;
                Email.ema_destinatario = VM.FirstOrDefault().Emails;
                Email.ema_motivo_envio = DbEmail.ema_motivo_envio;
                Email.ema_remetente_alias = DbEmail.ema_remetente_alias;
            }
            

            //carregando o template do email em html para string.
            string path = "~/EmailTemplates/CompartilharFormulario.html";
            path = HttpContext.Current.Server.MapPath(path);
            string email_body = System.IO.File.ReadAllText(path);
            string LinkForm = string.Empty;

            string host = HttpContext.Current.Request.Url.Host;

            LinkForm = "<ul>";
            foreach (var C in VM)
            {
                LinkForm += "<li>";
                if (host.Contains("localhost"))
                    LinkForm += string.Format("http://{0}:{1}/Registro?RediQ={2}", HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port, C.IdFormulario);
                else
                    LinkForm += string.Format("http://{0}/Registro?RediQ={1}", HttpContext.Current.Request.Url.Host, C.IdFormulario);
                LinkForm += "</li>";
            }

            LinkForm += "</ul>";
            email_body = email_body.Replace("@linkForm", LinkForm);            
            Email.Corpo = email_body;


            if (HttpContext.Current.Request.Url.Host.Contains("localhost"))
                //Enviar(Email, "Proseg");
                Enviar(Email);
            else
                Enviar(Email);
        }
        public static void EnviarLGPD(Email Email, string Name, string StrEmail, string Telefone, string Empresa, string Assunto, string Msg)
        {
            //carregando o template do email em html para string.
            string path = "~/EmailTemplates/ContatoEmail.html";
            path = HttpContext.Current.Server.MapPath(path);
            string email_body = System.IO.File.ReadAllText(path);
            email_body = email_body.Replace("@nome", Name);
            email_body = email_body.Replace("@email", StrEmail);
            email_body = email_body.Replace("@telefone", Telefone);
            email_body = email_body.Replace("@empresa", Empresa);
            email_body = email_body.Replace("@assunto", Assunto);
            email_body = email_body.Replace("@msg", Msg);

            Email.Corpo = email_body;

            //Enviar(Email);              
            Enviar(Email, "Proseg");
        }

        public static string EnviarLGPDLead(LeadLGPDVM VM)
        {
            // Enviar link de acesso ao formulário de avaliação LGPD
            var Email = new Email();

            // Salvar senha temporária (hash)
            //throw new Exception("erro testes");
            Email.Titulo = "Formulário de Avaliação LGPD";
            Email.ema_remetente = "proseg.ga@gmail.com";
            Email.ema_destinatario = VM.Email;
            Email.ema_motivo_envio = "Avaliação LGPD";
            Email.ema_remetente_alias = "LGPD";

            //carregando o template do email em html para string.
            string path = "~/EmailTemplates/EmailLGPDLead/index.html";
            path = HttpContext.Current.Server.MapPath(path);
            string email_body = System.IO.File.ReadAllText(path);

            // Link para redefinição            
            string linkRedefinir = HttpContext.Current.Request.Url.AbsoluteUri;
            linkRedefinir = linkRedefinir.Replace("Leads", "questionario");
            linkRedefinir = linkRedefinir.Replace("leads", "questionario");

            // hash de acesso            
            string hshAcesso = string.Format("{0}{1}{2}{3}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            hshAcesso = string.Format("{0}.{1}", Cripto.GerarHash32(hshAcesso), VM.Id);
            linkRedefinir += "?hash=" + hshAcesso;             
            // variáveis do template
            email_body = email_body.Replace("@lkform", linkRedefinir);
            email_body = email_body.Replace("@empresa", VM.Empresa);
            string dataExtenso = string.Format("{0} de {1} de {2}", DateTime.Now.Day, new DateTime(1900, DateTime.Now.Month, 1).ToString("MMMM", new CultureInfo("pt-BR")), DateTime.Now.Year);
            email_body = email_body.Replace("@data", dataExtenso);

            // Create the HTML view
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
                                                         email_body,
                                                         Encoding.UTF8,
                                                         MediaTypeNames.Text.Html);

            string mediaType = MediaTypeNames.Image.Jpeg;
            path = "~/EmailTemplates/EmailLGPDLead/images/bg_1.jpg";            
            path = HttpContext.Current.Server.MapPath(path);
            LinkedResource img = new LinkedResource(path, mediaType);
            // Make sure you set all these values!!!
            img.ContentId = "EmbeddedContent_1";
            img.ContentType.MediaType = mediaType;
            img.TransferEncoding = TransferEncoding.Base64;
            img.ContentType.Name = img.ContentId;
            img.ContentLink = new Uri("cid:" + img.ContentId);
            htmlView.LinkedResources.Add(img);


            path = "~/EmailTemplates/EmailLGPDLead/images/person_1.jpg";
            path = HttpContext.Current.Server.MapPath(path);
            img = new LinkedResource(path, mediaType);
            // Make sure you set all these values!!!
            img.ContentId = "EmbeddedContent_2";
            img.ContentType.MediaType = mediaType;
            img.TransferEncoding = TransferEncoding.Base64;
            img.ContentType.Name = img.ContentId;
            img.ContentLink = new Uri("cid:" + img.ContentId);
            htmlView.LinkedResources.Add(img);

            MailMessage msg = new MailMessage();

            msg.AlternateViews.Add(htmlView);
            msg.IsBodyHtml = true;

            Email.Corpo = email_body;

            Enviar(Email, msg, "Gmail");
            // Retorna hash para ser armazenado no banco
            return hshAcesso;
        }
        public static void RedefinicaoSenha(Usuario ObjU)
        {
            // Enviar link de solicitação de redefinição para o email do usuário
            var Email = new Email();

            // Salvar senha temporária (hash)
            ObjU = LoginService.SalvarSenhaTemporaria(ObjU.usu_id);


            Email.Titulo = "Titulo - teste";
            Email.ema_remetente = "mariosantos1205@gmail.com";
            Email.ema_destinatario = ObjU.usu_email;
            Email.ema_motivo_envio = "Redefinição de Senha";
            Email.ema_remetente_alias = "Mario Santos";
            
            //carregando o template do email em html para string.
            string path = "~/EmailTemplates/RecuperacaoSenhaEmail.html";
            path = HttpContext.Current.Server.MapPath(path);
            string email_body = System.IO.File.ReadAllText(path);

            // Link para redefinição
            string linkRedefinir = HttpContext.Current.Request.Url.AbsoluteUri;
            
            string senhaTemp = ObjU.usu_senha;
            // Senha sem a private key 1205
            senhaTemp = ObjU.usu_senha.Substring(0, senhaTemp.Length - 4);
            linkRedefinir += "?solicitRec=" + senhaTemp;
            

            email_body = email_body.Replace("@nomeCliente", ObjU.usu_nome);
            email_body = email_body.Replace("@linkRedefinir", linkRedefinir);

            Email.Corpo = email_body;

            Enviar(Email);                          
        }
        // refs: "http://www.macoratti.net/18/04/aspcoremvc_email1.htm"
        private static void Enviar(Email Email, string SMTP = "Gmail")
        {
            var db = new QuestionarioOrgDBEntities();

            IEnumerable<EmailConfiguracao> EmailConfig = db.EmailConfiguracao;

            switch(SMTP)
            {
                case "Gmail":
                    EmailConfig = EmailConfig.Where(x => x.eml_smtp_cliente.Contains("gmail"));
                    break;
                case "Proseg":
                    EmailConfig = EmailConfig.Where(x => x.eml_smtp_cliente.Contains("proseg"));
                    break;
                default:
                    break;
            }
            var EConfig = EmailConfig != null ? EmailConfig.FirstOrDefault() : null;

            if (EConfig != null)
            {


               


                //Configuração para envio de e-mail
                SmtpClient mailClient = new SmtpClient(EConfig.eml_smtp_cliente, EConfig.eml_smtp_cliente_porta);
                mailClient.UseDefaultCredentials = false;
                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailClient.EnableSsl = true;
                NetworkCredential cred = new NetworkCredential(EConfig.eml_smtp_usuario, EConfig.eml_smtp_senha);                
                mailClient.Credentials = cred;

                MailMessage mail = new MailMessage();
                string[] arrTo = Email.ema_destinatario.Split(';');
                foreach (var email in arrTo)                
                    if (!string.IsNullOrEmpty(email))
                        mail.To.Add(email);

                mail.From = new MailAddress(Email.ema_remetente, Email.ema_remetente_alias, System.Text.Encoding.UTF8);
                mail.Subject = Email.Titulo;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = Email.Corpo;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = Email.IsHtml ?? true;
                
                mailClient.Send(mail);
            }
        }
        private static void Enviar(Email ObjEmail, MailMessage mail, string SMTP)
        {
            var db = new QuestionarioOrgDBEntities();

            IEnumerable<EmailConfiguracao> EmailConfig = db.EmailConfiguracao;

            switch (SMTP)
            {
                case "Gmail":
                    EmailConfig = EmailConfig.Where(x => x.eml_smtp_cliente.Contains("gmail"));
                    break;
                case "Proseg":
                    EmailConfig = EmailConfig.Where(x => x.eml_smtp_cliente.Contains("proseg"));
                    break;
                default:
                    break;
            }
            var EConfig = EmailConfig != null ? EmailConfig.FirstOrDefault() : null;

            if (EConfig != null)
            {

                //Configuração para envio de e-mail
                SmtpClient mailClient = new SmtpClient(EConfig.eml_smtp_cliente, EConfig.eml_smtp_cliente_porta);
                mailClient.UseDefaultCredentials = false;
                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailClient.EnableSsl = true;
                NetworkCredential cred = new NetworkCredential(EConfig.eml_smtp_usuario, EConfig.eml_smtp_senha);
                mailClient.Credentials = cred;

                
                string[] arrTo = ObjEmail.ema_destinatario.Split(';');
                foreach (var email in arrTo)
                    if (!string.IsNullOrEmpty(email))
                        mail.To.Add(email);

                mail.From = new MailAddress(ObjEmail.ema_remetente, ObjEmail.ema_remetente_alias, Encoding.UTF8);
                mail.Subject = ObjEmail.Titulo;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Body = ObjEmail.Corpo;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = ObjEmail.IsHtml ?? true;

                mailClient.Send(mail);
            }
        }
    }
}