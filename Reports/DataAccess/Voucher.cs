using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class Voucher
    {
        public int IdVoucher { get; set; }
        public int IdUsuario { get; set; }
        public int Flag { get; set; }
        public int idTaxa { get; set; }
        public int codTaxa { get; set; }
        public decimal valorTaxa { get; set; }
        public decimal valorTaxaFinal { get; set; }
        public string NomePassageiro { get; set; }
        public string CpfPassageiro { get; set; }
        public string NumVoucher { get; set; }
        public int Diaria { get; set; }
        public string Destino { get; set; }
        public DateTime DtInicioVig { get; set; }
        public DateTime DtFinalVig { get; set; }
        public DateTime DtCadastro { get; set; }
        public string strDtInicioVig { get; set; }
        public string strDtFinalVig { get; set; }
        public string strDtCadastro { get; set; }
        public string msgErro { get; set; }
        public decimal Premio { get; set; }
        public int IdStatus { get; set; }
        public bool Deletado { get; set; }
        public Usuario Usuario { get; set; }


        public void Inserir()
        {
            try
            {                
                #region Inserção dos parametros

                System.Data.SqlClient.SqlParameter[] parametros = new SqlParameter[12];

                parametros[0] = new SqlParameter();
                parametros[0].SqlDbType = System.Data.SqlDbType.Int;
                parametros[0].Direction = System.Data.ParameterDirection.Input;
                parametros[0].Value = this.IdVoucher;
                parametros[0].ParameterName = "@ems_id";

                parametros[1] = new SqlParameter();
                parametros[1].SqlDbType = System.Data.SqlDbType.Int;
                parametros[1].Direction = System.Data.ParameterDirection.Input;
                parametros[1].Value = this.IdUsuario;
                parametros[1].ParameterName = "@idUsuario";

                parametros[2] = new SqlParameter();
                parametros[2].SqlDbType = System.Data.SqlDbType.NVarChar;
                parametros[2].Direction = System.Data.ParameterDirection.Input;
                parametros[2].Value = this.NomePassageiro;
                parametros[2].ParameterName = "@ems_nome_passageiro";

                parametros[3] = new SqlParameter();
                parametros[3].SqlDbType = System.Data.SqlDbType.Int;
                parametros[3].Direction = System.Data.ParameterDirection.Input;
                parametros[3].Value = this.Diaria;
                parametros[3].ParameterName = "@ems_diaria";

                parametros[4] = new SqlParameter();
                parametros[4].SqlDbType = System.Data.SqlDbType.NVarChar;
                parametros[4].Direction = System.Data.ParameterDirection.Input;
                parametros[4].Value = this.Destino;
                parametros[4].ParameterName = "@ems_destino";

                parametros[5] = new SqlParameter();
                parametros[5].SqlDbType = System.Data.SqlDbType.Date;
                parametros[5].Direction = System.Data.ParameterDirection.Input;
                parametros[5].Value = this.DtInicioVig;
                parametros[5].ParameterName = "@ems_inicio_vigencia";

                parametros[6] = new SqlParameter();
                parametros[6].SqlDbType = System.Data.SqlDbType.Date;
                parametros[6].Direction = System.Data.ParameterDirection.Input;
                parametros[6].Value = this.DtFinalVig;
                parametros[6].ParameterName = "@ems_fim_vigencia";

                parametros[7] = new SqlParameter();
                parametros[7].SqlDbType = System.Data.SqlDbType.Decimal;
                parametros[7].Direction = System.Data.ParameterDirection.Input;
                parametros[7].Value = this.Premio;
                parametros[7].ParameterName = "@ems_premio_licitacao";

                parametros[8] = new SqlParameter();
                parametros[8].SqlDbType = System.Data.SqlDbType.Int;
                parametros[8].Direction = System.Data.ParameterDirection.Input;
                parametros[8].Value = this.IdStatus;
                parametros[8].ParameterName = "@ems_status";

                parametros[9] = new SqlParameter();
                parametros[9].SqlDbType = System.Data.SqlDbType.Bit;
                parametros[9].Direction = System.Data.ParameterDirection.Input;
                parametros[9].Value = this.Deletado;
                parametros[9].ParameterName = "@ems_deletado";

                parametros[10] = new SqlParameter();
                parametros[10].SqlDbType = System.Data.SqlDbType.NVarChar;
                parametros[10].Direction = System.Data.ParameterDirection.Input;
                parametros[10].Value = this.CpfPassageiro;
                parametros[10].ParameterName = "@ems_cpf";

                parametros[11] = new SqlParameter();
                parametros[11].SqlDbType = System.Data.SqlDbType.NVarChar;
                parametros[11].Direction = System.Data.ParameterDirection.Input;
                parametros[11].Value = this.NumVoucher;
                parametros[11].ParameterName = "@ems_nr_voucher";

                #endregion

                DaoSQL oDaoInserir = new DaoSQL();
                oDaoInserir.SetUpdate("spGravaEmissaoVoucher", parametros);
                oDaoInserir.CloseConexao();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ConsultaTaxa()
        {
            try
            {
                DaoSQL oDaoConsulta = new DaoSQL();

                SqlDataReader dr = oDaoConsulta.GetSelect("SELECT * FROM Taxas");

                while (dr.Read())
                {
                    this.idTaxa = int.Parse(dr["tx_id"].ToString());
                    this.codTaxa = int.Parse(dr["tx_cod"].ToString());
                    this.valorTaxa = decimal.Parse(dr["tx_valor"].ToString());
                }

                oDaoConsulta.CloseConexao();
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public string SalvarStatusVoucher(int idVoucher, int idStatus, int idUsuario)
        {
            DaoSQL oDao = new DaoSQL();
            string[] param = new string[4];
            param[0] = idVoucher.ToString();
            param[1] = idStatus.ToString();
            param[2] = null;
            param[3] = idUsuario.ToString();

            string str_retorno = "";
            SqlDataReader dr = oDao.GetSelectSp("spAlteraEmissaoVoucher", param);

            while (dr.Read())
            {
                str_retorno = dr["retorno"].ToString();
            }

            return "";
        }


        public string SalvarStatusVoucher(int idVoucher, int idStatus, DateTime? dtCancelamento, int idUsuario)
        {
            DaoSQL oDao = new DaoSQL();
            string[] param = new string[4];
            param[0] = idStatus.ToString();
            param[1] = idVoucher.ToString();
            param[2] = dtCancelamento.ToString();
            param[3] = idUsuario.ToString();

            string str_retorno = "";

            try
            {

                SqlDataReader dr = oDao.GetSelectSp("spAlteraEmissaoVoucher", param);

                while (dr.Read())
                {
                    str_retorno = dr["retorno"].ToString();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return str_retorno;
        }

        public string ListarMesesVouchersDisponiveisPorAno(string ano)
        {
            DateTime dataInicial = Convert.ToDateTime("01/01/" + ano);
            DateTime dataFinal = Convert.ToDateTime("31/12/" + ano);

            DaoSQL oDao = new DaoSQL();
            string[] param = new string[2];
            param[0] = dataInicial.ToString();
            param[1] = dataFinal.ToString();

            string strPeriodo = "";

            SqlDataReader dr = oDao.GetSelectSp("spListarPeriodoVoucherPorAno", param);

            while (dr.Read())
            {
                var MonthAux = Convert.ToInt16(dr["ems_meses_disponiveis"].ToString());
                switch (MonthAux)
                {
                    case 1:
                        strPeriodo += "janeiro;";
                        break;
                    case 2:
                        strPeriodo += "fevereiro;";
                        break;
                    case 3:
                        strPeriodo += "março;";
                        break;
                    case 4:
                        strPeriodo += "abril;";
                        break;
                    case 5:
                        strPeriodo += "maio;";
                        break;
                    case 6:
                        strPeriodo += "junho;";
                        break;
                    case 7:
                        strPeriodo += "julho;";
                        break;
                    case 8:
                        strPeriodo += "agosto;";
                        break;
                    case 9:
                        strPeriodo += "setembro;";
                        break;
                    case 10:
                        strPeriodo += "outubro;";
                        break;
                    case 11:
                        strPeriodo += "novembro;";
                        break;
                    case 12:
                        strPeriodo += "dezembro;";
                        break;
                }
            }

            oDao.CloseConexao();
            return strPeriodo;
        }


        //8854
        public List<string> ListarDataCadastro_groupByAno()
        {
            DaoSQL oDao = new DaoSQL();
            try
            {
                SqlDataReader dr = oDao.GetSelectSp("spListarDataCadastro_groupByAno");
                List<string> listAnos = new List<string>();

                while (dr.Read())
                {
                    listAnos.Add(dr["data_cad_anos"].ToString());
                }
                oDao.CloseConexao();
                return listAnos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Voucher> ListarVouchersDisponiveisPorMesAno(string ano, string mes)
        {
            DateTime dataInicial = Convert.ToDateTime("01/" + mes + "/" + ano);
            //Identificando o ultimo dia do mês.
            DateTime dataFinal = Convert.ToDateTime("01/" + mes + "/" + ano);
            dataFinal = dataFinal.AddMonths(1);
            dataFinal = dataFinal.AddDays(-1);

            DaoSQL oDao = new DaoSQL();
            string[] param = new string[2];
            param[0] = dataInicial.ToString();
            param[1] = dataFinal.ToString();

            List<Voucher> Lista = new List<Voucher>();

            try
            { 
                SqlDataReader dr = oDao.GetSelectSp("spListarVoucherPorMesAno", param);

                while (dr.Read())
                {
                    var itemVoucher = new Voucher();

                    itemVoucher.IdVoucher = Convert.ToInt16(dr["ems_id"].ToString());
                    itemVoucher.NomePassageiro = dr["ems_nome_passageiro"].ToString();
                    if (!string.IsNullOrEmpty(dr["ems_cpf"].ToString()))
                        itemVoucher.CpfPassageiro = Convert.ToUInt64(dr["ems_cpf"].ToString()).ToString(@"000\.000\.000\-00");
                    //itemVoucher.CpfPassageiro = dr["ems_cpf"].ToString();
                    itemVoucher.NumVoucher = dr["ems_nr_voucher"].ToString();
                    itemVoucher.Diaria = Convert.ToInt16(dr["ems_diaria"].ToString());
                    itemVoucher.Destino = dr["ems_destino"].ToString();
                    itemVoucher.strDtInicioVig = Convert.ToDateTime(dr["ems_inicio_vigencia"].ToString()).ToShortDateString();
                    itemVoucher.strDtFinalVig = Convert.ToDateTime(dr["ems_fim_vigencia"].ToString()).ToShortDateString();
                    itemVoucher.strDtCadastro = Convert.ToDateTime(dr["ems_dt_cadastro"].ToString()).ToShortDateString();
                    itemVoucher.Premio = Convert.ToDecimal(dr["ems_premio_licitacao"].ToString());
                    itemVoucher.IdStatus = Convert.ToInt16(dr["ems_status"].ToString());

                    Lista.Add(itemVoucher);
                }

                oDao.CloseConexao();
            }
            catch(Exception e)
            {
                throw e;
            }

            
            return Lista;
        }

        public void DetalhaVoucherPorID(int idVoucher)
        {
            DaoSQL oDao = new DaoSQL();
            string[] param = new string[1];
            param[0] = idVoucher.ToString();

            SqlDataReader dr = oDao.GetSelectSp("spConsultarVoucherPorId", param);

            while (dr.Read())
            {
                this.IdVoucher = Convert.ToInt16(dr["ems_id"].ToString());
                this.NomePassageiro = dr["ems_nome_passageiro"].ToString();
                if (!string.IsNullOrEmpty(dr["ems_cpf"].ToString()))
                    this.CpfPassageiro = Convert.ToUInt64(dr["ems_cpf"].ToString()).ToString(@"000\.000\.000\-00");
                //itemVoucher.CpfPassageiro = dr["ems_cpf"].ToString();
                this.NumVoucher = dr["ems_nr_voucher"].ToString();
                this.Diaria = Convert.ToInt16(dr["ems_diaria"].ToString());
                this.Destino = dr["ems_destino"].ToString();
                this.strDtInicioVig = Convert.ToDateTime(dr["ems_inicio_vigencia"].ToString()).ToShortDateString();
                this.strDtFinalVig = Convert.ToDateTime(dr["ems_fim_vigencia"].ToString()).ToShortDateString();
                this.strDtCadastro = Convert.ToDateTime(dr["ems_dt_cadastro"].ToString()).ToShortDateString();
                this.Premio = Convert.ToDecimal(dr["ems_premio_licitacao"].ToString());
                this.IdStatus = Convert.ToInt16(dr["ems_status"].ToString());

            }
            oDao.CloseConexao();
        }

    }


}
