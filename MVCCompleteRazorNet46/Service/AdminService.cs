using QuestionarioCOrg.DataAccess;
using QuestionarioCOrg.Enums;
using QuestionarioCOrg.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace QuestionarioCOrg.Service
{
    public class AdminService
    {
        public static void RemoverRegistro(string Modulo, int Id, string NvlAcesso = null)
        {
            if (!string.IsNullOrEmpty(NvlAcesso))
            {
                // Caso o nivel de acesso não seja gestor, n é permitido remoção
                if (!NvlAcesso.Equals(EnumHelper.GetDescription(NivelAcessoEnum.Gestor)))
                    throw new Exception("Usuário sem permissão para remoção");
            }

            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            switch(Modulo)
            {
                case "Questionario":
                    var ObjQ = db.Questionario.Find(Id);
                    ObjQ.qst_ativo = false;
                    break;
                case "Classificacao":
                    var ObjC = db.Classificacao.Find(Id);
                    ObjC.cls_ativo = false;                    
                    break;
                case "Pergunta":
                    var ObjP = db.Pergunta.Find(Id);
                    ObjP.prg_ativo = false;
                    break;
                case "Resposta":
                    var ObjR = db.Resposta.Find(Id);
                    ObjR.rsp_ativo = false;
                    break;
                case "Empresa":
                    var ObjE = db.Empresa.Find(Id);
                    ObjE.emp_ativo = false;
                    break;
                default:
                    break;
            }
            
            db.SaveChanges();
        }

        public static void AtualizarNotificacoes(Usuario ObjU)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            var Notif = db.Notificacoes.Where(x => x.ntf_usu_id == ObjU.usu_id);
            
            if (Notif != null && Notif.Count() > 0)
            {                
                Notif.FirstOrDefault().ntf_dt_visualizacao = DateTime.Now;
                db.SaveChanges();                                
            }
            else
            {
                var NewNotif = new Notificacoes
                {
                    ntf_dt_visualizacao = DateTime.Now,
                    ntf_usu_id = ObjU.usu_id
                };

                db.Notificacoes.Add(NewNotif);
                db.SaveChanges();
            }
        }

        public static void CompartilharFormularios(ICollection<CompartilhamentoVM> VM)
        {   
            EmailService.EnviarCompartilhamentoFormularios(VM);
        }

        public static DashboardVM ConsolidarDados()
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();
            var VM = new DashboardVM();

            // Valores consolidados
            VM.QtdeFormularios = db.Questionario.Where(x => x.qst_ativo).Count();
            var ObjRUs = db.RespostaUsuario;
            var ObjUs = new List<Usuario>();
            foreach (var RU in ObjRUs)
                ObjUs.Add(RU.Usuario);
            VM.QtdeFormulariosRespondidos = ObjUs.Distinct().Count();
            VM.QtdeUsuarios = db.Usuario.Where(x => x.usu_ativo && x.usu_aprovado != null).Count();
            VM.QtdeEmpresas = db.Empresa.Where(x => x.emp_ativo).Count();

            VM.Usuarios = ListarUsuarios();

            VM.Empresas = ListarEmpresas();

            return VM;
        }

        public static IEnumerable<Classificacao> ListarClassificacoesAtivas()
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();
            var ObjCls = db.Classificacao.Where(x => x.cls_ativo);

            foreach (var Cls in ObjCls)
            {
                string Nome = CommonService.PrimeiraLetraMaiuscula(Cls.cls_nome);
                Cls.cls_nome = Nome;
            }

            return ObjCls;
        }

        public static IEnumerable<Classificacao> ConsultarClassificacoesAtivas(int IdQ)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();
            var ObjCls = db.Classificacao.Where(x => x.cls_ativo && x.cls_qst_id == IdQ);

            foreach (var Cls in ObjCls)
            {
                string Nome = CommonService.PrimeiraLetraMaiuscula(Cls.cls_nome);
                Cls.cls_nome = Nome;
            }

            return ObjCls;
        }

        public static IEnumerable<Resposta> ConsultarRespostasAtivas(int IdQ)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();
            var ObjRs = db.Resposta.Where(x => x.rsp_ativo && x.rsp_qst_id == IdQ);
            
            return ObjRs;
        }

        public static IEnumerable<Questionario> ListarQuestionariosAtivos()
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();
            var ObjQs = db.Questionario.Where(x => x.qst_ativo);

            foreach (var Q in ObjQs)
            {
                string Nome = CommonService.PrimeiraLetraMaiuscula(Q.qst_nome);
                Q.qst_nome = Nome;
            }

            return ObjQs;
        }

        public static int ConsolidarUsuariosSemAvaliacao(Usuario ObjU)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            int TotalNotif = 0;
            // Carrega a ultima data de visualização das notificações            
            var UltimaVisualizacaoNotifs = DateTime.Now;
            var Notifs = db.Notificacoes.Where(x => x.ntf_usu_id == ObjU.usu_id).ToList();
            if (Notifs != null)
                if (Notifs.Count() > 0)            
                    UltimaVisualizacaoNotifs = Notifs.OrderByDescending(x => x.ntf_dt_visualizacao).FirstOrDefault().ntf_dt_visualizacao;
            
            TotalNotif += db.Usuario.Where(x => x.usu_ativo 
                    && x.usu_aprovado == null
                    && x.usu_dt_cadastro >= UltimaVisualizacaoNotifs).Count();  // Filtra usuários apenas os mais recentes
            
            return TotalNotif;
        }

        public static int ConsolidarNovasRespostas(Usuario ObjU)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            int TotalNotif = 0;
            // Carrega a ultima data de visualização das notificações            
            var UltimaVisualizacaoNotifs = DateTime.Now;
            var Notifs = db.Notificacoes.Where(x => x.ntf_usu_id == ObjU.usu_id);
            if (Notifs != null)
                if (Notifs.Count() > 0)
                    UltimaVisualizacaoNotifs = Notifs.OrderByDescending(x => x.ntf_dt_visualizacao).FirstOrDefault().ntf_dt_visualizacao;

            // Verificar respostas
            // Agrupando usuários respondentes
            var Resps = db.RespostaUsuario.Where(x => x.rpu_datacadastro >= UltimaVisualizacaoNotifs);  // Filtra respostas apenas os mais recentes
            var ObjNovosRespondentes = new List<Usuario>();
            if (Resps != null)            
                foreach(var rp in Resps)                
                    ObjNovosRespondentes.Add(rp.Usuario);


            TotalNotif += ObjNovosRespondentes.Distinct().Count();
            // Verificar novas respostas C. Org            
            var RespsCOrg = db.Media.Where(x => x.mda_dt_cadastro >= UltimaVisualizacaoNotifs);
            ObjNovosRespondentes = new List<Usuario>();
            // Agrupando usuários respondentes
            if (RespsCOrg != null)
                foreach (var rp in RespsCOrg)
                    ObjNovosRespondentes.Add(rp.Usuario);
            TotalNotif += ObjNovosRespondentes.Distinct().Count();



            return TotalNotif;
        }

        public static int ConsolidarNovosLeads(Usuario ObjU)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            int TotalNotif = 0;
            // Carrega a ultima data de visualização das notificações            
            var UltimaVisualizacaoNotifs = DateTime.Now;
            var Notifs = db.Notificacoes.Where(x => x.ntf_usu_id == ObjU.usu_id);
            if (Notifs != null)
                if (Notifs.Count() > 0)
                    UltimaVisualizacaoNotifs = Notifs.OrderByDescending(x => x.ntf_dt_visualizacao).FirstOrDefault().ntf_dt_visualizacao;
                        
            TotalNotif += db.lead_empresa_lgpd.Where(x => x.data_cadastro >= UltimaVisualizacaoNotifs).Count();  // Filtra leads apenas os mais recentes

            return TotalNotif;
        }

        public static IEnumerable<lead_empresa_lgpd> ConsultarLeads(string TpLead = "lead")
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();
            var ObjL = db.lead_empresa_lgpd.Where(x => x.formulario.Equals(TpLead));

            foreach (var Lead in ObjL)
            {
                string Nome = CommonService.PrimeiraLetraMaiuscula(Lead.nome_completo);
                Lead.nome_completo = Nome;
            }


            return ObjL;
        }

        public static EmpresaQuestionario SalvarPermissaoFormulario(int IdEQ, bool Ativo)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            var ObjEQ = db.EmpresaQuestionario.Find(IdEQ);

            ObjEQ.eqt_ativo = Ativo;
            db.SaveChanges();

            return ObjEQ;
        }

        public static IEnumerable<EmpresaQuestionario> ConsultarQuestionariosPorEmpresa(int IdE)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            var ObjEQs = db.EmpresaQuestionario.Where(x => x.eqt_emp_id == IdE && x.Questionario.qst_ativo);            

            return ObjEQs;
        }

        public static IEnumerable<Pergunta> ConsultarPerguntas(int IdQ)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            var ObjPs = db.Pergunta.Where(x => x.prg_qst_id == IdQ && x.prg_ativo);
                        
            return ObjPs;
        }
        public static ICollection<Usuario> ConsultarRespondentes(int? IdQ, int? IdE = 0)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            var ObjQCOrg = db.Questionario.Where(x => x.qst_nome.Contains("Cultura Organizacional")).FirstOrDefault();
            int IdQCOrg = ObjQCOrg.qst_id;

            if (IdQ == IdQCOrg)
            {
                var ObjMd = db.Media;
                var LsUs = new List<Usuario>();

                if (ObjMd.Count() > 0)
                {
                    foreach (var iMd in ObjMd)
                        LsUs.Add(iMd.Usuario);
                }

                return LsUs.Distinct().ToList();
            }
            else
            {
                var ObjRUs = db.RespostaUsuario.Where(x => x.rpu_qst_id == IdQ).ToList();
                if (IdE != 0)
                    ObjRUs = ObjRUs.Where(x => x.Empresa.emp_id == IdE).ToList();
                var LsUs = new List<Usuario>();

                foreach (var RU in ObjRUs)
                    LsUs.Add(RU.Usuario);

                return LsUs.Distinct().ToList();
            }
        }


        public static IEnumerable<Usuario> ListarUsuarios()
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            return db.Usuario;
        }

        public static IEnumerable<Empresa> ListarEmpresas()
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            return db.Empresa;
        }
        public static Empresa ConsultarEmpresaPorCnpj(string CNPJ)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();
            CNPJ = Regex.Replace(CNPJ, "[^0-9,]", "");
            return db.Empresa.Where(x => x.emp_cnpj == CNPJ).FirstOrDefault();
        }

        public static Usuario ConsultarUsuarioPorEmail(string Email)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();
            var ObjU = db.Usuario.Where(x => x.usu_email == Email).FirstOrDefault();
            return db.Usuario.Where(x => x.usu_email == Email).FirstOrDefault();
        }


        public static IEnumerable<RespostaUsuario> ConsultarRespostasUsuarios(int IdE, int IdQ, int IdU = 0)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();
            if (IdU != 0)
                return db.RespostaUsuario.Where(x => x.rpu_qst_id == IdQ && x.rpu_emp_id == IdE && x.rpu_usu_id == IdU);
            else
                return db.RespostaUsuario.Where(x => x.rpu_qst_id == IdQ && x.rpu_emp_id == IdE);
        }


        public static Usuario ConsultarUsuarioPorCPF(string CPF)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();
            CPF = Regex.Replace(CPF, "[^0-9,]", "");
            return db.Usuario.Where(x => x.usu_cpf == CPF).FirstOrDefault();
        }

        public static UsuarioVM SalvarUsuario(UsuarioVM VM)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities(); 

            // Usuario
            // retirando máscaras
            VM.CPF = Regex.Replace(VM.CPF, "[^0-9,]", "");
            VM.Telefone = Regex.Replace(VM.Telefone, "[^0-9,]", "");
            // Nivel de acesso padrão - respondente
            string strAcesso = EnumHelper.GetDescription(NivelAcessoEnum.Respondente);
            var Acesso = ConsultarNivelAcessoPorNome(strAcesso);
            VM.IdAcesso = Acesso != null ? Acesso.nvl_id : 1;            
            
            if (VM.Id == 0)
            {
                var objU = VM.ToModel();
                db.Usuario.Add(objU);
                db.SaveChanges();
                VM.Id = objU.usu_id;
            }else
            {
                var objU = db.Usuario.Find(VM.Id);
                objU.usu_nome = !string.IsNullOrEmpty(VM.Nome) ? VM.Nome : objU.usu_nome;
                objU.usu_cargo = !string.IsNullOrEmpty(VM.Cargo) ? VM.Cargo : objU.usu_cargo;
                objU.usu_cpf = !string.IsNullOrEmpty(VM.CPF) ? VM.CPF : objU.usu_cpf;
                objU.usu_senha = !string.IsNullOrEmpty(VM.Senha) ? VM.Senha : objU.usu_senha;
                objU.usu_email = !string.IsNullOrEmpty(VM.Email) ? VM.Email : objU.usu_email;
                objU.usu_telefone = !string.IsNullOrEmpty(VM.Telefone) ? VM.Telefone : objU.usu_telefone;
                db.SaveChanges();
            }

            return VM;
        }

        public static QuestionarioVM SalvarQuestionario(QuestionarioVM VM)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            if (VM.Id == 0)
            {
                var objQ = VM.ToModel();
                db.Questionario.Add(objQ);
                db.SaveChanges();
                VM.Id = objQ.qst_id;

                // Vincula questionário recém criado com todas as empresascadastradas
                var Emps = db.Empresa;
                foreach (var Emp in Emps)
                {
                    var EQ = new EmpresaQuestionario()
                    {
                        eqt_qst_id = VM.Id,
                        eqt_emp_id = Emp.emp_id,
                        eqt_dt_cadastro = DateTime.Now,
                        eqt_ativo = true
                    };
                    db.EmpresaQuestionario.Add(EQ);
                }
                db.SaveChanges();
            }
            else
            {
                var objQ = db.Questionario.Find(VM.Id);
                objQ.qst_nome = VM.Nome;
                objQ.qst_publicado = VM.Publicado;
                db.SaveChanges();
            }

            return VM;
        }

        public static void SalvarRegistroUsuarioEmpresa(EmpresaVM VME, UsuarioVM VMU)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            // EMPRESA
            if (VME.Id == 0)
            {
                var objE = VME.ToModel();
                db.Empresa.Add(objE);
                db.SaveChanges();
                VME.Id = objE.emp_id;

                // Vincula empresa recém criada com todos os questionários cadastrados
                var Qsts = db.Questionario;
                foreach (var Qst in Qsts)
                {
                    var EQ = new EmpresaQuestionario()
                    {
                        eqt_emp_id = VME.Id,
                        eqt_qst_id = Qst.qst_id,
                        eqt_dt_cadastro = DateTime.Now,
                        eqt_ativo = true
                    };
                    db.EmpresaQuestionario.Add(EQ);
                }
                db.SaveChanges();
            }
            else
            {
                var objE = db.Empresa.Find(VME.Id);
                objE.emp_nome = VME.Nome;
                objE.emp_cidade = VME.Cidade;
                objE.emp_estado = VME.Estado;
                objE.emp_ramo = VME.Ramo;

                db.SaveChanges();
            }

            // USUARIO
            // retirando máscaras
            VMU.CPF = Regex.Replace(VMU.CPF, "[^0-9,]", "");
            VMU.Telefone = Regex.Replace(VMU.Telefone, "[^0-9,]", "");
            // Vinculo c empresa recém cadastrada.
            VMU.IdEmpresa = VME.Id;
            // Nivel de acesso padrão - respondente
            string strAcesso = EnumHelper.GetDescription(NivelAcessoEnum.Respondente);
            var Acesso = ConsultarNivelAcessoPorNome(strAcesso);
            VMU.IdAcesso = Acesso != null ? Acesso.nvl_id : 1;

            if (VMU.Id == 0)
            {
                var objU = VMU.ToModel();
                objU.usu_aprovado = DateTime.Now;
                db.Usuario.Add(objU);
                db.SaveChanges();
                VMU.Id = objU.usu_id;
            }
            else
            {
                var objU = db.Usuario.Find(VMU.Id);
                objU.usu_nome = !string.IsNullOrEmpty(VMU.Nome) ? VMU.Nome : objU.usu_nome;
                objU.usu_cargo = !string.IsNullOrEmpty(VMU.Cargo) ? VMU.Cargo : objU.usu_cargo;
                objU.usu_cpf = !string.IsNullOrEmpty(VMU.CPF) ? VMU.CPF : objU.usu_cpf;
                objU.usu_email = !string.IsNullOrEmpty(VMU.Email) ? VMU.Email : objU.usu_email;
                objU.usu_telefone = !string.IsNullOrEmpty(VMU.Telefone) ? VMU.Telefone : objU.usu_telefone;                
                db.SaveChanges();
            }
        }

        public static EmpresaVM SalvarEmpresa(EmpresaVM VM)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            if (VM.Id == 0)
            {
                var objE = VM.ToModel();
                db.Empresa.Add(objE);
                db.SaveChanges();
                VM.Id = objE.emp_id;

                // Vincula empresa recém criada com todos os questionários cadastrados
                var Qsts = db.Questionario;
                foreach(var Qst in Qsts)
                {
                    var EQ = new EmpresaQuestionario() { 
                        eqt_emp_id = VM.Id,
                        eqt_qst_id = Qst.qst_id,
                        eqt_dt_cadastro = DateTime.Now,
                        eqt_ativo = true
                    };
                    db.EmpresaQuestionario.Add(EQ);
                }
                db.SaveChanges();
            }
            else
            {
                var objE = db.Empresa.Find(VM.Id);
                objE.emp_nome = VM.Nome;
                objE.emp_cidade = VM.Cidade;
                objE.emp_estado = VM.Estado;
                objE.emp_ramo = VM.Ramo;

                db.SaveChanges();
            }

            return VM;
        }

        public static bool ValidaQuestionarioPorUsuario(Usuario Usuario, int IdQ, bool QstCOrg = false)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();
            if (Usuario == null)
                return false;

            // Caso o form. seja C. Org, pegar id do banco.
            if (QstCOrg)
            {
                var ObjQ = db.Questionario.Where(x => x.qst_nome.Contains("Cultura Organizacional")).FirstOrDefault();
                if (ObjQ != null)
                    IdQ = ObjQ.qst_id;
            }

            if (Usuario.Empresa.EmpresaQuestionario.Where(x => x.eqt_emp_id == Usuario.usu_emp_id
                         && x.eqt_qst_id == IdQ && x.eqt_ativo).Count() > 0)
                return true;

            return false;
        }

        public static NivelAcesso ConsultarNivelAcessoPorNome(string Acesso)
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            return db.NivelAcesso.Where(x => x.nvl_nome.Equals(Acesso)).FirstOrDefault();
        }

        private static void CadastrarEmpresaQuestionario(Empresa E = null, Questionario Q = null)
        {

        }
    }
}