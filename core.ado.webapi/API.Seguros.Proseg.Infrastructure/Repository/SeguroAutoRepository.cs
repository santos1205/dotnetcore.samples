using API.Seguros.Proseg.Domain.Constants;
using API.Seguros.Proseg.Domain.DTOs;
using API.Seguros.Proseg.Domain.Interfaces.Repository;
using API.Seguros.Proseg.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace API.Seguros.Proseg.Infrastructure.Repository
{
    public class SeguroAutoRepository : ISeguroAutoRepository
    {
        private MultiSegurosContext _multiSegurosContext;

        public SeguroAutoRepository(MultiSegurosContext multiSegurosContext)
        {
            _multiSegurosContext = multiSegurosContext;
        }

        public List<RetornoSeguroAutoDTO> ListSeguroAutoPorCPF(string cpf)
        {
            SqlTransaction transaction = null;
            try
            {
                List<RetornoSeguroAutoDTO> listRetornoSeguroAutoDTO = new List<RetornoSeguroAutoDTO>();

                using (SqlConnection sql = new SqlConnection(_multiSegurosContext.Database.GetDbConnection().ConnectionString))
                {
                    sql.Open();
                    transaction = sql.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand("stpAuCliCpf", sql, transaction))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Chave", cpf));

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RetornoSeguroAutoDTO retorno = new RetornoSeguroAutoDTO();
                                                                
                                retorno.Cli_Cpf_Cgc = reader[0]?.ToString();
                                retorno.NomeSegurado = reader[2]?.ToString();
                                retorno.Placa = reader[3]?.ToString();
                                retorno.ModeloVeiculo = reader[4]?.ToString();                                
                                retorno.DataInicioVigencia = reader[5]?.ToString();
                                retorno.DataFinalVigencia = reader[6]?.ToString();
                                retorno.NumeroApolice = reader[7]?.ToString();
                                retorno.NomeSeguradora = reader[8]?.ToString();
                                retorno.ValorSeguro = reader[9]?.ToString();
                                retorno.ContatoSeguradora = reader[10]?.ToString();
                                retorno.Celular = reader[11]?.ToString();
                                retorno.CEP = reader[12]?.ToString();
                                retorno.ContatoCorretora = ProsegConstants.WHATSAPP;

                                listRetornoSeguroAutoDTO.Add(retorno);

                            }
                        }
                    }
                }

                return listRetornoSeguroAutoDTO;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
