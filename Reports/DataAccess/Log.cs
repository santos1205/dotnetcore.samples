using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class Log
    {
        public string Descricao { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Data { get; set; }
        public string StatusDe { get; set; }
        public string StatusPara { get; set; }
        public bool Deletado { get; set; }

        public void Login()
        {
            this.Data = DateTime.Now;
            this.Descricao = "Login com sucesso";
            this.Inserir();
        }

        public void LoginSemSucesso(string tentativaLogin)      //CPF
        {
            this.Data = DateTime.Now;
            this.Descricao = "Tentativa de Login sem sucesso. Login: " + tentativaLogin;
            this.StatusDe = "";
            this.StatusPara = "";
            this.Inserir();
        }

        public void Cancelamento()
        {
            this.Data = DateTime.Now;
            this.Descricao = "Cancelamento";
            this.Inserir();
        }

        private void Inserir()
        {
            try
            {

                #region Inserção dos parametros

                System.Data.SqlClient.SqlParameter[] parametros = new SqlParameter[6];

                parametros[0] = new SqlParameter();
                parametros[0].SqlDbType = System.Data.SqlDbType.VarChar;
                parametros[0].Direction = System.Data.ParameterDirection.Input;
                parametros[0].Value = this.Descricao;
                parametros[0].ParameterName = "@descricao";

                parametros[1] = new SqlParameter();
                parametros[1].SqlDbType = System.Data.SqlDbType.DateTime;
                parametros[1].Direction = System.Data.ParameterDirection.Input;
                parametros[1].Value = this.Data;
                parametros[1].ParameterName = "@data";

                parametros[2] = new SqlParameter();
                parametros[2].SqlDbType = System.Data.SqlDbType.VarChar;
                parametros[2].Direction = System.Data.ParameterDirection.Input;
                parametros[2].Value = this.StatusDe;
                parametros[2].ParameterName = "@log_de";

                parametros[3] = new SqlParameter();
                parametros[3].SqlDbType = System.Data.SqlDbType.VarChar;
                parametros[3].Direction = System.Data.ParameterDirection.Input;
                parametros[3].Value = this.StatusPara;
                parametros[3].ParameterName = "@log_para";
                
                parametros[4] = new SqlParameter();
                parametros[4].SqlDbType = System.Data.SqlDbType.Int;
                parametros[4].Direction = System.Data.ParameterDirection.Input;
                parametros[4].Value = this.IdUsuario;
                parametros[4].ParameterName = "@idUsuario";

                parametros[5] = new SqlParameter();
                parametros[5].SqlDbType = System.Data.SqlDbType.Bit;
                parametros[5].Direction = System.Data.ParameterDirection.Input;
                parametros[5].Value = this.Deletado;
                parametros[5].ParameterName = "@deletado";


                #endregion

                DaoSQL oDaoInserir = new DaoSQL();
                oDaoInserir.SetUpdate("spInserirLog", parametros);
                oDaoInserir.CloseConexao();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
