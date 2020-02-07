using BaseAccess.Enums;
using BaseAccess.VModels;
using System;
using System.Web;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Common;

namespace BaseAccess
{
    public class UsuarioService
    {
        
        public void Add(Usuario Usuario)
        {
            try
            {
                SindicatoMedicoEntities ctx = new SindicatoMedicoEntities();
                ctx.Usuario.Add(Usuario);
                ctx.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string msgError = "Erro durante o cadastro da empresa - ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msgError += ve.ErrorMessage;
                        throw new Exception(msgError);
                    }
            }
        }

        public static bool VerificaPadraoSegurancaSenha(Usuario Usuario)
        {
            SindicatoMedicoEntities ctx = new SindicatoMedicoEntities();

            return ctx.Usuario.FirstOrDefault(x => x.usr_id == Usuario.usr_id).usr_atualiza_senha;
        }

        public static Usuario VerificarSession()
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
            return Usuario;
        }

        public static bool VerificarSenhaCriptografada(Usuario Usuario, string senhaInput)
        {
            SindicatoMedicoEntities ctx = new SindicatoMedicoEntities();
            // Carrega o hash senha armazenado
            string hashSenha = ctx.Usuario.FirstOrDefault(x => x.usr_id == Usuario.usr_id).usr_senha;
            return Cripto.VerificaHash32(senhaInput, hashSenha);
        }

        public static bool VerificarSenhaCriptografada(int IdUsuario, string senha, string senhaCriptografada)
        {
            SindicatoMedicoEntities ctx = new SindicatoMedicoEntities();
            var Usuario = ctx.Usuario.FirstOrDefault(x => x.usr_id == IdUsuario);
            return Cripto.VerificaHash32(senha, senhaCriptografada);
        }

        public Usuario VerificaToken(string token)
        {
            SindicatoMedicoEntities ctx = new SindicatoMedicoEntities();
            return ctx.Usuario.Where(x => x.usr_senha.Equals(token)).FirstOrDefault();
        }

        public Usuario ConsultarEmail(string email)
        {
            SindicatoMedicoEntities ctx = new SindicatoMedicoEntities();
            return ctx.Usuario.Where(x => x.usr_email.Equals(email)).FirstOrDefault();
        }

        public static Usuario ConsultarPorCpf(string usr_cpf)
        {
            SindicatoMedicoEntities ctx = new SindicatoMedicoEntities();

            var Usuario = ctx.Usuario.Where(x => x.usr_cpf.Equals(usr_cpf)).FirstOrDefault();
            return Usuario;
        }

        public static Usuario ConsultarPorId(int usr_id)
        {
            SindicatoMedicoEntities ctx = new SindicatoMedicoEntities();
            return ctx.Usuario.FirstOrDefault(x => x.usr_id == usr_id);
        }

        public static void AprovarUsuario(int usr_id)
        {
            SindicatoMedicoEntities ctx = new SindicatoMedicoEntities();
            ctx.Usuario.Where(x => x.usr_id == usr_id).FirstOrDefault().usr_aprovado = "A";
            ctx.SaveChanges();
        }

        public static void AtualizarNivelAcessoUsuario(string Acesso, int IdUsuario, Usuario UsuarioLogado = null)
        {
            var ctx = new SindicatoMedicoEntities();

            try
            {                
                if (UsuarioLogado != null)
                    LogService.CapturaOpcaoLogUsuario(UsuarioLogado, (int)AcaoEnum.AlteracaoNivelAcessoUsuario, IdUsuario);

                var UsuarioBase = ctx.Usuario.FirstOrDefault(x => x.usr_id == IdUsuario);

                int IdNvlAcesso = 0;
                if (Acesso == "C")
                    IdNvlAcesso = 1;
                if (Acesso == "F")
                    IdNvlAcesso = 2;
                if (Acesso == "A")
                    IdNvlAcesso = 3;

                if (IdNvlAcesso != 0)
                    UsuarioBase.usr_nvl_id = IdNvlAcesso;
                ctx.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string msgError = "";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;
                throw new Exception(msgError);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public static void ReprovarUsuario(Usuario Usuario, int usr_id)
        {
            SindicatoMedicoEntities ctx = new SindicatoMedicoEntities();

            // Captura log - atualizar contatos
            LogService.CapturaOpcaoLog(Usuario, AcaoEnum.ReprovacaoUsuario.ToString(), usr_id);


            ctx.Usuario.Where(x => x.usr_id == usr_id).FirstOrDefault().usr_aprovado = "R";
            ctx.SaveChanges();
        }

        public static ICollection<Usuario> ConsultarPorParams(string nome = "", string status = "N")
        {
            SindicatoMedicoEntities ctx = new SindicatoMedicoEntities();

            status = string.IsNullOrEmpty(status) ? "N" : status;

            var Usuario = ctx.Usuario.Where(x => x.usr_aprovado == status);
            if (!string.IsNullOrEmpty(nome))
                Usuario = Usuario.Where(x => x.usr_nome.Contains(nome));

            // Formatando as datas
            var UsuarioDatesFormatted = Usuario.ToList();
            foreach (var item in UsuarioDatesFormatted)
                item.usr_dt_cadastro = item.usr_dt_cadastro;

            return UsuarioDatesFormatted;
        }

        public static void AtualizaSenha(string usr_cpf, string novaSenha)
        {
            SindicatoMedicoEntities ctx = new SindicatoMedicoEntities();

            var Usuario = ctx.Usuario.Where(x => x.usr_cpf.Equals(usr_cpf)).FirstOrDefault();
            Usuario.usr_senha = novaSenha;
            Usuario.usr_atualiza_senha = true;
            ctx.SaveChanges();
        }

        public static UsuarioVM Serialize(Usuario Usuario)
        {
            var VM = new UsuarioVM();
            VM.IdUsuario = Usuario.usr_id;
            VM.Nome = Usuario.usr_nome;
            VM.Cpf = Usuario.usr_cpf;
            VM.Email = Usuario.usr_email;
            if(Usuario.NivelAcesso != null)
                VM.NvlAcesso = Usuario.NivelAcesso.nvl_id;
            
            return VM;
        }

        public static UsuarioVM SerializeError(Usuario Usuario)
        {
            var VM = new UsuarioVM();
            VM.MsgErro = Usuario.MsgErro;
            return VM;
        }

        public static UsuarioVM SerializeErrorSenha(Usuario Usuario)
        {
            var VM = new UsuarioVM();
            VM.MsgSenha = Usuario.MsgErroSenha;
            return VM;
        }

        public static ICollection<UsuarioVM> Serialize(ICollection<Usuario> Usuarios)
        {
            List<UsuarioVM> lista = new List<UsuarioVM>();

            int maxCount = Usuarios.Count();

            foreach (var item in Usuarios)
            {
                var Usu = new UsuarioVM();

                Usu.IdUsuario = item.usr_id;
                Usu.Nome = item.usr_nome;
                Usu.Cpf = item.usr_cpf;
                Usu.Email = item.usr_email;
                Usu.DataCadastro = item.usr_dt_cadastro.ToShortDateString();
                Usu.NvlAcesso = item.NivelAcesso.nvl_id;
                Usu.Aprovado = item.usr_aprovado;

                lista.Add(Usu);
            }

            return lista;
        }
    }
}
