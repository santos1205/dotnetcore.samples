using API.Viagem.Domain;
using API.Viagem.Domain.DTOs;
using API.Viagem.Domain.Enums;
using API.Viagem.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace API.Viagem.Infrastructure.Repository
{
    public class CotacaoRepository : EFRepository<TblViagemCotacoes>, ICotacaoRepository
    {
        private readonly IPassageiroRepository _passageiroRepository;

        public CotacaoRepository(MultCalcSegContext dbContext, IPassageiroRepository passageiroRepository) : base(dbContext)
        {
            _passageiroRepository = passageiroRepository;
        }

        public TblViagemCotacoes SalvarCotacao(CotacaoEnvioDTO cotacaoEnvioDTO, OrigemParceiroEnum origemParceiro)
        {
            try
            {
                TblViagemCotacoes viagemCotacoes = new TblViagemCotacoes()
                {
                    CotIdCorretor = cotacaoEnvioDTO.IdCorretor,
                    CotIdEstipulante = cotacaoEnvioDTO.IdEstipulante,
                    CotIdPdv = cotacaoEnvioDTO.IdPdv,
                    CotDtCotacao = DateTime.Now,
                    CotDesIdDestino = cotacaoEnvioDTO.Destino,
                    CotDtPartida = cotacaoEnvioDTO.DtPartida,
                    CotDtRetorno = cotacaoEnvioDTO.DtRetorno,
                    CotNomeUsuarioEmissao = cotacaoEnvioDTO.NomeUsuarioEmissao,
                    CotFpgIdFormaPagamento = cotacaoEnvioDTO.FormaPagamento,
                    CotOrpIdOrigemParceiro = (int)origemParceiro
                };

                TblViagemCotacoes retornoCotacao = _dbContext.Set<TblViagemCotacoes>().Add(viagemCotacoes).Entity;


                foreach (CotacaoEnvioPassageiroDTO passageiro in cotacaoEnvioDTO.CotacaoEnvioPassageiroDTO)
                {
                    TblViagemPassageiros passageiroDB = _passageiroRepository.GetPassageirosPorCPF(passageiro.CPF);
                    int retornoIDPassageiros = 0;

                    if (passageiroDB != null)
                    {
                        passageiroDB.PsgNome = passageiro.Nome;
                        passageiroDB.PsgSobrenome = passageiro.SobreNome;
                        passageiroDB.PsgTelefone = passageiro.Telefone;
                        passageiroDB.PsgEmail = passageiro.Email;
                        passageiroDB.PsgDtNascimento = passageiro.DataNascimento;

                        _dbContext.Entry(passageiroDB).State = EntityState.Modified;
                        retornoIDPassageiros = passageiroDB.PsgIdPassageiro;
                    }
                    else
                    {
                        passageiroDB = new TblViagemPassageiros()
                        {
                            PsgCpf = passageiro.CPF,
                            PsgNome = passageiro.Nome,
                            PsgSobrenome = passageiro.SobreNome,
                            PsgTelefone = passageiro.Telefone,
                            PsgEmail = passageiro.Email,
                            PsgDtNascimento = passageiro.DataNascimento
                        };
                        retornoIDPassageiros = _dbContext.Set<TblViagemPassageiros>().Add(passageiroDB).Entity.PsgIdPassageiro;
                    }


                    TblViagemPassageirosCotacoes tblViagemPassageirosCotacoes = new TblViagemPassageirosCotacoes()
                    {
                        PctCotIdCotacao = retornoCotacao.CotIdCotacao,
                        PctPsgIdPassageiro = retornoIDPassageiros,
                        PctBolPassageiroPrincipal = passageiro.PassageiroPrincipal
                    };

                    _dbContext.Set<TblViagemPassageirosCotacoes>().Add(tblViagemPassageirosCotacoes);
                }

                _dbContext.SaveChanges();

                return retornoCotacao;
            }
            catch (Exception exc)
            {
                throw exc;
            }            
        }

        public void SalvarRetornoCotacao(TblViagemCotacoesResultados tblViagemCotacoesResultados)
        {
            try
            {
                TblViagemCotacoesResultados retornoCotacao = _dbContext.Set<TblViagemCotacoesResultados>().Add(tblViagemCotacoesResultados).Entity;
                _dbContext.SaveChanges();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
