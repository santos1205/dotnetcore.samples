using System;
using DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Premium
{
    public partial class _BilhetesEmitidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var Usuario = Session["Usuario"] as Usuario;
            if (Usuario == null)
                Response.Redirect("~/Acesso/ManterUsuario.aspx");
        }

        [WebMethod]
        public static Voucher InserirVoucher(Voucher objVoucher)
        {
            try
            {
                //Instancia do obj usuario.
                var Usuario = HttpContext.Current.Session["Usuario"] as Usuario;

                //Define valores através do objeto.
                objVoucher.IdUsuario = Usuario.IdUsuario;

                //Cadastra voucher.
                objVoucher.Inserir();                
            }
            catch(Exception e)
            {
                objVoucher.msgErro = "Erro: " + e.Message;
                return objVoucher;
            }
            return objVoucher;
        }

        [WebMethod]
        public static string CalculaPremio(int diarias)
        {
            //Instancia do obj Voucher
            Voucher objVoucher = new Voucher();
            
            //Realiza a consulta na tabela.
            objVoucher.ConsultaTaxa();

            //Regra: Quantidade de diarias * 7.33 (valor da Taxa Diária)
            objVoucher.valorTaxaFinal = diarias * objVoucher.valorTaxa;            
            var arrVlrFinal = objVoucher.valorTaxaFinal.ToString().Split(',');
            string strvlrFinal = "";
            if (arrVlrFinal.Length > 1)
            {
                if (arrVlrFinal[1].Length == 1)
                    arrVlrFinal[1] = arrVlrFinal[1] + "0";                
                strvlrFinal = arrVlrFinal[0] + "," + arrVlrFinal[1].Substring(0, 2);
            }            
            else
                strvlrFinal = arrVlrFinal[0] + ",00";


            return "R$ " + strvlrFinal;
        }
        
    }
}