using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Common
{
    public class Email
    {       

        public bool EnviarEmailCancelamento(Solicitante Solicitante)
        {
            string from = "atendimento@corretorapremium.com.br";
            string nomeFrom = "Corretora Premium";
            string to = "comercial@corretorapremium.com.br";            
            string titulo = "Solicitação de Cancelamento";

            try
            {
                //carregando o template do email em html para string.
                string path = "~/templates/SolicitacaoEmail.html";
                path = HttpContext.Current.Server.MapPath(path);
                string email_body = System.IO.File.ReadAllText(path);

                email_body = email_body.Replace("@nomeCliente", Solicitante.Nome);
                email_body = email_body.Replace("@email", Solicitante.Email);
                email_body = email_body.Replace("@cpf", Solicitante.Cpf);
                email_body = email_body.Replace("@telefone", Solicitante.Telefone);
                email_body = email_body.Replace("@rg", Solicitante.Rg);
                email_body = email_body.Replace("@voucher", Solicitante.Voucher);

                EnviarEmail(from, nomeFrom, to, titulo, email_body);
                return true;
            }
            catch (Exception e)
            {
                throw e;
                return false;
            }

        }

        public bool EnviarEmailRecuperacaoSenha(Usuario Usuario)
        {
            string from = "atendimento@corretorapremium.com.br";
            string nomeFrom = "Corretora Premium";
            string to = Usuario.Email;
            string titulo = "Recuperação de Senha";

            //Redefinir a senha temporaria (token: cpf + 4 digitos randomicos)
            Random random = new Random();
            string numRand = random.Next(0, 9999).ToString("0000");
            string senhaTemp = Usuario.Cpf + numRand;
            //colocar uma máscara no token, para não ser logado diretamente, antes de passar pela confirmação da nova senha pelo usuário.
            string senhaTempMascara = senhaTemp + "1205";
            Usuario.AtualizaSenha(senhaTempMascara);
            string linkRedefinir = HttpContext.Current.Request.Url.AbsoluteUri;
            linkRedefinir += "?solicitRec=" + senhaTemp;
            linkRedefinir = linkRedefinir.Replace("/RecuperarSenhaAsync", "");
            try
            {
                //carregando o template do email em html para string.
                string path = "~/templates/RecuperacaoSenhaEmail.html";
                path = HttpContext.Current.Server.MapPath(path);
                string email_body = System.IO.File.ReadAllText(path);
                email_body = email_body.Replace("@nomeCliente", Usuario.Nome);
                email_body = email_body.Replace("@linkRedefinir", linkRedefinir);

                EnviarEmail(from, nomeFrom, to, titulo, email_body);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Houve um erro na tentativa de recuperar a senha: " + e.Message);
            }
        }

        private void EnviarEmail(string from, string nomeFrom, string to, string titulo, string body)
        {
            //Configuração para envio de e-mail
            SmtpClient mailClient = new SmtpClient("mail.proseg.com.br", 25);
            NetworkCredential cred = new NetworkCredential("dti", "Adm@Proseg!2015");
            mailClient.Credentials = cred;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(to);                
                mail.From = new MailAddress(from, nomeFrom, System.Text.Encoding.UTF8);
                mail.Subject = titulo;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = body;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;

                mailClient.Send(mail);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
