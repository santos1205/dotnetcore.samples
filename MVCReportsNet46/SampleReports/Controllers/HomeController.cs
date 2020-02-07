using Microsoft.Reporting.WebForms;
using SampleReports.Datasets;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System;

namespace SampleReports.Controllers
{

    // refs:
    // https://www.youtube.com/watch?v=jUi42kcUvvo&t=20s
    // https://www.youtube.com/watch?v=lnzAptaN0Fs

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var ds = LoadDataSet();

            var viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\rptUsuarios.rdlc";
            viewer.LocalReport.DataSources.Add(new ReportDataSource("dsUsuarios", (DataTable)ds.Usuarios));

            viewer.LocalReport.Refresh();


            ExportarPDF(viewer);

            ViewBag.RPT = viewer;

            return View();
        }



        private dsUsuarios LoadDataSet()
        {
            var ds = new dsUsuarios();

            var ctx = new Database1Entities();

            var objUsus = ctx.Usuarios;

            foreach(var usu in objUsus)            
                ds.Usuarios.AddUsuariosRow(usu.Id, usu.Nome);          

            
            return ds;
        }

        private void ExportarPDF(ReportViewer rptRelatorio, string Titulo = "titulo")
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
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Titulo + "_" + DateTime.Now.ToShortDateString() + "." + extension);
            Response.BinaryWrite(byteArray);
            HttpContext.ApplicationInstance.CompleteRequest();
        }
    }
}