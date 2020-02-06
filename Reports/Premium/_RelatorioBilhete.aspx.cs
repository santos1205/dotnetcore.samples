using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Microsoft.Reporting.WebForms;
using DataAccess.RelatorioBilheteDataSetTableAdapters;

namespace Premium
{
    public partial class _RelatorioBilhete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaRelatorio();
            }
        }

        [WebMethod]
        public static void CarregaRelatorio()
        {
            string Mes;
            string Ano;

            //QueryString recebida da função impressaoRelatorio.
            Mes = HttpContext.Current.Request.QueryString["Mes"];
            Ano = HttpContext.Current.Request.QueryString["Ano"];

            //Mantido para teste
            //Mes = "02";
            //Ano = "2018";

            //Instancia do relatório
            ReportViewer rptRelatorio = new ReportViewer();

            //Limpa dataSource
            rptRelatorio.LocalReport.DataSources.Clear();
            
            //Chamada dos valores do Relatorio Voucher
            //EmissaoVoucherDsTableAdapter EmissaoBilhete = new EmissaoVoucherDsTableAdapter();
            DataAccess.Voucher voucher = new DataAccess.Voucher();
            

            //Instancia do obj Usuário.
            var Usuario = HttpContext.Current.Session["Usuario"] as DataAccess.Usuario;

            //Instancia do datasource a adição das listas
            //Adição lista Orçamento ao DataSet.
            ReportDataSource rpdB = new ReportDataSource();
            rpdB.Name = "BilheteDs";
            //rpdB.Value = EmissaoBilhete.GetData(DataInicial, Datafinal); //Chama resultado da proc spListarVoucherPorMesAno.
            rpdB.Value = voucher.ListarVouchersDisponiveisPorMesAno(Mes, Ano); 

            //Parametros.
            ReportParameter[] param = new ReportParameter[4];
            param[0] = new ReportParameter("paramUsuario", Usuario.Nome.ToString());
            param[1] = new ReportParameter("paramFatura", (Mes + "/" + Ano).ToString());
            param[2] = new ReportParameter("paramData", DateTime.Now.ToString());

            //8874: Inserir data de vencimento
            DateTime dtVenc = Convert.ToDateTime(string.Format("20/{0}/{1}", Mes, DateTime.Now.Year));
            // passando para o mês seguinte ao da fatura
            string strDtVenc = dtVenc.AddMonths(1).ToShortDateString();

            param[3] = new ReportParameter("paramDtVencimento", strDtVenc);
            //param[2] = new ReportParameter("paramApolice", );

            //Insere os valores no report e define localização do mesmo.
            rptRelatorio.LocalReport.ReportPath = "Relatorio.rdlc";
            rptRelatorio.LocalReport.DataSources.Clear();
            rptRelatorio.LocalReport.DataSources.Add(rpdB);
            rptRelatorio.LocalReport.SetParameters(param);
            rptRelatorio.LocalReport.Refresh();

            ExportarPDF(rptRelatorio, Mes, Ano);
        }

        [WebMethod]
        public static void ExportarPDF(ReportViewer rptRelatorio, string Mes, string Ano)
        {
            //Variaveis base do relatorio.
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            //Monta o array dos bytes para criação do PDF.
            byte[] byteArray = rptRelatorio.LocalReport.Render("Pdf", null, out mimeType,
                                                            out encoding, out extension,
                                                            out streamids, out warnings);

            //Exporta o PDF.
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = mimeType;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=Fatura"+ "/" + Mes + "/" + Ano + "." + extension);
            HttpContext.Current.Response.BinaryWrite(byteArray);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        [WebMethod]
        public static void SalvarStatusVoucherAsync(int IdVoucher, int IdStatus, DateTime? DataCancelamento)
        { 
            
        }
    }
}