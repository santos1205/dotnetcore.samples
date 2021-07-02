namespace API.Viagem.Domain
{
    public partial class TblViagemVouchers
    {
        public int VchIdVoucher { get; set; }
        public int VchEmrIdEmissaoRetorno { get; set; }
        public int VchPsgIdPassageiro { get; set; }  
        public int VchCodigo { get; set; }
        public string VchNumero { get; set; }
        public decimal VchValor { get; set; }
        public decimal VchValorAssistencia { get; set; }
        public string VchCodigoApolice { get; set; }
        public decimal VchPremioSeguroTotal { get; set; }
        public decimal VchValorIof { get; set; }
        public byte VchMoeda { get; set; }
        public decimal VchCambio { get; set; }
        public byte VchTipoDocumento { get; set; }
        public string VchNumeroDocumento { get; set; }
        public string VchNomeCliente { get; set; }
        public byte VchEstadoVoucher { get; set; }
        public bool VchBlnAtivo { get; set; }
        public decimal VchAcrescimoFinanceiro { get; set; }
        public decimal VchValorTaxaGateway { get; set; }

        public virtual TblViagemEmissoesRetorno VchEmrIdEmissaoRetornoNavigation { get; set; }
        public virtual TblViagemPassageiros VchPsgIdPassageiroNavigation { get; set; }
    }
}
