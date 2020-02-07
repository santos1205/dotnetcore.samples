using BaseAccess;
using BaseAccess.Enums;
using BaseAccess.Services;
using BaseAccess.VModels;
using Common;
using System;
using System.Web;
using System.Web.Services;
using System.Text.RegularExpressions;

namespace slnSindicatoMedico.Usuario
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        [WebMethod]
        public static void RealizaLogoutAsync()
        {
            HttpContext.Current.Session.Clear();
        }

        [WebMethod]
        public static string VerificaLoginAsync(string user, string senha)
        {
            try
            {
                var Usuario = new BaseAccess.Usuario();                
                //Se login e usr_senha do usuário confere, verifica se o msm está usr_aprovado.                
                Usuario = UsuarioService.ConsultarPorCpf(user);
                if (Usuario == null)
                    return "Login ou senha inválidos.";
                // Essa condição ficará vigente até todos os usuário tiverem senhas criptografadas. Então a condição só será a VerificarSenhaCriptografada.
                if (Usuario.usr_senha.Equals(senha) || UsuarioService.VerificarSenhaCriptografada(Usuario, senha))
                {
                    HttpContext.Current.Session["Usuario"] = Usuario;
                    HttpContext.Current.Session["NomeUsuario"] = Usuario.usr_nome;
                    HttpContext.Current.Session["IdNvlAcesso"] = Usuario.NivelAcesso.nvl_id.ToString();
                  
                    // Captura Log de Login
                    LogService.CapturaOpcaoLog(Usuario, AcaoEnum.Login.ToString());
                    return Usuario.usr_aprovado + ":" + Usuario.usr_nvl_id;
                }
                else
                    return "Login ou senha inválidos.";
                
            }
            catch (Exception e)
            {
                //var Email = new Email().EnviarEmailErro(Usuario, "Erro no login: " + e.Message);
                return "Ocorreu erro na tentativa de login: " + e.Message;
            }
        }

        [WebMethod]
        public static UsuarioVM VerificaSessionAsync()
        {
            var Usuario = UsuarioService.VerificarSession();
            try
            {
                return UsuarioService.Serialize(Usuario);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [WebMethod]
        public static UsuarioVM ConsultaUsuarioPorCpfAsync(string usr_cpf)
        {
            var UService = new UsuarioService();
            var Usuario = UsuarioService.ConsultarPorCpf(usr_cpf);
            return UsuarioService.Serialize(Usuario);
        }

        [WebMethod]
        public static UsuarioVM CadastraUsuario(BaseAccess.Usuario objUsuario)
        {
            try
            {
                var UService = new UsuarioService();
                objUsuario.usr_dt_cadastro = DateTime.Now;
       
                objUsuario.usr_aprovado = "N";
                objUsuario.usr_deletado = false;

                if (ValidarSenha(objUsuario.usr_senha))
                {
                    // Criptografar senha.
                    objUsuario.usr_senha = Cripto.GerarHash32(objUsuario.usr_senha);
                    UService.Add(objUsuario);

                    //Após a conclusão do cadastro, é enviado um e-mail para o usuário.
                    var objEmail = new Email();
                    if (!objEmail.EnviarEmailCadastro(objUsuario.usr_nome, objUsuario.usr_email))
                    {
                        objEmail.EnviarEmailErro(objUsuario.usr_nome, "Problemas no envio de email.");
                        throw new Exception("Problemas no envio de email.");
                    }
                }
                else
                {
                    objUsuario.MsgErroSenha = "senha invalida";
                    return UsuarioService.SerializeErrorSenha(objUsuario);
                }
            }
            catch (Exception ex)
            {
                objUsuario.MsgErro = ex.Message;
                return UsuarioService.SerializeError(objUsuario);
            }
            return UsuarioService.Serialize(objUsuario);
        }

        public static bool ValidarSenha(string txt_usr_senha)
        {
            bool senhaValida = true;
            Regex rgxLetraMaiuscula = new Regex(@"[A-Z]");
            Regex rgxNumeros = new Regex(@"[0-9]");
            Regex rgxCaracterEspecial = new Regex(@"[\^\!#$%&'()*+,-./:;?@[\\\]_`´{|}~¨¿♥]");

            if (txt_usr_senha.Length < 6 || !rgxLetraMaiuscula.IsMatch(txt_usr_senha) || !rgxNumeros.IsMatch(txt_usr_senha) || !rgxCaracterEspecial.IsMatch(txt_usr_senha))
            {
                senhaValida = false;
            }
           
            return senhaValida;
        }


        [WebMethod]
        public static string SalvarRecuperacaoSenhaAsync(string cpf, string novaSenha)
        {
            try
            {   
                var Usuario = UsuarioService.ConsultarPorCpf(cpf);
                //Atualiza usr_senha pelo usr_cpf.

                if (ValidarSenha(novaSenha))
                {
                    // Criptografar senha
                    novaSenha = Cripto.GerarHash32(novaSenha);
                    UsuarioService.AtualizaSenha(cpf, novaSenha);
                    // Captura Log - Redefinição Senha.
                    LogService.CapturaOpcaoLog(Usuario, AcaoEnum.RedefinicaoSenha.ToString());

                    return "Sua senha foi atualizada com sucesso.";
                }
                else
                {
                    return "sua nova senha esta fora do padrão, a senha deve ter no minímo 6 caracteres, incluindo números, ao menos uma letra maiúscula e ao menos um caractere especial por favor verifique!";
                }
            }
            catch (Exception e)
            {
                return "Erro durante a recuperação da senha.";
            }
        }


        [WebMethod]
        public static string VerificaTokenPadraoSenhaAsync(string token)
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
            if (UsuarioService.VerificarSenhaCriptografada(Usuario.usr_id, Usuario.usr_senha, token))
                return Usuario.usr_cpf;
            return "";
        }
            

        [WebMethod]
        public static UsuarioVM VerificaTokenRecupSenhaAsync(string token)
        {
            var UService = new UsuarioService();
            var Usuario = new BaseAccess.Usuario();
            //Usuario.VerificaToken(token);
            Usuario = UService.VerificaToken(token + "1205");
            return UsuarioService.Serialize(Usuario);
        }


        [WebMethod]
        public static bool VerificaEmailCadastradoAsync(string email)
        {
            //var Usuario = new BaseAccess.Usuario();

            var UService = new UsuarioService();
            var Usuario = UService.ConsultarEmail(email);
            //Usuario.ConsultarPorEmail(email);

            if (Usuario != null)
                return true;
            else
                return false;
        }

        [WebMethod]
        public static string RecuperarSenhaAsync(string cpf)
        {
            try
            {                
                //Recupera todos os dados do usuário pelo e-mail.
                var Usuario = new BaseAccess.Usuario();
                
                Usuario = UsuarioService.ConsultarPorCpf(cpf);
                if (Usuario.usr_cpf == null)
                    return "Não foi encontrado registro com o usr_cpf informado.";
                //Verifica se o usuário está usr_aprovado.
                if (Usuario.usr_aprovado != "A")
                    throw new Exception("Favor aguarde a aprovação do seu cadastro.");
                var Email = new Email();                
                LogService.CapturaOpcaoLog(Usuario, AcaoEnum.RecuperacaoSenha.ToString());
                Random random = new Random();
                string numRand = random.Next(0, 9999).ToString("0000");
                string senhaTemp = Usuario.usr_cpf + numRand;
                //colocar uma máscara no token, para não ser logado diretamente, antes de passar pela confirmação da nova usr_senha pelo usuário.
                string senhaTempMascara = senhaTemp + "1205";
                UsuarioService.AtualizaSenha(Usuario.usr_cpf, senhaTempMascara);
                string linkRedefinir = HttpContext.Current.Request.Url.AbsoluteUri;
                linkRedefinir += "?solicitRec=" + senhaTemp;
                linkRedefinir = linkRedefinir.Replace("/RecuperarSenhaAsync", "");


                Email.EnviarRecuperarSenha(Usuario.usr_nome, Usuario.usr_senha, Usuario.usr_email, linkRedefinir);
                
                return "Dentro de Instantes você receberá um e-mail com o link para redefinição de senha";
            }
            catch (Exception e)
            {
                return "Erro durante a recuperação de senha: " + e.Message;
            }
        }
    }
} 