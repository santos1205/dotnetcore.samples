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
    
    public partial class LOG_BAIRRO
    {
        public LOG_BAIRRO()
        {
            this.LOG_FAIXA_BAIRRO = new HashSet<LOG_FAIXA_BAIRRO>();
            this.LOG_VAR_BAI = new HashSet<LOG_VAR_BAI>();
        }
    
        public int BAI_NU { get; set; }
        public string UFE_SG { get; set; }
        public int LOC_NU { get; set; }
        public string BAI_NO { get; set; }
        public string BAI_NO_ABREV { get; set; }
    
        public virtual LOG_LOCALIDADE LOG_LOCALIDADE { get; set; }
        public virtual ICollection<LOG_FAIXA_BAIRRO> LOG_FAIXA_BAIRRO { get; set; }
        public virtual ICollection<LOG_VAR_BAI> LOG_VAR_BAI { get; set; }
    }
}
