using System;
using System.Net.Mail;

namespace API.Seguros.Proseg.Domain.Util
{
    public class Email
    {
        public bool SendMail(string strDe, string strPara, string strAssunto, string strTexto, string strPathAnexo, bool bolConfRec)
        {
            try
            {
                //Cria o objeto que envia o e-mail 
                SmtpClient client = new SmtpClient
                {
                    Host = "mail.proseg.com.br",
                    UseDefaultCredentials = true,
                    Credentials = new System.Net.NetworkCredential("envio", "inmetro")
                };

                //Cria o endereço de email do remetente 
                MailAddress mailDe = new MailAddress(strDe);

                MailMessage mensagem = new MailMessage();

                if (strTexto.IndexOf("<html") > -1 || strTexto.IndexOf("<Html") > -1)
                {
                    mensagem.IsBodyHtml = true;
                }
                else
                {
                    mensagem.IsBodyHtml = false;
                }


                //tratamento para envio de email para mais de um usuario
                if (strPara.IndexOf(",") > -1)
                {
                    string[] arrListaMails = strPara.Split(',');
                    for (int i = 0; i < arrListaMails.Length; i++)
                    {
                        mensagem.To.Add(new MailAddress(arrListaMails[i]));
                    }
                }
                else
                {
                    mensagem.To.Add(new MailAddress(strPara));
                }
                mensagem.From = mailDe;

                //Assunto do email
                mensagem.Subject = strAssunto;

                if (bolConfRec)
                {
                    mensagem.Headers.Add("Disposition-Notification-To", strDe);
                }

                //incluindo anexo
                mensagem.Attachments.Add(new Attachment(strPathAnexo));

                //Conteúdo do email
                mensagem.Body = strTexto;

                try
                {
                    //Envia o email 
                    client.Send(mensagem);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool SendMail(string strDe, string strPara, string strAssunto, string strTexto, bool bolConfRec, bool LogEmail = false)
        {
            try
            {

                // 9419 m. Santos
                string strUrlAmbiente = "http://segurosns.prosegnet.com.br/";
                //string strUrlAmbiente = ConfigurationSettings.AppSettings["url"];

                //m 09/08/2016
                //Só envia e-mail em ambiente de produção
                // 9419
                //if (System.Web.HttpContext.Current.Server.MachineName != "PROD-APP-MC")
                // 9617
                if (strUrlAmbiente == "http://segurosns.prosegnet.com.br/" || strUrlAmbiente == "http://multicalculo.proseg.com.br/")
                {
                    return false;
                }

                //Cria o objeto que envia o e-mail 
                SmtpClient client = new SmtpClient
                {
                    Host = "mail.proseg.com.br",
                    UseDefaultCredentials = true,
                    Credentials = new System.Net.NetworkCredential("envio", "inmetro")
                };

                //Cria o endereço de email do remetente 
                MailAddress mailDe = new MailAddress(strDe);

                MailMessage mensagem = new MailMessage();

                if (strTexto.IndexOf("<html") > -1 || strTexto.IndexOf("<Html") > -1)
                {
                    mensagem.IsBodyHtml = true;
                }
                else
                {
                    mensagem.IsBodyHtml = false;
                }


                //tratamento para envio de email para mais de um usuario
                if (strPara.IndexOf(",") > -1)
                {
                    string[] arrListaMails = strPara.Split(',');
                    for (int i = 0; i < arrListaMails.Length; i++)
                    {
                        mensagem.To.Add(new MailAddress(arrListaMails[i]));
                    }
                }
                else
                {
                    mensagem.To.Add(new MailAddress(strPara));
                }
                mensagem.From = mailDe;

                //confirmando recebimento
                if (bolConfRec)
                {
                    mensagem.Headers.Add("Disposition-Notification-To", strDe);
                }

                //Assunto do email
                mensagem.Subject = strAssunto;

                //Conteúdo do email
                mensagem.Body = strTexto;

                try
                {
                    //Envia o email 
                    client.Send(mensagem);

                    //m 03/01/2018
                    //BugId: 8324
                    #region LogEmail

                    //if (LogEmail)
                    //{
                    //    DaoSQL oDao = new DaoSQL();

                    //    string[,] paramEmail = new string[7, 3];

                    //    paramEmail[0, 0] = "@idUsuario";
                    //    paramEmail[0, 1] = Convert.ToString(HttpContext.Current.Session["idUsuario"]); //idUsuario
                    //    paramEmail[0, 2] = "INT";

                    //    paramEmail[1, 0] = "@idCalculo";
                    //    paramEmail[1, 1] = "0";
                    //    paramEmail[1, 2] = "INT";

                    //    paramEmail[2, 0] = "@idproposta";
                    //    paramEmail[2, 1] = "0";
                    //    paramEmail[2, 2] = "INT";

                    //    paramEmail[3, 0] = "@emailDestinatario";
                    //    paramEmail[3, 1] = strPara;
                    //    paramEmail[3, 2] = "VARCHAR";

                    //    paramEmail[4, 0] = "@emailTitulo";
                    //    paramEmail[4, 1] = strAssunto;
                    //    paramEmail[4, 2] = "VARCHAR";

                    //    paramEmail[5, 0] = "@emailCorpo";
                    //    paramEmail[5, 1] = strTexto;
                    //    paramEmail[5, 2] = "NTEXT";

                    //    Metodos objMetodos = new Metodos();
                    //    paramEmail[6, 0] = "@EmailDataEnvio";
                    //    paramEmail[6, 1] = Convert.ToDateTime(objMetodos.DataServidor()).ToString("dd/MM/yy HH:mm");
                    //    paramEmail[6, 2] = "VARCHAR";

                    //    SqlDataReader oDr = oDao.GetSelectSp("spInserirLogEmail", paramEmail);

                    //}

                    #endregion

                    return true;
                }
                catch
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
