using Microsoft.EntityFrameworkCore;
using MovileWeb.DataAccess;
using MovileWeb.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MovileWeb.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {

        private CEPContext _dbContext;

        public EnderecoRepository(CEPContext DBContext)
        {
            _dbContext = DBContext;
        }

        public Endereco ConsultarEnderecoPorCep(string Cep)
        {

            SqlTransaction transaction = null;
            Endereco Endereco = new Endereco();

            try
            {
                using (SqlConnection sql = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
                {
                    sql.Open();
                    transaction = sql.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand("spEndereco_SelectByCep", sql, transaction))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@strCep", Cep));

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {                
                                Endereco.Cep = reader[0]?.ToString();
                                Endereco.Logradouro = reader[1]?.ToString();
                                Endereco.Estado = reader[2]?.ToString();
                                Endereco.Bairro = reader[3]?.ToString();
                                Endereco.Cidade = reader[4]?.ToString();
                            }
                        }
                    }
                }

                return Endereco;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
