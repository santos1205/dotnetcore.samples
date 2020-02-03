using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAccess.VModels
{
    public class PagamentoVM
    {
        public int Id { get; set; }
        public string CpfSegurado { get; set; }
        public string NomeSegurado { get; set; }
        public string CrmTitular { get; set; }
        public int NrDependentes { get; set; }
        public string Plano { get; set; }
        public string DtVencimento { get; set; }
        public string VlPremio { get; set; }
        public string VlVencimento { get; set; }
        public string VlJuros { get; set; }
        public string FPagamento { get; set; }
        public string Produto { get; set; }
        public string DtPagamento { get; set; }
        public string NrDocumentoPagamento { get; set; }
        public string Status { get; set; }
        public string MsgErro { get; set; }
    }
}
