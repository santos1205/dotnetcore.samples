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
    
    public partial class LOG_FAIXA_BAIRRO
    {
        public int BAI_NU { get; set; }
        public string FCB_CEP_INI { get; set; }
        public string FCB_CEP_FIM { get; set; }
    
        public virtual LOG_BAIRRO LOG_BAIRRO { get; set; }
    }
}
