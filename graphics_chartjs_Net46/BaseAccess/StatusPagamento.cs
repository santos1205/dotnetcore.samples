//------------------------------------------------------------------------------
// <auto-generated>
//    O código foi gerado a partir de um modelo.
//
//    Alterações manuais neste arquivo podem provocar comportamento inesperado no aplicativo.
//    Alterações manuais neste arquivo serão substituídas se o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BaseAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class StatusPagamento
    {
        public StatusPagamento()
        {
            this.Pagamento = new HashSet<Pagamento>();
        }
    
        public int spg_id { get; set; }
        public string spg_nome { get; set; }
        public string spg_descricao { get; set; }
        public bool spg_ativo { get; set; }
    
        public virtual ICollection<Pagamento> Pagamento { get; set; }
    }
}
