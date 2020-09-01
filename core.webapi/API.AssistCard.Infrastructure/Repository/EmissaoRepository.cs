using API.Viagem.Domain;
using API.Viagem.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace API.Viagem.Infrastructure.Repository
{
    public class EmissaoRepository : EFRepository<TblViagemEmissoes>, IEmissaoRepository
    {
        private readonly IPassageiroRepository _passageiroRepository;

        public EmissaoRepository(MultCalcSegContext dbContext, IPassageiroRepository passageiroRepository) : base(dbContext)
        {
            _passageiroRepository = passageiroRepository;
        }

        public TblViagemEmissoes SalvarEmissao(TblViagemEmissoes viagemEmissoes, List<TblViagemPassageiros> lstViagemPassageiros)
        {
            try
            {
                TblViagemEmissoes retornoEmissao = _dbContext.Set<TblViagemEmissoes>().Add(viagemEmissoes).Entity;

                foreach (TblViagemPassageiros passageiro in lstViagemPassageiros)
                {
                    TblViagemPassageiros passageiroDB = _passageiroRepository.GetById(passageiro.PsgIdPassageiro);

                    //Complementar informações do Passageiro
                    if (passageiroDB != null)
                    {
                        passageiroDB.PsgEndereco = passageiro.PsgEndereco;
                        passageiroDB.PsgCodigoPostal = passageiro.PsgCodigoPostal;
                        passageiroDB.PsgCidade = passageiro.PsgCidade;
                        passageiroDB.PsgNumeroDocumento = passageiro.PsgNumeroDocumento;
                        passageiroDB.PsgEstado = passageiro.PsgEstado;
                        passageiroDB.PsgPaisDomicilio = passageiro.PsgPaisDomicilio;
                        passageiroDB.PsgDadoAdicional1 = passageiro.PsgDadoAdicional1;
                        passageiroDB.PsgDadoAdicional2 = passageiro.PsgDadoAdicional2;
                        passageiroDB.PsgGenero = passageiro.PsgGenero;
                        passageiroDB.PsgBairro = passageiro.PsgBairro;
                        passageiroDB.PsgNumeroEndereco = passageiro.PsgNumeroEndereco;
                        passageiroDB.PsgComplemento = passageiro.PsgComplemento;
                        passageiroDB.PsgNomeContato = passageiroDB.PsgNome;
                        passageiroDB.PsgTelefoneContato = passageiroDB.PsgTelefone;
                        passageiroDB.PsgTipoDocumento = "9";
                        passageiroDB.PsgNacIdNacionalidade = passageiro.PsgNacIdNacionalidade;

                        _dbContext.Entry(passageiroDB).State = EntityState.Modified;
                    }
                }

                _dbContext.SaveChanges();

                return retornoEmissao;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void SalvarRetornoEmissao(TblViagemEmissoesRetorno viagemEmissoesRetorno, List<TblViagemVouchers> lstViagemVouchers)
        {
            try
            {
                TblViagemEmissoesRetorno retornoEmissao = _dbContext.Set<TblViagemEmissoesRetorno>().Add(viagemEmissoesRetorno).Entity;

                foreach (TblViagemVouchers voucher in lstViagemVouchers)
                {
                    voucher.VchEmrIdEmissaoRetorno = retornoEmissao.EmrIdEmissaoRetorno;
                    TblViagemVouchers retornoVoucher = _dbContext.Set<TblViagemVouchers>().Add(voucher).Entity;
                }

                _dbContext.SaveChanges();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
