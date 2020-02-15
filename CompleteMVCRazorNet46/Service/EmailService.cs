using QuestionarioCOrg.DataAccess;
using QuestionarioCOrg.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
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

            Email.Titulo = "Formulário | Opportuna - Smart Forms";
            Email.ema_remetente = DbEmail.ema_remetente;
            Email.ema_destinatario = VM.FirstOrDefault().Emails;
            Email.ema_motivo_envio = DbEmail.ema_motivo_envio;
            Email.ema_remetente_alias = DbEmail.ema_remetente_alias;
            

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
                    LinkForm += string.Format(@"<a href=""http://{0}:{1}/Registro?RediQ={2}"">http://{0}:{1}/Registro?RediQ={2}</a>", HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port, C.IdFormulario);
                else
                    LinkForm += string.Format(@"<a href=""http://{0}/Registro?RediQ={1}"">http://{0}/Registro?RediQ={1}</a>", HttpContext.Current.Request.Url.Host, C.IdFormulario);
                LinkForm += "</li>";
            }

            LinkForm += "</ul>";
            email_body = email_body.Replace("@linkForm", LinkForm);            
            Email.Corpo = email_body;

            // Enviar mensagem junto c email
            AlternateView view = AlternateView.CreateAlternateViewFromString(email_body, null, MediaTypeNames.Text.Html);

            LinkedResource resource = new LinkedResource(HttpContext.Current.Server.MapPath("/assets/img/logo_opportuna_tecnologia.png"));
            resource.ContentId = "Imagem1";
            view.LinkedResources.Add(resource);

            Enviar(Email, Resource: resource);
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

            Enviar(Email);                          
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
        // refs: "https://israelaece.com/2005/10/16/embutindo-imagens-no-envio-de-emails/"
        private static void Enviar(Email Email, string SMTP = "Gmail", LinkedResource Resource = null)
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
                // Se tiver imagem, envia p anexo, para ser carregada nas caixas de entradas
                if (Resource != null)
                {
                    AlternateView view = AlternateView.CreateAlternateViewFromString(Email.Corpo, null, MediaTypeNames.Text.Html);
                    view.LinkedResources.Add(Resource);
                    mail.AlternateViews.Add(view);
                    mail.IsBodyHtml = true;
                }
                

                mailClient.Send(mail);
            }
        }
    }
}