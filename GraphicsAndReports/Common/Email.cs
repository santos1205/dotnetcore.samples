using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace Common
{
    public class Email
    {
        public bool EnviarEmailCadastro(string NomeUsuario, string email)
        {
            string from = "sistema@sindicatomedico.com.br";
            string nomeFrom = "Sindicato Médico";
            string to = email;
            string titulo = "Cadastro de Acesso";

            try
            {
                //carregando o template do usr_email em html para string.
                string path = "~/templates/CadastroEmail.html";
                path = HttpContext.Current.Server.MapPath(path);
                string Email_body = System.IO.File.ReadAllText(path);
                Email_body = Email_body.Replace("@nomeCliente", NomeUsuario);

                EnviarEmail(from, nomeFrom, to, titulo, Email_body);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool EnviarEmailReprovacaoUsuario(string NomeUsuario, string email)
        {
            string from = "sistema@sindicatomedico.com.br";
            string usr_nomeFrom = "Sindicato Médico";
            string to = email;
            string titulo = "Retorno do cadastro de acesso";

            try
            {
                //carregando o template do usr_email em html para string.
                string path = "~/templates/ReprovacaoUsuario.html";
                path = HttpContext.Current.Server.MapPath(path);
                string Email_body = System.IO.File.ReadAllText(path);
                Email_body = Email_body.Replace("@nomeCliente", NomeUsuario);

                EnviarEmail(from, usr_nomeFrom, to, titulo, Email_body);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool EnviarAprovacaoUsuario(string NomeUsuario, string cpf, string senha, string email)
        {
            string from = "sistema@sindicatomedico.com.br";
            string usr_nomeFrom = "Sindicato Médico";
            string to = email;
            string titulo = "Aprovação de Acesso";
            string link = "";

            string host = HttpContext.Current.Request.Url.Host;

            if (host.Equals("des.sindicatomedico.corretorapremium.com.br") || host.Contains("localhost"))
                link = "http://des.sindicatomedico.corretorapremium.com.br/Usuario/Login.aspx";
            if (host.Equals("hes.sindicatomedico.corretorapremium.com.br"))
                link = "http://hes.sindicatomedico.corretorapremium.com.br/Usuario/Login.aspx";
            if (host.Equals("sindicatomedico.corretorapremium.com.br"))
                link = "http://sindicatomedico.corretorapremium.com.br/Usuario/Login.aspx";

            try
            {
                //carregando o template do usr_email em html para string.
                string path = "~/templates/AprovacaoUsuario.html";
                path = HttpContext.Current.Server.MapPath(path);
                string Email_body = System.IO.File.ReadAllText(path);
                Email_body = Email_body.Replace("@nomeCliente", NomeUsuario);
                Email_body = Email_body.Replace("@usuario", cpf);
                Email_body = Email_body.Replace("@senha", senha);
                Email_body = Email_body.Replace("@link", link);

                EnviarEmail(from, usr_nomeFrom, to, titulo, Email_body);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool EnviarEmailErro(string NomeUsuario, string str_exception)
        {
            string from = "sistema@sindicatomedico.com.br";
            string usr_nomeFrom = "Sindicato Médico";
            string to = "mario.santos@proseg.com.br";
            string titulo = "Erro no Cálculo";
            string host = HttpContext.Current.Request.Url.Host;
            string ambiente = "";

            if (host.Equals("des.sindicatomedico.corretorapremium.com.br"))
                ambiente = "Desenvolvimento";
            if (host.Equals("hes.sindicatomedico.corretorapremium.com.br"))
                ambiente = "Homologação";
            if (host.Equals("sindicatomedico.corretorapremium.com.br"))
                ambiente = "Produção";

            titulo = titulo + " - " + ambiente;

            try
            {
                //carregando o template do usr_email em html para string.
                string path = "~/templates/ErroCalculoEmail.html";
                path = HttpContext.Current.Server.MapPath(path);
                string Email_body = System.IO.File.ReadAllText(path);
                Email_body = Email_body.Replace("@nomeCliente", NomeUsuario);
                Email_body = Email_body.Replace("@exception", str_exception);

                EnviarEmail(from, usr_nomeFrom, to, titulo, Email_body);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool EnviarEmailErro(string NomeUsuario, string str_exception, string tituloEmail)
        {
            string from = "sistema@seguroenergiasolar.com.br";
            string usr_nomeFrom = "Seguro Energia Solar";
            string to = "mario.santos@proseg.com.br";
            string titulo = tituloEmail;
            string host = HttpContext.Current.Request.Url.Host;
            string ambiente = "";

            if (host.Equals("des.seguroenergiasolar.com.br"))
                ambiente = "Desenvolvimento";
            if (host.Equals("hes.seguroenergiasolar.com.br"))
                ambiente = "Homologação";
            if (host.Equals("www.seguroenergiasolar.com.br"))
                ambiente = "Produção";

            titulo = titulo + " - " + ambiente;

            try
            {
                //carregando o template do usr_email em html para string.
                string path = "~/templates/ErroCalculoEmail.html";
                path = HttpContext.Current.Server.MapPath(path);
                string Email_body = System.IO.File.ReadAllText(path);
                Email_body = Email_body.Replace("@nomeCliente", NomeUsuario);
                Email_body = Email_body.Replace("@exception", str_exception);

                EnviarEmail(from, usr_nomeFrom, to, titulo, Email_body);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void EnviarRecuperarSenha(string NomeUsuario, string senha, string email, string linkRedefinicaoSenha)
        {
            string from = "sistema@sindicatomedico.com.br";
            string usr_nomeFrom = "Sindicato Médico";
            string to = email;
            string titulo = "Recuperação de senha";

            //var UService = new UsuarioService();

            //Redefinir a usr_senha temporaria (token: usr_cpf + 4 digitos randomicos)
            try
            {                
                //Random random = new Random();
                //string numRand = random.Next(0, 9999).ToString("0000");
                //string senhaTemp = Usuario.usr_cpf + numRand;
                ////colocar uma máscara no token, para não ser logado diretamente, antes de passar pela confirmação da nova usr_senha pelo usuário.
                //string senhaTempMascara = senhaTemp + "1205";
                //UService.AtualizaSenha(Usuario.usr_cpf, senhaTempMascara);
                //string linkRedefinir = HttpContext.Current.Request.Url.AbsoluteUri;
                //linkRedefinir += "?solicitRec=" + senhaTemp;
                //linkRedefinir = linkRedefinir.Replace("/RecuperarSenhaAsync", "");

                //carregando o template do Email em html para string.
                string path = "~/templates/RecuperacaoSenhaEmail.html";
                path = HttpContext.Current.Server.MapPath(path);
                string Email_body = System.IO.File.ReadAllText(path);
                Email_body = Email_body.Replace("@nomeCliente", NomeUsuario);
                Email_body = Email_body.Replace("@linkRedefinir", linkRedefinicaoSenha);

                EnviarEmail(from, usr_nomeFrom, to, titulo, Email_body);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region Emails Comments
        //Métodos encapsulados de usr_email.
        //public bool Enviarusr_emailAprovacaoUsuario(Usuario Usuario)
        //{
        //    string from = "sistema@seguroenergiasolar.com.br";
        //    string usr_nomeFrom = "Seguro Energia Solar";
        //    string to = Usuario.usr_email;            
        //    string titulo = "Aprovação de Acesso";
        //    string link = "";

        //    string host = HttpContext.Current.Request.Url.Host;            

        //    if (host.Equals("des.seguroenergiasolar.com.br"))
        //        link = "http://des.seguroenergiasolar.com.br/Administracao/ManterUsuario.aspx";                
        //    if (host.Equals("hes.seguroenergiasolar.com.br"))
        //        link = "http://hes.seguroenergiasolar.com.br/Administracao/ManterUsuario.aspx";
        //    if (host.Equals("seguroenergiasolar.com.br"))
        //        link = "http://seguroenergiasolar.com.br/Administracao/ManterUsuario.aspx";

        //    try
        //    {
        //        //carregando o template do usr_email em html para string.
        //        string path = "~/templates/AprovacaoUsuario.html";
        //        path = HttpContext.Current.Server.MapPath(path);
        //        string usr_email_body = System.IO.File.ReadAllText(path);
        //        usr_email_body = usr_email_body.Replace("@nomeCliente", Usuario.usr_nome);
        //        usr_email_body = usr_email_body.Replace("@usuario", Usuario.Login);
        //        usr_email_body = usr_email_body.Replace("@usr_senha", Usuario.usr_senha);
        //        usr_email_body = usr_email_body.Replace("@link", link);

        //        Enviarusr_email(from, usr_nomeFrom, to, titulo, usr_email_body);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }

        //}

        //public bool EnviarEmailReprovacaoUsuario(Usuario Usuario)
        //{
        //    string from = "sistema@seguroenergiasolar.com.br";
        //    string usr_nomeFrom = "Seguro Energia Solar";
        //    string to = Usuario.usr_email;
        //    string titulo = "Retorno do cadastro de acesso";

        //    try
        //    {
        //        //carregando o template do usr_email em html para string.
        //        string path = "~/templates/ReprovacaoUsuario.html";
        //        path = HttpContext.Current.Server.MapPath(path);
        //        string Email_body = System.IO.File.ReadAllText(path);
        //        Email_body = Email_body.Replace("@nomeCliente", Usuario.usr_nome);

        //        EnviarEmail(from, usr_nomeFrom, to, titulo, Email_body);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }

        //}


        #endregion


        private void EnviarEmail(string from, string nomeFrom, string to, string titulo, string body)
        {
            //Configuração para envio de e-mail
            SmtpClient mailClient = new SmtpClient("mail.proseg.com.br", 25);
            NetworkCredential cred = new NetworkCredential("dti", "Adm@Proseg!2015");
            mailClient.Credentials = cred;


            string host = HttpContext.Current.Request.Url.Host;

            MailMessage mail = new MailMessage();

            // Se o ambiente é de teste, enviar Email a mim.
            if (host.Contains("localhost") || host.Contains("des."))
            {
                mail.To.Add("mario.santos@proseg.com.br");
                //mail.To.Add("informatica@proseg.com.br");
            }

            string[] arrTo = to.Split(';');
            foreach (var item in arrTo)
                mail.To.Add(item);


            // Anexando as imagens para serem exibidas no email (cabeçalho e rodapé)            
            string strImgCabecalho = "cid:ImgCabecalho";
            body = body.Replace("@imgCabecalho", strImgCabecalho);
            string strImgRodape = "cid:ImgRodape";
            body = body.Replace("@imgRodape", strImgRodape);
            AlternateView vm = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
            string img_path = "~/templates/imgs/logoSindmedico.png";
            img_path = HttpContext.Current.Server.MapPath(img_path);
            LinkedResource rs = new LinkedResource(img_path);
            rs.ContentId = "ImgCabecalho";
            vm.LinkedResources.Add(rs);
            img_path = "~/templates/imgs/rodapeEmail.png";
            img_path = HttpContext.Current.Server.MapPath(img_path);
            rs = new LinkedResource(img_path);
            rs.ContentId = "ImgRodape";
            vm.LinkedResources.Add(rs);
            mail.AlternateViews.Add(vm);

            mail.From = new MailAddress(from, nomeFrom, System.Text.Encoding.UTF8);
            mail.Subject = titulo;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            //mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;

            mailClient.Send(mail);
        }
    }
}
