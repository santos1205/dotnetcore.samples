using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Premium
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string CarregaComboMes(string ano)
        {
            var Voucher = new Voucher();
            string strPeriodoVoucher = Voucher.ListarMesesVouchersDisponiveisPorAno(ano);

            return strPeriodoVoucher;
        }

        [WebMethod]
        public static string SalvarVoucher(int idVoucher, int idStatus, string dt_cancelamento)
        {
            var Voucher = new Voucher();
            string retornoBanco = "";
            DateTime dataCancelamento = new DateTime();


            if (!string.IsNullOrEmpty(dt_cancelamento))
            {
                dataCancelamento = Convert.ToDateTime(dt_cancelamento);
            }

            try
            {
                var Usuario = HttpContext.Current.Session["Usuario"] as Usuario;
                retornoBanco = Voucher.SalvarStatusVoucher(idVoucher, idStatus, dataCancelamento, Usuario.IdUsuario);
            }
            catch(Exception e)
            {
                return "Erro: " + e.Message;
            }            

            return retornoBanco;
        }

        
        //8854
        // m. Santos
        [WebMethod]
        public static List<string> ListarDataCadastroAno()
        {
            var listAno = new Voucher().ListarDataCadastro_groupByAno();
            return listAno;
        }

        [WebMethod]
        public static List<Voucher> ListarVouchers(string ano, string mes)
        {
            var Voucher = new Voucher();

            var Lista = new List<Voucher>();


            mes = mes == "janeiro" ? "1" : mes;
            mes = mes == "fevereiro" ? "2" : mes;
            mes = mes == "março" ? "3" : mes;
            mes = mes == "abril" ? "4" : mes;
            mes = mes == "maio" ? "5" : mes;
            mes = mes == "junho" ? "6" : mes;
            mes = mes == "julho" ? "7" : mes;
            mes = mes == "agosto" ? "8" : mes;
            mes = mes == "setembro" ? "9" : mes;
            mes = mes == "outubro" ? "10" : mes;
            mes = mes == "novembro" ? "11" : mes;
            mes = mes == "dezembro" ? "12" : mes;

            try
            {
                Lista = Voucher.ListarVouchersDisponiveisPorMesAno(ano, mes);
            }
            catch(Exception e)
            {
                var VoucherErro = new Voucher { msgErro = "Erro: " + e.Message };
                Lista.Add(VoucherErro);
            }

            return Lista;
        }

        [WebMethod]
        public static Voucher DetalhaVoucher(int idVoucher)
        {
            var voucher = new Voucher();
            voucher.DetalhaVoucherPorID(idVoucher);
            return voucher;
        }
    }
}