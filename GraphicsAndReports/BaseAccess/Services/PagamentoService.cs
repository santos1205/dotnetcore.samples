using BaseAccess.Enums;
using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace BaseAccess.Services
{
    public class PagamentoService
    {
        public static ICollection<Pagamento> ListarPorParams(
            DateTime DtInicial,
            DateTime DtFinal,
            string nrCarteira,
            int? IdPlano,
            int? crm,
            int? IdStatus,
            string cpf,
            string nome,
            Usuario Usuario = null
        )
        {
            var ctx = new SindicatoMedicoEntities();
            var Segurados = new List<Segurado>();
            var Paga = new List<Pagamento>();
            var pags = new List<Pagamento>();
            if (Usuario != null)
                LogService.CapturaOpcaoLog(Usuario, AcaoEnum.ConsultarBaixaPagamento.ToString());
            else
                return new List<Pagamento>();

            try
            {
                if (IdPlano == 1) // apenas odonto
                    Paga = ctx.Pagamento.Where(x => x.Plano.pla_tpp_id == 1 && x.Segurado.seg_ativo == true).ToList();
           
                if (IdPlano == 2 || IdPlano == null) // se for odonto/saude ou se não estiver preenchidp
                    Paga = ctx.Pagamento.Where(x => x.Segurado.seg_ativo == true).ToList();

                if (IdPlano == 3) // Apenas odonto
                    Paga = ctx.Pagamento.Where(x => x.Plano.pla_tpp_id == 2 && x.Segurado.seg_ativo == true).ToList();

                if (!string.IsNullOrEmpty(cpf))
                    Paga = Paga.Where(x => x.Segurado.seg_cpf == cpf).ToList();

                if (!string.IsNullOrEmpty(nome))
                    Paga = Paga.Where(x => x.Segurado.seg_nome.ToLower().StartsWith(nome.ToLower())).ToList();

                 if (crm != null)
                    Paga = Paga.Where(x => x.Segurado.seg_crm == crm).ToList();

                 if (!string.IsNullOrEmpty(nrCarteira))
                    Paga = Paga.Where(x => x.Segurado.seg_numero_carteira == nrCarteira).ToList();

                foreach (var pag in Paga)
                {
                    if (IdStatus != null & IdStatus != 0)
                        if (pag.StatusPagamento.spg_id != IdStatus)
                            continue;

                    if (DtInicial != null && DtFinal != null)
                        // Se a data de vencimento n estiver no período, passa para o proximo registro. 
                        if ((DtInicial != DateTime.MinValue) && (DtFinal != DateTime.MinValue))
                            if (!(pag.pag_data_vencimento >= DtInicial && pag.pag_data_vencimento <= DtFinal))
                                continue;
                    if ((DtInicial != DateTime.MinValue) && (DtFinal != DateTime.MinValue))
                        if (DtInicial != null && DtFinal == null)
                            // Se a data de vencimento n estiver no período, passa para o proximo registro.
                            if (!(pag.pag_data_vencimento >= DtInicial))
                                continue;
                    if ((DtInicial != DateTime.MinValue) && (DtFinal != DateTime.MinValue))
                        if (DtInicial != null && DtFinal == null)
                            // Se a data de vencimento n estiver no período, passa para o proximo registro.
                            if (!(pag.pag_data_vencimento >= DtInicial))
                                continue;
                    if ((DtInicial != DateTime.MinValue) && (DtFinal != DateTime.MinValue))
                        if (DtInicial == null && DtFinal != null)
                            // Se a data de vencimento n estiver no período, passa para o proximo registro.
                            if (!(pag.pag_data_vencimento <= DtFinal))
                                continue;

                    pags.Add(pag);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pags;
        }

        public static Pagamento ConsultarPagamentoPorId(int Id)
        {
            var ctx = new SindicatoMedicoEntities();

            var Pagamento = ctx.Pagamento.Where(x => x.pag_id == Id).FirstOrDefault();
            return Pagamento;
        }

        public static void SalvarBaixaPagamento(Pagamento Pagamento, Usuario Usuario = null)
        {
            if (Usuario == null)
                return;

            var ctx = new SindicatoMedicoEntities();
            bool updPag = false;

            try
            {
                var BPagSeg = ctx.Pagamento.FirstOrDefault(x => x.pag_id == Pagamento.pag_id);

                // Se existe pag. no respectivo vencimento, atualiza, senão adiciona
                if (BPagSeg != null)
                    updPag = true;
                else
                    BPagSeg = new Pagamento();

                BPagSeg.pag_data_pagamento = Pagamento.pag_data_pagamento;
                BPagSeg.pag_id = Pagamento.pag_id;
                BPagSeg.pag_data_baixa_pagamento = DateTime.Now;
                BPagSeg.pag_timestamp_baixa = DateTime.Now;
                BPagSeg.pag_identificacao = Pagamento.pag_identificacao;
                BPagSeg.pag_valor_recebido = Pagamento.pag_valor_recebido;
                BPagSeg.pag_juros = Pagamento.pag_juros;
                BPagSeg.pag_spg_id = (int)PagamentoStatusEnum.Recebido;
                if (!updPag)
                    ctx.Pagamento.Add(BPagSeg);
                ctx.SaveChanges();


                // registra log
                if (updPag)
                    LogService.CapturaOpcaoLog(Usuario, AcaoEnum.AlteracaoBaixaPagamentoManual.ToString(), (int)BPagSeg.pag_seg_id);
                else
                    LogService.CapturaOpcaoLog(Usuario, AcaoEnum.BaixaPagamentoManual.ToString(), (int)BPagSeg.pag_seg_id);
            }
            catch (DbEntityValidationException e)
            {
                string msgError = "Erro ao salvar baixa de pagamento ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;
                throw new Exception(msgError);
            }



        }

        public static PagamentoVM Serialize(Pagamento Pagamento)
        {
            var ctx = new SindicatoMedicoEntities();
            var PagVM = new PagamentoVM();

            if (Pagamento != null)
            {
                PagVM.Id = Pagamento.pag_id;
                PagVM.NomeSegurado = Pagamento.Segurado.seg_nome;
                PagVM.CpfSegurado = Pagamento.Segurado.seg_cpf;
                PagVM.CrmTitular = Pagamento.Segurado.seg_crm + "-" + Pagamento.Segurado.seg_crm_estado;
                PagVM.Plano = Pagamento.Plano.pla_nome;
                if (Pagamento.pag_data_vencimento != null)
                    PagVM.DtVencimento = Pagamento.pag_data_vencimento.ToShortDateString();
                if (Pagamento.pag_data_pagamento != null)
                    PagVM.DtPagamento = DateTime.Parse(Pagamento.pag_data_pagamento.ToString()).ToShortDateString();
                PagVM.VlPremio = string.Format("{0:N2}", Pagamento.pag_valor_recebido);      // formatar valor financeiro
                PagVM.VlJuros = string.Format("{0:N2}", Pagamento.pag_juros);      // formatar valor financeiro
                PagVM.VlVencimento = string.Format("{0:N2}", Pagamento.pag_valor_vencimento);      // formatar valor financeiro
                PagVM.FPagamento = Pagamento.Segurado.FormaPagamento.for_descricao;
                PagVM.Status = Pagamento.StatusPagamento.spg_nome;
                PagVM.NrDocumentoPagamento = Pagamento.pag_identificacao;
            }

            return PagVM;
        }

        public static ICollection<PagamentoVM> Serialize(ICollection<Pagamento> Pagamentos)
        {
            List<PagamentoVM> lista = new List<PagamentoVM>();
            var ctx = new SindicatoMedicoEntities();

            int maxCount = Pagamentos.Count();

            foreach (var item in Pagamentos)
            {
                try
                {
                    var Pag = new PagamentoVM();

                    Pag.Id = item.pag_id;

                    if (item.pag_pla_id != null)
                    {
                        Pag.Produto = item.Plano.pla_tpp_id == 1 ? "Saúde" : "Odonto";
                    }
                    Pag.NomeSegurado = item.Segurado.seg_nome;
                    Pag.CpfSegurado = item.Segurado.seg_cpf;
                    Pag.DtVencimento = item.pag_data_vencimento.ToShortDateString();
                    Pag.VlPremio = string.Format("{0:N2}", item.pag_valor_vencimento);      // formatar valor financeiro
                    Pag.Plano = item.Plano.pla_nome;
                    Pag.FPagamento = item.Segurado.FormaPagamento.for_descricao;

                    if (item.pag_data_pagamento != null)
                    {
                        DateTime dataPagamento = Convert.ToDateTime(item.pag_data_pagamento);
                        Pag.DtPagamento = dataPagamento.ToShortDateString();
                    }

                    // Carregar o nr. de dependentes
                    int NrDeps = ctx.Dependente.Where(x => x.dep_cpf_titular == item.Segurado.seg_cpf && x.dep_ativo == true).Count();
                    Pag.NrDependentes = NrDeps;

                    Pag.Status = item.StatusPagamento.spg_nome;

                    lista.Add(Pag);
                }
                catch (Exception ex)
                { }
            }

            return lista;
        }

    }
}
