//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BaseAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class LOG_CPC
    {
        public LOG_CPC()
        {
            this.LOG_FAIXA_CPC = new HashSet<LOG_FAIXA_CPC>();
        }
    
        public int CPC_NU { get; set; }
        public string UFE_SG { get; set; }
        public int LOC_NU { get; set; }
        public string CPC_NO { get; set; }
        public string CPC_ENDERECO { get; set; }
        public string CEP { get; set; }
    
        public virtual LOG_LOCALIDADE LOG_LOCALIDADE { get; set; }
        public virtual ICollection<LOG_FAIXA_CPC> LOG_FAIXA_CPC { get; set; }
    }
}
