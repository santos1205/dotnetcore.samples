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
    
    public partial class Log
    {
        public int log_id { get; set; }
        public int log_aco_id { get; set; }
        public System.DateTime log_data { get; set; }
        public int log_usr_id { get; set; }
        public string log_cpf { get; set; }
        public string log_par_id { get; set; }
        public bool log_ativo { get; set; }
    
        public virtual Acao Acao { get; set; }
        public virtual Parentesco Parentesco { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}