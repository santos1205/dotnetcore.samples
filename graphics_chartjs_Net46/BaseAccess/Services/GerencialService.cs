using BaseAccess.Enums;
using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseAccess.Services
{
    public class GerencialService
    {
        public static ICollection<Prc_consolidado_segurado_Result> ConsultaConsolidadoSegurado()
        {
            var ctx = new SindicatoMedicoEntities();
            return ctx.Prc_consolidado_segurado().ToList();
        }

        public static ICollection<Prc_consolidado_pagamento_Result> ConsultaConsolidadoPagamento()
        {
            var ctx = new SindicatoMedicoEntities();
            string dataInicial = string.Format("01-{0}-{1}", DateTime.Now.Month, DateTime.Now.Year);
            string dataFinal = string.Format("{0}-{1}-{2}", DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), DateTime.Now.Month, DateTime.Now.Year);            
            return ctx.Prc_consolidado_pagamento(DateTime.Parse(dataInicial), DateTime.Parse(dataFinal)).ToList();
        }


        public static ICollection<Prc_consolidado_faturamento_Result> ConsultaConsolidadoFaturamento()
        {
            var ctx = new SindicatoMedicoEntities();
            string dataInicial = string.Format("01-{0}-{1}", DateTime.Now.Month, DateTime.Now.Year);
            string dataFinal = string.Format("{0}-{1}-{2}", DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), DateTime.Now.Month, DateTime.Now.Year);
            return ctx.Prc_consolidado_faturamento(DateTime.Parse(dataInicial), DateTime.Parse(dataFinal)).ToList();
        }

        public static ICollection<Prc_detalhado_segurado_Result> ConsultaDetalhadoGraficoSegurados()
        {
            var ctx = new SindicatoMedicoEntities();
            return ctx.Prc_detalhado_segurado().ToList();
        }

		public static ICollection<Prc_Consolidado_Premio_Result> ConsultaConsolidadoPremio()
        {
            var ctx = new SindicatoMedicoEntities();
            try
            {
                return ctx.Prc_Consolidado_Premio().ToList();
            }
            catch (Exception ex)
            {
                string erroCausa = ex.Message;
                return ctx.Prc_Consolidado_Premio().ToList();
            }
        }


        public static GerencialVM Serialize(ICollection<Prc_consolidado_segurado_Result> Consolidados)
        {
            var VM = new GerencialVM();
            foreach (var item in Consolidados)
            {
                // Titular
                if (item.vw_TipoPessoa == "T")
                {
                    if (item.vw_Produto == (int)TipoPlanoEnum.Saude)
                        VM.QtdTitularSaude = item.vw_quantidade;
                    if (item.vw_Produto == (int)TipoPlanoEnum.Odonto)
                        VM.QtdTitularOdonto = item.vw_quantidade;
                }
                // Dependente
                if (item.vw_TipoPessoa == "D")
                {
                    if (item.vw_Produto == (int)TipoPlanoEnum.Saude)
                        VM.QtdDependenteSaude = item.vw_quantidade;
                    if (item.vw_Produto == (int)TipoPlanoEnum.Odonto)
                        VM.QtdDependenteOdonto = item.vw_quantidade;
                }
            }
            return VM;
        }

        public static ICollection<GerencialGraficoVM> GraficoSerialize(ICollection<Prc_detalhado_segurado_Result> Detalhados)
        {
            var ListaVM = new List<GerencialGraficoVM>();
            int QtdeTotal = 0;

            for (int i=1;i<=12;i++)
            {
                var VM = new GerencialGraficoVM();
                var DetMes = Detalhados.FirstOrDefault(x => DateTime.Parse(x.vw_periodo.ToString()).Month == i);
                if (DetMes != null)
                {
                    VM.Mes = i;
                    VM.QtdeSaude = DetMes.vw_quantidadesaude;
                    VM.QtdeOdonto = DetMes.vw_quantidadeodonto;
                }else
                {
                    VM.Mes = i;
                    VM.QtdeSaude = 0;
                    VM.QtdeOdonto = 0;
                }

                // nr. total dos segurados
                if (QtdeTotal < (VM.QtdeSaude + VM.QtdeOdonto))
                    QtdeTotal = (int)VM.QtdeSaude + (int)VM.QtdeOdonto;

                ListaVM.Add(VM);
            }
            foreach (var item in ListaVM)
                item.QtdeTotal = QtdeTotal;
            
            return ListaVM;
        }

        public static GerencialVM Serialize(ICollection<Prc_consolidado_pagamento_Result> ConsolidPagamento)
        {
            var VM = new GerencialVM();
            if (ConsolidPagamento.Count <= 0)
            {
                VM.ConsolidPagamentoRecebidoSaude = "0";
                VM.ConsolidPagamentoPendenteSaude = "0";

                VM.ConsolidPagamentoRecebidoOdonto = "0";
                VM.ConsolidPagamentoPendenteOdonto = "0";
            }
            else
            {
                foreach (var item in ConsolidPagamento)
                {
                    // Saúde
                    if (item.vw_produto == (int)TipoPlanoEnum.Saude)
                    {
                        VM.ConsolidPagamentoRecebidoSaude = item.vw_valor_recebido.ToString();
                        VM.ConsolidPagamentoPendenteSaude = item.vw_valor_pendente.ToString();
                    }
                    // Odonto
                    if (item.vw_produto == (int)TipoPlanoEnum.Odonto)
                    {
                        VM.ConsolidPagamentoRecebidoOdonto = item.vw_valor_recebido.ToString();
                        VM.ConsolidPagamentoPendenteOdonto = item.vw_valor_pendente.ToString();
                    }
                }
            }               

            return VM;
        }

        public static GerencialVM Serialize(ICollection<Prc_consolidado_faturamento_Result> Consolidados)
        {
            var VM = new GerencialVM();
            foreach (var item in Consolidados)
            {
                // Saúde
                if (item.vw_produto == (int)TipoPlanoEnum.Saude)
                    VM.ConsolidFaturamentoSaude = item.vw_valor_faturamento.ToString();
                // Odonto
                if (item.vw_produto == (int)TipoPlanoEnum.Odonto)
                    VM.ConsolidFaturamentoOdonto = item.vw_valor_faturamento.ToString();
            }

            return VM;
        }

        public static ICollection<GerencialVM> Serialize(ICollection<Prc_Consolidado_Premio_Result> Consolidados)
        {
            List<GerencialVM> lista = new List<GerencialVM>();

            foreach (var item in Consolidados)
            {
                var Gerencial = new GerencialVM();
                DateTime mes = Convert.ToDateTime(item.vw_periodo);
                Gerencial.ConsolidPremioMes = mes.Month.ToString();
                Gerencial.ConsolidPremioQuantidadeSaude = item.vw_quantidadesaude.ToString();
                Gerencial.ConsolidValorPremioSaude = item.vw_premiosaude.ToString();
                Gerencial.ConsolidPremioQuantidadeOdonto = item.vw_quantidadeodonto.ToString();
                Gerencial.ConsolidValorPremioOdonto = item.vw_premioodonto.ToString();

                lista.Add(Gerencial);
            }

            return lista;
        }
    }
}
