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
    
    public partial class MelhorDiaPagamento
    {
        public MelhorDiaPagamento()
        {
            this.Segurado = new HashSet<Segurado>();
        }
    
        public int mdp_id { get; set; }
        public byte mdp_dia { get; set; }
    
        public virtual ICollection<Segurado> Segurado { get; set; }
    }
}
