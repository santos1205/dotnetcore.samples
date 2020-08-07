using QuestionarioCOrg.DataAccess;
using QuestionarioCOrg.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace QuestionarioCOrg.Service
{
    public class LGPDService
    {
        readonly private static QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();
        public static void SalvarLead(LeadEbookVM VM)
        {
            db.lead_empresa_lgpd.Add(VM.ToModel());
            db.SaveChanges();
        }

        public static int CarregarEtapa(int Id)
        {
            var objLead = db.lead_empresa_lgpd.Find(Id);

            int EtapaAtual = objLead.etapa_chat != null ? (int)objLead.etapa_chat : 0;

            return (int)EtapaAtual;
        }

        public static void DesativarHash(int Id)
        {
            var objLead = db.lead_empresa_lgpd.Find(Id);
            objLead.lead_hash = null;
            objLead.etapa_chat = 0;

            db.SaveChanges();
        }

        public static void SalvarDepartamentoAvulso(int Id, string Departamento, int Etapa)
        {
            var objLead = db.lead_empresa_lgpd.Find(Id);
            var objDepto = new Departamento
            {
                dpt_nome = Departamento,
                dpt_emp_id = (int)objLead.lgpd_id_empresa,
                dpt_ativo = true,
                dpt_dt_cadastro = DateTime.Now
            };

            db.Departamento.AddOrUpdate(objDepto);
            objLead.etapa_chat = Etapa;
            db.SaveChanges();
        }

        public static void SalvarDepartamentosChat(int Id, string[] Departamentos, int Etapa)
        {
            var objLead = db.lead_empresa_lgpd.Find(Id);
            foreach(var depto in Departamentos)
            {
                var objDepto = new Departamento
                {
                    dpt_nome = depto,
                    dpt_emp_id = (int)objLead.lgpd_id_empresa,
                    dpt_dt_cadastro = DateTime.Now,
                    dpt_ativo = true                    
                };
                db.Departamento.AddOrUpdate(objDepto);
            }
            objLead.etapa_chat = Etapa;

            db.SaveChanges();
        }

        public static void SalvarDadosChat(int IdLead, string Resposta, int Etapa)
        {
            var objLead = db.lead_empresa_lgpd.Find(IdLead);
            
            switch (Etapa)
            {
                // Atualização pergunta: A empresa faz armazenamento de dados dos clientes?
                case 3:
                    objLead.lgpd_armazena_dados = Resposta;
                    break;
                // Atualização pergunta: A empresa utiliza serviços de parceiros/terceiros para realizar o processamento/trabalho de dados de seus clientes?
                case 4:
                    objLead.compartilha_dados = Resposta;
                    break;
                // Atualização pergunta: Sua empresa já iniciou a adequação da LGPD?
                case 5:
                    objLead.iniciou_adequacao = Resposta;
                    break;
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 14:
                    //INSERIR DPTO EMPRESA
                    var objDepto = new Departamento
                    {
                        dpt_nome = Resposta,
                        dpt_dt_cadastro = DateTime.Now,
                        dpt_ativo = true,
                        dpt_emp_id = (int)objLead.lgpd_id_empresa                       
                    };
                    db.Departamento.Add(objDepto);
                    db.SaveChanges();
                    break;
                case 20:
                    objLead.lgpd_situacao_ti = Resposta;
                    break;
                case 21:
                    objLead.lgpd_situacao_juridico = Resposta;
                    break;
                case 22:
                    objLead.qnt_colaborador = int.Parse(Resposta);
                    break;
                default:
                    break;

            }
            objLead.etapa_chat = Etapa;

            db.SaveChanges();
        }
        public static lead_empresa_lgpd ConsultarLead(int Id)
        {
            return db.lead_empresa_lgpd.Find(Id);
        }

        public static Empresa ConsultarEmpresaLead(int IdLead)
        {
            var objLead = db.lead_empresa_lgpd.Find(IdLead);
            var objEmpresa = db.Empresa.Find(objLead.lgpd_id_empresa);
            return objEmpresa;
        }

        public static IEnumerable<Departamento> ConsultarDeptosLead(int IdLead)
        {
            var objLead = db.lead_empresa_lgpd.Find(IdLead);
            
            if (objLead != null)
            {
                var objEmpresa = db.Empresa.Find(objLead.lgpd_id_empresa);
                if (objEmpresa == null)
                    return new List<Departamento>();
                return objEmpresa.Departamento;
            }
            return null;
        }

        public static bool VerificarAcessoLPGDLeads(string hash)
        {
            if (hash == null)
                return false;
            // Verifica se o hash informado existe na base
            if (db.lead_empresa_lgpd.Where(x => x.lead_hash.Equals(hash)).Count() == 0)
                return false;

            return true;
        }

        public static LeadLGPDVM SalvarLead(LeadLGPDVM VM, string hashAcesso = null)
        {
            var objLeadLGPD = VM.ToModel();

            objLeadLGPD.lead_hash = hashAcesso;
            db.lead_empresa_lgpd.AddOrUpdate(objLeadLGPD);

            db.SaveChanges();
            VM.Id = objLeadLGPD.id;

            return VM;
        }

        public static EmpresaVM SalvarEmpresa(EmpresaVM VM)
        {
            var objE = VM.ToModel();
            db.Empresa.AddOrUpdate(objE);
            db.SaveChanges();
            VM.Id = objE.emp_id;

            return VM;
        }
    }
}