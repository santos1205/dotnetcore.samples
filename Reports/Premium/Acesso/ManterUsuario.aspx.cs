using Common;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Premium.Acesso
{
    public partial class ManterUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            

        }


        [WebMethod]
        public static string MetodoTeste()
        {
            return "true";
        }

        [WebMethod]
        public static Usuario ConsultaUsuarioPorCpfAsync(string cpf)
        {
            Usuario Usuario = new Usuario();
            Usuario.ConsultarPorCpf(cpf);

            return Usuario;
        }

        [WebMethod]
        public static string CadastraUsuario(Usuario objUsuario)
        {            
            var vlrDataCadastro = DateTime.Now;            
            try
            {                
                objUsuario.DataCadastro = vlrDataCadastro;
                objUsuario.Deletado = false;
                objUsuario.Inserir();

                return "ok";
            }
            catch (Exception e)
            {
                return new Exception("Problemas no cadastro do usuário: " + e.Message).ToString();
            }

        }

        [WebMethod]
        public static string VerificaLoginAsync(string user, string senha)
        {
            var Usuario = new Usuario();
            try
            {
                //Se login e senha do usuário confere, verifica se o msm está aprovado.
                bool confereLogin = Usuario.VerificaLogin(user, senha);
                if (confereLogin)
                {
                    //Verifica se usuário está aprovado.
                    if (Usuario.Aprovado)
                    {
                        var Log = new Log
                        {
                            IdUsuario = Usuario.IdUsuario,
                        };
                        Log.Login();
                        return "true";
                    }
                    else {
                        var Log = new Log
                        {
                            IdUsuario = Usuario.IdUsuario,
                        };
                        Log.LoginSemSucesso(user);
                        return "pendente";
                    }
                }
                else
                {
                    var Log = new Log
                    {
                        IdUsuario = Usuario.IdUsuario,
                    };
                    Log.LoginSemSucesso(user);
                }
            }
            catch (Exception e)
            {
                return "Erro: " + e.Message;
            }            
            return "false";
        }

        [WebMethod]
        public static Usuario VerificaTokenRecupSenhaAsync(string token)
        {
            var Usuario = new Usuario();
            Usuario.VerificaToken(token);
            return Usuario;
        }

        [WebMethod]
        public static string SalvarRecuperacaoSenhaAsync(string cpf, string novaSenha)
        {
            try
            {
                var Usuario = new Usuario();
                //Atualiza senha pelo cpf.
                Usuario.Cpf = cpf;
                Usuario.AtualizaSenha(novaSenha);

                return "Sua senha foi atualizada com sucesso.";
            }
            catch (Exception e)
            {
                return "Erro durante a recuperação da senha.";
            }
        } 

        [WebMethod]
        public static string RecuperarSenhaAsync(string cpf)
        {
            try
            {
                //Recupera todos os dados do usuário pelo e-mail.
                var Usuario = new Usuario();
                Usuario.ConsultarPorCpf(cpf);
                if (Usuario.Cpf == null)
                    throw new Exception("Não foi encontrado registro com o cpf informado.");                
                var Email = new Email();
                Email.EnviarEmailRecuperacaoSenha(Usuario);
                    
                return "Dentro de Instantes você receberá um e-mail com o link para redefinição de senha";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        } 

    }
}