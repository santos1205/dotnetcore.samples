using API.Seguros.Proseg.Domain.Constants;
using API.Seguros.Proseg.Domain.DTOs;
using API.Seguros.Proseg.Domain.Enums;
using API.Seguros.Proseg.Domain.Interfaces.Repository;
using API.Seguros.Proseg.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace API.Seguros.Proseg.Infrastructure.Repository
{
    public class SeguroModalidadeRepository : ISeguroModalidadeRepository
    {

        private MultiSegurosContext _multiSegurosContext;

        public SeguroModalidadeRepository(MultiSegurosContext multiSegurosContext)
        {
            _multiSegurosContext = multiSegurosContext;
        }
        public List<RetornoSeguroDTO> ListSeguroPorCPF_Modalidade(string cpf, int codModalidadeSeg)
        {
            try
            {
                List<RetornoSeguroDTO> listRetornoSeguroResidDTO = new List<RetornoSeguroDTO>();

                using (SqlConnection sql = new SqlConnection(_multiSegurosContext.Database.GetDbConnection().ConnectionString))
                {
                    sql.Open();
                    SqlTransaction transaction = sql.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand("stpRsCliCpf", sql, transaction))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Chave", cpf));
                        cmd.Parameters.Add(new SqlParameter("@CodModalidadeSeg", codModalidadeSeg));

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RetornoSeguroDTO retorno = new RetornoSeguroDTO();
                                
                                    retorno.Cli_Cpf_Cgc = reader[0]?.ToString();
                                    retorno.NomeSegurado = reader[1]?.ToString();
                                    retorno.Email = reader[2]?.ToString();
                                    retorno.DataInicioVigencia = reader[3]?.ToString();
                                    retorno.DataFinalVigencia = reader[4]?.ToString();
                                    retorno.NumeroApolice = reader[5]?.ToString();
                                    retorno.ValorSeguro = reader[6]?.ToString();
                                    retorno.NomeSeguradora = reader[7]?.ToString();
                                    retorno.ContatoSeguradora = reader[8]?.ToString();
                                    // retorno.Endereco = codModalidadeSeg == (int)ModalidadeSeguroEnum.Pet ? reader[15]?.ToString() : reader[14]?.ToString();  // SE FOR PET, PEGA ENDEREÇO DO CLIENTE SENÃO ENDEREÇO DE RISCO
                                    retorno.Endereco = reader[9]?.ToString();
                                    retorno.ContatoCorretora = ProsegConstants.WHATSAPP;
                                
                                listRetornoSeguroResidDTO.Add(retorno);
                            }
                        }
                    }
                }

                return listRetornoSeguroResidDTO;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public List<RetornoSeguroViagemDTO> ListSeguroViagemPorCPF(string cpf)
        {
            try
            {
                List<RetornoSeguroViagemDTO> listRetornoSeguroViagemDTO = new List<RetornoSeguroViagemDTO>();

                using (SqlConnection sql = new SqlConnection(_multiSegurosContext.Database.GetDbConnection().ConnectionString))
                {
                    sql.Open();
                    SqlTransaction transaction = sql.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand("stpRsCliCpf", sql, transaction))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Chave", cpf));
                        cmd.Parameters.Add(new SqlParameter("@CodModalidadeSeg", (int)ModalidadeSeguroEnum.Viagem));

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RetornoSeguroViagemDTO retorno = new RetornoSeguroViagemDTO();

                                retorno.Cli_Cpf_Cgc = reader[0]?.ToString();
                                retorno.NomeSegurado = reader[1]?.ToString();
                                retorno.Email = reader[2]?.ToString();
                                retorno.DataIda = reader[3]?.ToString();
                                retorno.DataVolta = reader[4]?.ToString();
                                retorno.NumeroApolice = reader[5]?.ToString();
                                retorno.ValorSeguro = reader[6]?.ToString();
                                retorno.NomeSeguradora = reader[7]?.ToString();
                                retorno.ContatoSeguradora = reader[8]?.ToString();
                                retorno.Endereco = reader[14]?.ToString();
                                retorno.ContatoCorretora = ProsegConstants.WHATSAPP;

                                listRetornoSeguroViagemDTO.Add(retorno);

                            }
                        }
                    }
                }

                return listRetornoSeguroViagemDTO;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
