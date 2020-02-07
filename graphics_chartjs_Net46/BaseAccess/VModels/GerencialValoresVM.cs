namespace BaseAccess.VModels
{
    public class GerencialVM
    {
        public int? PeriodoMes { get; set; }
        public int? QtdSeguradosSaude { get; set; }
        public int? Produto { get; set; }
        public int? QtdSeguradosOdonto { get; set; }
        public int? QtdTitularSaude { get; set; }
        public int? QtdDependenteSaude { get; set; }
        public int? QtdTitularOdonto { get; set; }
        public int? QtdDependenteOdonto { get; set; }
        public string ConsolidPagamentoRecebidoSaude { get; set; }
        public string ConsolidPagamentoPendenteSaude { get; set; }
        public string ConsolidPagamentoRecebidoOdonto { get; set; }
        public string ConsolidPagamentoPendenteOdonto { get; set; }
        public string ConsolidFaturamentoSaude { get; set; }
        public string ConsolidFaturamentoOdonto { get; set; }
        public string ConsolidPremioMes{ get; set; }
        public string ConsolidValorPremioSaude { get; set; }
        public string ConsolidPremioQuantidadeSaude { get; set; }
        public string ConsolidValorPremioOdonto { get; set; }
        public string ConsolidPremioQuantidadeOdonto { get; set; }

    }
}
