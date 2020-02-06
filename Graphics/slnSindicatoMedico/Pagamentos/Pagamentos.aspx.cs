using BaseAccess;
using BaseAccess.Enums;
using BaseAccess.Services;
using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using System.Web.Http;
using System.Web.Script.Services;

namespace slnSindicatoMedico.MasterPage
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        private static HttpPostedFile ArquivoValidacao;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region MOCKS DE TESTE
            // MOCKS DE TESTE
            //var pags = ListarPagamentoPorParams(DateTime.Parse("01-06-2019"), DateTime.Parse("01-06-2019"), "", 1, null, 3, "", "");            
            //var Pagamento = ConsultaPagamentoPorIdAsync(82);
            //SalvarBaixaPagamentoAsync(new Pagamento()
            //{
            //    pag_id = 377,
            //    pag_seg_id = 5585,
            //    pag_identificacao = "1234",
            //    pag_data_pagamento = DateTime.Parse("18-06-2019"),
            //    pag_data_vencimento = DateTime.Parse("18-06-2019"),
            //    pag_data_baixa_pagamento = DateTime.Now,
            //    pag_valor_recebido = (decimal)550.63
            //}); 
            #endregion

            var httpPostedFile = HttpContext.Current.Request.Files["UploadArquivoValidacao"];
            if (httpPostedFile != null)
            {
                ArquivoValidacao = httpPostedFile;
            }
        }

        [WebMethod]
        public static int VerificarNivelAcessoUsuarioAsync()
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
            if (Usuario != null)
                return Usuario.usr_nvl_id;
            else
                return 0;
        }

        [WebMethod]
        public static bool? VerificaPadraoSenha()
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
            if (Usuario != null)
                return UsuarioService.VerificaPadraoSegurancaSenha(Usuario);
            else
                return null;
        }

        [WebMethod]
        public static string RedefinirPadraoSenha()
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
            string senhaHash = Cripto.GerarHash32(Usuario.usr_senha);
            return senhaHash;
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
        public static void RealizaLogoutAsync()
        {
            HttpContext.Current.Session.Clear();
        }

        [WebMethod]
        public static ICollection<UsuarioVM> ListarUsuariosPendentes(string NomeUsuario, string Status)
        {
            var Usuarios = UsuarioService.ConsultarPorParams(nome: NomeUsuario, status: Status);
            return UsuarioService.Serialize(Usuarios);
        }

        [WebMethod]
        public static void SalvarBaixaPagamentoAsync(Pagamento Pagamento)
        {
            //Atualiza status segurados
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
            if (Usuario == null)
                return;

            PagamentoService.SalvarBaixaPagamento(Pagamento, Usuario);
        }

        [WebMethod]
        public static PagamentoVM ConsultaPagamentoPorIdAsync(int Id)
        {
            var Pagamento = PagamentoService.ConsultarPagamentoPorId(Id);
            return PagamentoService.Serialize(Pagamento);
        }

        [WebMethod]
        public static ICollection<PagamentoVM> ListarPagamentoPorParams(
            DateTime DtInicial,
            DateTime DtFinal,
            string NrCarteira,
            int? IdPlano,
            int? Crm,
            int? IdStatus,
            string Cpf,
            string Nome
        )
        {
            try
            {
                var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;

                var Pagamentos = PagamentoService.ListarPorParams(DtInicial, DtFinal, NrCarteira, IdPlano, Crm, IdStatus, Cpf, Nome, Usuario);

                // Caso retorne mais que 500 registros, limitar, para n dar erro na paginação js.
                var PagamentosSerilized = PagamentoService.Serialize(Pagamentos);

                if (Pagamentos.Count() > 500)
                    return PagamentosSerilized.Take(500).OrderByDescending(pag => Convert.ToDateTime(pag.DtVencimento)).ToList();
                else
                    return PagamentosSerilized.OrderByDescending(pag => Convert.ToDateTime(pag.DtVencimento)).ToList();
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        [WebMethod]
        public static ICollection<TemplateVM> ConsultarTemplatePadrao()
        {
            try
            {
                var lista = TemplateService.ListarTemplatesPadraoImportacao();
                return TemplateService.Serialize(lista);
            }
            catch (Exception ex)
            {
                var strMsg = ex.Message;
            }
            return null;
        }

        [WebMethod]
        public static ICollection<LayoutVM> ConsultarLayoutTemplatePorId(int Id)
        {
            try
            {
                var lista = TemplateService.ConsultarLayoutTemplateAtivo(Id);
                return TemplateService.Serialize(lista);
            }
            catch (Exception ex)
            {
                var strMsg = ex.Message;
            }
            return null;
        }

        [WebMethod]
        public static ICollection<HistoricoEventoVM> ListarHistoricoImportacao()
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;

            if (Usuario == null)
                return null;

            var Historico = TemplateService.ListarHistoricoImportacao();
            return TemplateService.SerializeImportacao(Historico);

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static ICollection<string> Validar(string MesReferencia)
        {

            MesReferencia = MesReferencia.Replace("/", "");

            ICollection<string> colecao = new List<string>();
            var httpPostedFile = ArquivoValidacao;
            var linhasArquivo = new List<string>();
            string importacaoLog = string.Empty;
            var leituraArquivo = new IOFile();
            int IdentificacaoTemplate = 0;

            linhasArquivo = leituraArquivo.ReadFile(httpPostedFile.InputStream);

            for (int i = 0; i < linhasArquivo.Count; i++)
            {
                ICollection<TemplateVM> Templates = ConsultarTemplatePadrao();
                string linha = linhasArquivo[i];
                bool erroMes = false;

                foreach (var template in Templates)
                {
                    string[] palavras = linha.Split(Convert.ToChar(template.Delimitador));
                    IdentificacaoTemplate = template.IdTemplate;
                    ICollection<LayoutVM> Layouts = ConsultarLayoutTemplatePorId(template.IdTemplate);
                    if (palavras.Length == Layouts.Count)
                    {
                        foreach (var layout in Layouts)
                        {
                            int posicaoCampo = layout.PosicaoCampo;
                            string Palavra = palavras[posicaoCampo - 1];

                            int tamanhoCampo = layout.TamanhoCampo;
                            string formatoCampo = layout.FormatoCampo;
                            string nomeCampo = layout.NomeCampo;
                            string templateId = layout.IdTemplate.ToString();

                            bool allCharactersInStringAreDigits = Palavra.All(char.IsDigit);

                            if(nomeCampo.Equals("Dia Mes"))
                            {
                                if (Palavra.Length != tamanhoCampo)
                                {
                                    colecao.Add(" Arquivo não validado, linha " + (i + 1) + ", o tamanho do campo esta diferente do determinado; ");
                                    importacaoLog += " Erro na Importação, linha " + (i + 1) + ", o tamanho do campo esta diferente do determinado ; ";
                                    erroMes = true;
                                    break;
                                }
                                else
                                {
                                    string mesRef = MesReferencia.Substring(0, 2);
                                    string mesAtual = Palavra.Substring(2, 2);

                                    if (!mesRef.Equals(mesAtual))
                                    {
                                        colecao.Add(" Arquivo não validado, período selecionado divergente do período do arquivo; ");
                                        importacaoLog += " Erro na Importação, linha " + (i + 1) + ", período selecionado divergente do período do arquivo; ";
                                        erroMes = true;
                                        break;
                                    }
                                }
                               
                            }
                            if (Palavra.Length > tamanhoCampo)
                            {
                                colecao.Add(" Arquivo não validado, linha " + (i + 1) + ", o tamanho do campo esta diferente do determinado; ");
                                importacaoLog += " Erro na Importação, linha " + (i + 1) + ", o tamanho do campo esta diferente do determinado; ";
                            }

                            if (formatoCampo == "N" && allCharactersInStringAreDigits == false)
                            {
                                colecao.Add(" Arquivo não validado, linha " + (i + 1) + ", o tipo do campo " + posicaoCampo + " está diferente do determinado; ");
                                importacaoLog += " Erro na Importação, linha " + (i + 1) + ", o tipo do campo " + posicaoCampo + " está diferente do determinado; ";
                            }
                        }
                        
                    }
                    else
                    {
                        colecao.Add(" Arquivo não validado, aquivo contem o número palavras diferente do configurado, erro linha: " + (i + 1) + "; ");
                        importacaoLog += " Erro na Importação, aquivo contem o número palavras diferente do configurado, erro linha: " + (i + 1);
                    }
                }
                if(erroMes)
                {
                    break;
                }
            }

            if (string.IsNullOrEmpty(importacaoLog))
            {
                GravarLeitura(IdentificacaoTemplate, httpPostedFile.FileName.ToString(), MesReferencia, (int)StatusHistoricoEventoEnum.validado);
                colecao.Add("Arquivo validado com sucesso");
                return colecao;
            }
            else
            {
                GravarLeitura(IdentificacaoTemplate, httpPostedFile.FileName.ToString(), MesReferencia, (int)StatusHistoricoEventoEnum.erro);
                return colecao;
            }
        }

        [WebMethod]
        public static void GravarLeitura(int IdTemplate, string nomeArquivo, string MesReferencia, int Status)
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
            try
            {
                TemplateService.GravarLeituraArquivoAutomatico(Usuario, IdTemplate, nomeArquivo,Status, MesReferencia);
            }
            catch (Exception ex)
            {
               
            }
        }

        [WebMethod]
        public static string GravarImportacao(ICollection<RepositorioImportacaoVM> Repositorio, string AnoMesReferencia, int idTemplate)
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
            try
            {
                TemplateService.salvarImportacao(Usuario, Repositorio, AnoMesReferencia, idTemplate);
                return "Sucesso Importacao";
            }
            catch (Exception ex)
            {
                return "Erro Importacao";
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static ICollection<string> Importar(string MesReferencia)
        {
            MesReferencia = MesReferencia.Replace("/", "");
            string mes = MesReferencia.Substring(0, 2);
            string ano = MesReferencia.Substring(2, 4);

            string anomes = ano + mes;
            ICollection<string> colecao = new List<string>();
            var httpPostedFile = ArquivoValidacao;
            var linhasArquivo = new List<string>();
            var leituraArquivo = new IOFile();
            var listaVM = new List<RepositorioImportacaoVM>();
            int idTemplate = 0;
            linhasArquivo = leituraArquivo.ReadFile(httpPostedFile.InputStream);

            for (int i = 0; i < linhasArquivo.Count; i++)
            {
                ICollection<TemplateVM> Templates = ConsultarTemplatePadrao();
                string linha = linhasArquivo[i];
                foreach (var template in Templates)
                {
                    idTemplate = template.IdTemplate;
                    var tVM = new RepositorioImportacaoVM()
                    {
                        TemplateId = template.IdTemplate,
                        Arquivo = linha
                    };
                    listaVM.Add(tVM);
                }
            }

            string importacao = GravarImportacao(listaVM, anomes, idTemplate);

            if (importacao.Equals("Sucesso Importacao"))
            {
                GravarLeitura(idTemplate, httpPostedFile.FileName.ToString(), MesReferencia, (int)StatusHistoricoEventoEnum.importado);

                colecao.Add("O arquivo foi importado com sucesso");
                return colecao;
            }
            else
            {
                GravarLeitura(idTemplate, httpPostedFile.FileName.ToString(), MesReferencia, (int)StatusHistoricoEventoEnum.erroimportacao);
                colecao.Add("Erro ao importar o arquivo");
                return colecao;
            }
        }
    }
}