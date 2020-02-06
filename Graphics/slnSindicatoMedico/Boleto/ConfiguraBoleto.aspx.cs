using BaseAccess;
using BaseAccess.Enums;
using BaseAccess.Services;
using BaseAccess.VModels;
using Common;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace slnSindicatoMedico.MasterPage
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
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
        public static string GerarArquivoBoleto(int IdTemplate, int MesReferencia, string cpf, string filiacao)
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
            string nomeArquivoGerado = "";
            try
            {                
                nomeArquivoGerado = TemplateService.GerarArquivoBoleto(IdTemplate, MesReferencia, cpf, filiacao);
                TemplateService.CadastraHistoricoBoleto(Usuario, 1, nomeArquivoGerado, (int)StatusHistoricoEventoEnum.gerado, MesReferencia);

                return nomeArquivoGerado;
            }
            catch(Exception ex)
            {
                TemplateService.CadastraHistoricoBoleto(Usuario, 1, nomeArquivoGerado, (int)StatusHistoricoEventoEnum.erro, MesReferencia);
                return ex.Message;
            }                        
        }


        [WebMethod]
        public static ICollection<UsuarioVM> ListarUsuariosPendentes(string NomeUsuario, string Status)
        {
            var Usuarios = UsuarioService.ConsultarPorParams(nome: NomeUsuario, status: Status);
            return UsuarioService.Serialize(Usuarios);
        }


        [WebMethod]
        public static ICollection<HistoricoEventoVM> ListarHistoricoExportacao()
        {            
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;

            if (Usuario == null)
                return null;

            var Historico = TemplateService.ListarHistoricoExportacao();
            return TemplateService.Serialize(Historico);
            
        }

        [WebMethod]
        public static string SalvarLayoutTemplate(string NomeTemplate, ICollection<LayoutTemplate> Layout)
        {
            var Usuario = HttpContext.Current.Session["Usuario"] as BaseAccess.Usuario;
            if (Usuario == null)
                return "";

            try
            {
                TemplateService.SalvarLayout(Usuario, NomeTemplate, Layout);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return "";
        }

        [WebMethod]
        public static void RealizaLogoutAsync()
        {
            HttpContext.Current.Session.Clear();
        }

        [WebMethod]
        public static ICollection<TemplateVM> ConsultarTemplatesConfigurados()
        {
            try
            {
                var lista = TemplateService.ListarTemplatesConfigurados();
                return TemplateService.Serialize(lista);
            }
            catch
            {

            }
            return null;
        }

        [WebMethod]
        public static ICollection<LayoutVM> ConsultarLayoutTemplatePorId(int Id)
        {
            try
            {
                var lista = TemplateService.ConsultarLayoutTemplate(Id);
                return TemplateService.Serialize(lista);
            }
            catch(Exception ex)
            {
                var strMsg = ex.Message;
            }
            return null;
        }
    }
}