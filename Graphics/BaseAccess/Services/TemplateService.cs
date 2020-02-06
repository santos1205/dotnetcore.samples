using BaseAccess.Enums;
using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using Common;
using System.Threading.Tasks;

namespace BaseAccess.Services
{
    public class TemplateService
    {
        public static ICollection<Template> ListarTemplatesConfigurados()
        {
            var ctx = new SindicatoMedicoEntities();
            var lista = ctx.Template;

            return lista.ToList();
        }

        public static ICollection<LayoutTemplate> ConsultarLayoutTemplate(int Id)
        {
            var ctx = new SindicatoMedicoEntities();

            try
            {
                var lista = ctx.LayoutTemplate.Where(x => x.lay_tpl_id == Id).OrderBy(x => x.lay_posicao_campo);
                return lista.ToList();
            }
            catch (DbEntityValidationException e)
            {
                string msgError = "Erro durante o cadastro - ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                throw new Exception("erro adicionar dependente: " + msgError);
            }
        }

        public static string GerarArquivoBoleto(int IdTemplate, int MesReferencia, string cpf, string filiacao)
        {
            // TODO: ESTE MÉTODO PASSARÁ POR EVOLUÇÃO, QNDO TIVERMOS MAIS DE UM LAYOUT DE EXPORTAÇÃO.
            try
            {
                List<string> ConteudoLinhas = new List<string>();

                // Colocar o cabeçalho            
                string linha = "";
                var ctx = new SindicatoMedicoEntities();
                var Layout = ctx.LayoutTemplate.Where(x => x.lay_tpl_id == IdTemplate && x.lay_ativo == true).OrderBy(x => x.lay_posicao_campo);
                foreach (var lyt in Layout)
                    linha += lyt.lay_nome_campo + ";";
                ConteudoLinhas.Add(linha);
                linha = "";
                // FILTROS
                // PAGAMENTOS SOMENTE ATIVOS, EM ABERTO, DOS PLANOS DE SAÚDE E DO MÊS DE REFERÊNCIA SELECIONADO.
                var listaPags = ctx.Pagamento.Where(x => 
                    x.pag_ativo == true &&
                    x.pag_spg_id == (int)PagamentoStatusEnum.AguardandoPagamento && 
                    x.Plano.pla_tpp_id == (int)TipoPlanoEnum.Saude && 
                    x.pag_data_vencimento.Month == MesReferencia
                );
                
                if (!string.IsNullOrEmpty(cpf))
                    listaPags = listaPags.Where(x => x.Segurado.seg_cpf.Equals(cpf));
                decimal numfiliacao = new decimal();

                if (!string.IsNullOrEmpty(filiacao))
                {
                    numfiliacao = decimal.Parse(filiacao);
                    listaPags = listaPags.Where(x => x.Segurado.seg_num_filiacao == numfiliacao);
                }                   

                // caso n retorne valor, disparar exceção de erro.
                if (listaPags.Count() == 0)
                    throw new Exception("erro durante a exportação. Nenhum registro encontrado para exportação.");
                                
                foreach (var item in listaPags)
                {
                    // Percorrendo o layout para carregar de acordo com a configuração
                    foreach (var lyt in Layout)
                    {
                        if (lyt.lay_nome_campo.Equals("Número Dac"))
                        {
                            // Nr. Dac
                            DadosCobranca DadosCobranca = item.Segurado.DadosCobranca.FirstOrDefault();
                            string NrDac = "";
                            if (DadosCobranca != null)
                                NrDac = DadosCobranca.dco_agencia + "" + DadosCobranca.dco_conta;
                            else
                                NrDac = "";
                            linha += "" + NrDac + ";";
                        }
                        if (lyt.lay_nome_campo.Equals("Id Filiação"))
                            // Nr. Filiacao
                            linha += "" + item.Segurado.seg_num_filiacao + ";";
                        if (lyt.lay_nome_campo.Equals("Nome Pagador"))
                        {
                            // Nome Segurado
                            linha += "" + item.Segurado.seg_nome + ";";
                        }
                        if (lyt.lay_nome_campo.Equals("Vencimento"))
                        {
                            // Vencimento                
                            if (item.pag_data_vencimento != null)
                            {
                                string dataVencimento = item.pag_data_vencimento.ToShortDateString().Replace("/", "");
                                linha += "" + dataVencimento + ";";
                            }
                        }
                        if (lyt.lay_nome_campo.Equals("Dia Mes"))
                        {
                            // Dia Mes
                            if (item.Segurado != null)
                            {
                                if (item.Segurado.MelhorDiaPagamento != null)
                                {
                                    string DiaMes = item.Segurado.MelhorDiaPagamento.mdp_dia.ToString("00") + "" + item.pag_data_vencimento.Month.ToString("00");
                                    linha += "" + DiaMes + ";";
                                }                                
                            }                            
                        }
                        if (lyt.lay_nome_campo.Equals("Crédito / Débito"))
                        {
                            // Valor credito / debito 
                            decimal? vlrVencimento = item.pag_valor_vencimento;
                            if (vlrVencimento != null)
                                linha += "" + vlrVencimento.ToString().Replace(",", ".") + ";";
                        }
                        if (lyt.lay_nome_campo.Equals("Outros Valores"))
                        {
                            // Outros valores
                            linha += "4.95;";
                        }
                        if (lyt.lay_nome_campo.Equals("Histórico"))
                        {
                            // Histórico
                            linha += "L;";
                        }
                        if (lyt.lay_nome_campo.Equals("Dep Rec"))
                        {
                            // Dep Rec
                            linha += "4320;";
                        }
                        if (lyt.lay_nome_campo.Equals("CPF Segurado"))
                        {
                            // CPF Segurado
                            linha += item.Segurado.seg_cpf + ";";
                        }
                        if (lyt.lay_nome_campo.Equals("Valor"))
                        {
                            // Valor pagamento
                            decimal? vlrVencimento = item.pag_valor_vencimento;
                            if (vlrVencimento != null)
                                linha += "" + vlrVencimento.ToString().Replace(",", ".") + ";";
                            else
                                linha += ";";
                        }
                        if (lyt.lay_nome_campo.Equals("Cod. Boleto"))
                            // Código Boleto
                            linha += "01;";
                    }

                    ConteudoLinhas.Add(linha);
                    linha = "";
                }

                // Nome do arquivo (boletos)
                string mes = MesReferencia.ToString("00");
                string ano = DateTime.Now.Year.ToString();
                string diaGeracao = DateTime.Now.Day.ToString("00");
                string mesGeracao = DateTime.Now.Month.ToString("00");
                string nomeArq = string.Format("boletos_mes_referencia_{0}{1}_id_{2}{3}_{4}{5}{6}", mes, ano, diaGeracao, mesGeracao, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);

                IOFile.GravarArquivoBoleto(nomeArq, ConteudoLinhas);

                return nomeArq;
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }

        public static ICollection<HistoricoEvento> ListarHistoricoExportacao()
        {
            var ctx = new SindicatoMedicoEntities();
            // Lista o histórico, apenas dos arquivos de exportação.
            var lista = ctx.HistoricoEvento.Where(x => x.Template.tpl_tipo_acao == "E").OrderByDescending(x => x.hev_DataEvento);
            return lista.ToList();
        }

        public static ICollection<HistoricoEvento> ListarHistoricoImportacao()
        {
            var ctx = new SindicatoMedicoEntities();
            // Lista o histórico, apenas dos arquivos de exportação.
            var lista = ctx.HistoricoEvento.Where(x => x.Template.tpl_tipo_acao == "I").OrderByDescending(x => x.hev_DataEvento);
            return lista.ToList();
        }

        public static ICollection<LayoutTemplate> ConsultarLayoutTemplateAtivo(int Id)
        {
            var ctx = new SindicatoMedicoEntities();

            try
            {
                var lista = ctx.LayoutTemplate.Where(x => x.lay_tpl_id == Id && x.lay_ativo == true).OrderBy(x => x.lay_posicao_campo);
                return lista.ToList();
            }
            catch (DbEntityValidationException e)
            {
                string msgError = "Erro durante o cadastro - ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                throw new Exception("erro adicionar dependente: " + msgError);
            }
        }

        public static ICollection<Template> ListarTemplatesPadraoImportacao()
        {
            var ctx = new SindicatoMedicoEntities();
            try
            {
                var lista = ctx.Template.Where(x => x.tpl_tipo_acao == "I");
                return lista.ToList();
            }
            catch (DbEntityValidationException e)
            {
                string msgError = "Erro durante a consulta - ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                throw new Exception("erro ao consultar template padrão: " + msgError);
            }
        }

        public static void SalvarLayout(Usuario Usuario, string NomeTemplate, ICollection<LayoutTemplate> Layout)
        {
            var ctx = new SindicatoMedicoEntities();

            try
            {
                int IdTemplate = Layout.FirstOrDefault().lay_tpl_id;
                var Template = ctx.Template.FirstOrDefault(x => x.tpl_id == IdTemplate);
                Template.tpl_nome_template = NomeTemplate;
                
                foreach (var itemLayout in Template.LayoutTemplate)
                {
                    itemLayout.lay_ativo = Layout.FirstOrDefault(x => x.lay_id == itemLayout.lay_id).lay_ativo;
                    itemLayout.lay_posicao_campo = Layout.FirstOrDefault(x => x.lay_id == itemLayout.lay_id).lay_posicao_campo;
                }

                // Tornando o template salvo em padrão
                var Templates = ctx.Template.Where(x => x.tpl_ativo == true).ToList();
                foreach(var tmpl in Templates)                
                    if (tmpl.tpl_id == IdTemplate)
                        tmpl.tpl_padrao = true;
                    else
                        tmpl.tpl_padrao = false;

                ctx.SaveChanges();

                LogService.CapturaOpcaoLog(Usuario, AcaoEnum.AlteracaoConfiguracaoArquivoBoleto.ToString());
            }
            catch (DbEntityValidationException e)
            {
                string msgError = "erro durante a atualização do layout - ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;
                throw new Exception(msgError);
            }
        }

        public static void CadastraHistoricoBoleto(Usuario Usuario, int IdTemplate, string NomeArquivo, int StatusEvento, int MesReferencia)
        {
            var ctx = new SindicatoMedicoEntities();
            var HistoricoEvento = new HistoricoEvento();
            string strMesRef = MesReferencia.ToString("00") + DateTime.Now.Year.ToString("00");

            HistoricoEvento.hev_DataEvento = DateTime.Now;
            HistoricoEvento.hev_NomeArquivo = NomeArquivo;
            HistoricoEvento.hev_tpl_id = IdTemplate;
            HistoricoEvento.hev_StatusEvento = (short)StatusEvento;
            HistoricoEvento.hev_usr_id = Usuario.usr_id;
            HistoricoEvento.hev_MesReferencia = strMesRef;

            ctx.HistoricoEvento.Add(HistoricoEvento);
            ctx.SaveChanges();
        }

        public static void GravarLeituraArquivoAutomatico(Usuario Usuario, int IdTemplate, string NomeArquivo, int StatusEvento, string MesReferencia)
        {
            var ctx = new SindicatoMedicoEntities();
            var HistoricoEvento = new HistoricoEvento();
            string strMesRef = MesReferencia;

            HistoricoEvento.hev_DataEvento = DateTime.Now;
            HistoricoEvento.hev_NomeArquivo = NomeArquivo;
            HistoricoEvento.hev_tpl_id = IdTemplate;
            HistoricoEvento.hev_StatusEvento = (short)StatusEvento;
            HistoricoEvento.hev_usr_id = Usuario.usr_id;
            HistoricoEvento.hev_MesReferencia = strMesRef;

            ctx.HistoricoEvento.Add(HistoricoEvento);
            ctx.SaveChanges();

            if(StatusEvento==2 || StatusEvento == 5)
                LogService.CapturaOpcaoLog(Usuario, AcaoEnum.AlteracaoBaixaPagamentoAutomatica.ToString());
        }

        public static ICollection<TemplateVM> Serialize(ICollection<Template> Templates)
        {
            var listaVM = new List<TemplateVM>();

            foreach (var item in Templates)
            {
                var tVM = new TemplateVM()
                {
                    IdTemplate = item.tpl_id,
                    NomeTemplate = item.tpl_nome_template,
                    Padrao = item.tpl_padrao,
                    Delimitador = item.tpl_delimitador
                };

                listaVM.Add(tVM);
            } 

            return listaVM;
        }

        public static ICollection<LayoutVM> Serialize(ICollection<LayoutTemplate> Layouts)
        {
            var listaVM = new List<LayoutVM>();

            foreach (var item in Layouts)
            {
                var lVM = new LayoutVM()
                {
                    IdLayout = item.lay_id,
                    IdTemplate = item.lay_tpl_id,
                    NomeCampo = item.lay_nome_campo,
                    NomeTemplate = item.Template.tpl_nome_template,
                    PosicaoCampo = item.lay_posicao_campo,
                    TamanhoCampo = item.lay_tamanho_campo,
                    FormatoCampo = item.lay_formato_campo,
                    ObrigatoriedadeCampo = item.lay_obrigatoriedade.ToString(),
                    Ativo = item.lay_ativo
                };

                listaVM.Add(lVM);
            }

            return listaVM;
        }
        
        public static ICollection<HistoricoEventoVM> Serialize(ICollection<HistoricoEvento> Historico)
        {
            var listaVM = new List<HistoricoEventoVM>();

            foreach (var item in Historico)
            {
                var hVM = new HistoricoEventoVM()
                {
                    Id = item.hev_id,
                    DataEvento = item.hev_DataEvento.ToString(),
                    Usuario = item.Usuario.usr_nome,
                    NomeArquivo = item.hev_NomeArquivo,
                    NomeTemplate = item.Template.tpl_nome_template,
                    MesRef = item.hev_MesReferencia,
                    Status = item.hev_StatusEvento == 1 ? "gerado" : "erro"
                };
                hVM.MesRef = item.hev_MesReferencia.Substring(0, 2) + "/" + item.hev_MesReferencia.Substring(2, 4);

                listaVM.Add(hVM);
            }

            return listaVM;
        }

        public static ICollection<HistoricoEventoVM> SerializeImportacao(ICollection<HistoricoEvento> Historico)
        {
            var listaVM = new List<HistoricoEventoVM>();
            string status = string.Empty;
            foreach (var item in Historico)
            {
                if (item.hev_StatusEvento == 1)
                    status = "Gerado";
                if (item.hev_StatusEvento == 2)
                    status = "Erro";
                if (item.hev_StatusEvento == 3)
                    status = "Validado";
                if (item.hev_StatusEvento == 4)
                    status = "Importado";
                if (item.hev_StatusEvento == 5)
                    status = "Erro Importação";

                var hVM = new HistoricoEventoVM()
                {
                    Id = item.hev_id,
                    DataEvento = item.hev_DataEvento.ToString(),
                    Usuario = item.Usuario.usr_nome,
                    NomeArquivo = item.hev_NomeArquivo,
                    NomeTemplate = item.Template.tpl_nome_template,
                    MesRef = item.hev_MesReferencia,
                    Status = status
                };
                hVM.MesRef = item.hev_MesReferencia.Substring(0, 2) + "/" + item.hev_MesReferencia.Substring(2, 4);

                listaVM.Add(hVM);
            }

            return listaVM;
        }

        public static void salvarImportacao(Usuario usuario,ICollection<RepositorioImportacaoVM> RepositorioImportacao, string ANOMES, int idTemplate)
        {
            var ctx = new SindicatoMedicoEntities();

            try
            {
                var RepositorioDel = ctx.RepositorioImportacao;
                foreach (var Registro in RepositorioDel)
                {
                    ctx.RepositorioImportacao.Remove(Registro);
                }
                ctx.SaveChanges();

                foreach (var item in RepositorioImportacao)
                {
                    var RpAdd = new RepositorioImportacao()
                    {
                        rep_tpl_id = item.TemplateId,
                        rep_arquivo = item.Arquivo
                    };

                    ctx.RepositorioImportacao.Add(RpAdd);
                }

                ctx.SaveChanges();

                try
                {
                    ctx.StpBaixaPagamentoAutomatico(idTemplate,ANOMES);
                }
                catch (DbEntityValidationException e)
                {
                    string msgError = "erro durante a atualização do layout - ";
                    LogService.CapturaOpcaoLog(usuario, AcaoEnum.AlteracaoBaixaPagamentoAutomatica.ToString());
                }

                LogService.CapturaOpcaoLog(usuario, AcaoEnum.BaixaPagamentoAutomatica.ToString());
            }
            catch (DbEntityValidationException e)
            {
                LogService.CapturaOpcaoLog(usuario, AcaoEnum.AlteracaoBaixaPagamentoAutomatica.ToString());
                string msgError = "erro durante a atualização do layout - ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;
                throw new Exception(msgError);
            }
        }
    }
}
