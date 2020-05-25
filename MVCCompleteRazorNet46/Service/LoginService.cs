using QuestionarioCOrg.DataAccess;
using QuestionarioCOrg.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionarioCOrg.Service
{
    public class LoginService
    {
         private static QuestionarioOrgDBEntities _dbContext = new QuestionarioOrgDBEntities();
        public static Usuario VerificarLogin(LoginVM VM)
        {   
            // CENÁRIO DE VERIFICAÇÃO COM SUCESSO
            var objU = _dbContext.Usuario.Where(x => x.usu_ativo && x.usu_aprovado != null && x.usu_senha == VM.Senha && x.usu_email == VM.Email).FirstOrDefault();
            if (objU != null)
                return objU;            
            // CENÁRIO DE VERIFICAÇÃO COM RESTRIÇÃO DE APROVAÇÃO
            objU = _dbContext.Usuario.Where(x => x.usu_ativo && x.usu_senha == VM.Senha && x.usu_email == VM.Email && x.usu_aprovado == null).FirstOrDefault();
            if (objU != null)
                throw new Exception("Seu acesso está pendente de aprovação.");
            // CENÁRIO DE VERIFICAÇÃO DE LOGIN OU SENHA INVÁLIDOS
            objU = _dbContext.Usuario.Where(x => x.usu_ativo && x.usu_aprovado != null && x.usu_senha == VM.Senha && x.usu_email == VM.Email).FirstOrDefault();
            if (objU == null)
                throw new Exception("Login ou senha inválidos.");

            return objU;
        }

        public static bool ValidarSolicitacaoSenha(string Hash)
        {
            
            // Acrescenta a private key 1205
            Hash = Hash + "1205";
            var ObjU = _dbContext.Usuario.Where(x => x.usu_senha.Equals(Hash)).FirstOrDefault();
            if (ObjU != null)
                return true;
            return false;
        }


        public static Usuario SalvarSenhaTemporaria(int IdUsuario)
        {            
            var ObjU = _dbContext.Usuario.Find(IdUsuario);
            Random random = new Random();
            string numRand = random.Next(0, 9999).ToString("0000");
            string senhaTemp = ObjU.usu_cpf + numRand + "1205";

            ObjU.usu_senha = senhaTemp;

            _dbContext.SaveChanges();

            return ObjU;
        }
    }
}