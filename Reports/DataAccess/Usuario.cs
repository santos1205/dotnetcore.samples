using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace DataAccess
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public bool Deletado { get; set; }
        public bool Aprovado { get; set; }
        public string Senha { get; set; }

        public void Inserir()
        {
            try
            {

                #region Inserção dos parametros

                System.Data.SqlClient.SqlParameter[] parametros = new SqlParameter[9];

                parametros[0] = new SqlParameter();
                parametros[0].SqlDbType = System.Data.SqlDbType.VarChar;
                parametros[0].Direction = System.Data.ParameterDirection.Input;
                parametros[0].Value = this.Nome;
                parametros[0].ParameterName = "@nome";

                parametros[1] = new SqlParameter();
                parametros[1].SqlDbType = System.Data.SqlDbType.Char;
                parametros[1].Direction = System.Data.ParameterDirection.Input;
                parametros[1].Value = this.Genero;
                parametros[1].ParameterName = "@genero";

                parametros[2] = new SqlParameter();
                parametros[2].SqlDbType = System.Data.SqlDbType.Date;
                parametros[2].Direction = System.Data.ParameterDirection.Input;
                parametros[2].Value = this.DataNascimento;
                parametros[2].ParameterName = "@dataNascimento";

                parametros[3] = new SqlParameter();
                parametros[3].SqlDbType = System.Data.SqlDbType.Char;
                parametros[3].Direction = System.Data.ParameterDirection.Input;
                parametros[3].Value = this.Cpf;
                parametros[3].ParameterName = "@Cpf";

                parametros[4] = new SqlParameter();
                parametros[4].SqlDbType = System.Data.SqlDbType.VarChar;
                parametros[4].Direction = System.Data.ParameterDirection.Input;
                parametros[4].Value = this.Email;
                parametros[4].ParameterName = "@email";
                                
                parametros[5] = new SqlParameter();
                parametros[5].SqlDbType = System.Data.SqlDbType.DateTime;
                parametros[5].Direction = System.Data.ParameterDirection.Input;
                parametros[5].Value = this.DataCadastro;
                parametros[5].ParameterName = "@dataCadastro";

                parametros[6] = new SqlParameter();
                parametros[6].SqlDbType = System.Data.SqlDbType.VarChar;
                parametros[6].Direction = System.Data.ParameterDirection.Input;
                parametros[6].Value = this.Senha;
                parametros[6].ParameterName = "@senha";

                parametros[7] = new SqlParameter();
                parametros[7].SqlDbType = System.Data.SqlDbType.Char;
                parametros[7].Direction = System.Data.ParameterDirection.Input;
                parametros[7].Value = this.Telefone;
                parametros[7].ParameterName = "@telefone";

                parametros[8] = new SqlParameter();
                parametros[8].SqlDbType = System.Data.SqlDbType.Bit;
                parametros[8].Direction = System.Data.ParameterDirection.Input;
                parametros[8].Value = this.Deletado;
                parametros[8].ParameterName = "@deletado";

                
                #endregion

                DaoSQL oDaoInserir = new DaoSQL();
                oDaoInserir.SetUpdate("spInserirUsuario", parametros);
                oDaoInserir.CloseConexao();
                //carega id novo.
                this.ConsultarPorCpf(this.Cpf);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool VerificaToken(string token)
        {
            DaoSQL oDao = new DaoSQL();
            string[] param = new string[1];

            //verificador máscara: para evitar que seja autenticado direto pelo token.
            param[0] = token + "1205";

            bool tokenValido = false;

            //Verificação do token.
            //string vlrcpf = token.Substring(0, 11);
            //string nrRand = token.Substring(11, 4);
            SqlDataReader dr = oDao.GetSelectSp("spVerificaToken", param);

            while (dr.Read())
            {
                tokenValido = true;
                this.Cpf = dr["usr_cpf"].ToString();
            }

            oDao.CloseConexao();
            return tokenValido;
        }

        public bool VerificaLogin(string user, string senha)
        {
            DaoSQL oDao = new DaoSQL();
            string[] param = new string[2];
            param[0] = user;
            param[1] = senha;
            bool logado = false;

            SqlDataReader dr = oDao.GetSelectSp("spVerificaLogin", param);

            while (dr.Read())
            {
                logado = true;
                //Carrega os dados do usuario para carregar na Session                
                this.ConsultarPorCpf(user);

                HttpContext.Current.Session["Usuario"] = this;
                //var UsuarioLogado = HttpContext.Current.Session["Usuario"] as Usuario;
            }

            oDao.CloseConexao();
            return logado;
        }

        public void ConsultarPorCpf(string cpf)
        {
            DaoSQL oDao = new DaoSQL();
            string[] param = new string[1];
            param[0] = cpf;


            SqlDataReader dr = oDao.GetSelectSp("spConsultarUsuarioPorCpf", param);

            while (dr.Read())
            {
                string auxText = dr["usr_aprovado"].ToString();
                this.IdUsuario = int.Parse(dr["usr_id"].ToString());                
                this.Nome = dr["usr_nome"].ToString();
                this.Genero = dr["usr_genero"].ToString();
                this.DataNascimento = DateTime.Parse(dr["usr_dt_nascimento"].ToString());
                this.Cpf = dr["usr_cpf"].ToString();
                this.Email = dr["usr_email"].ToString();                
                this.DataCadastro = DateTime.Parse(dr["usr_dt_cadastro"].ToString());                                
                this.Senha = dr["usr_senha"].ToString();
                this.Aprovado = dr["usr_aprovado"].ToString() == "True" ? true : false;                                
                this.Deletado = dr["usr_deletado"].ToString() == "True" ? true : false;                                
            }
            oDao.CloseConexao();
        }

        //Atualiza senha pelo cpf.
        public void AtualizaSenha(string novaSenha)
        {
            System.Data.SqlClient.SqlParameter[] parametros = new SqlParameter[2];

            parametros[0] = new SqlParameter();
            parametros[0].SqlDbType = System.Data.SqlDbType.Char;
            parametros[0].Direction = System.Data.ParameterDirection.Input;
            parametros[0].Value = this.Cpf;
            parametros[0].ParameterName = "@cpf";

            parametros[1] = new SqlParameter();
            parametros[1].SqlDbType = System.Data.SqlDbType.NVarChar;
            parametros[1].Direction = System.Data.ParameterDirection.Input;
            parametros[1].Value = novaSenha;
            parametros[1].ParameterName = "@novaSenha";



            DaoSQL oDaoInserir = new DaoSQL();
            oDaoInserir.SetUpdate("spAtualizaSenhaUsuarioPorCpf", parametros);
            oDaoInserir.CloseConexao();
        }

    }
}
