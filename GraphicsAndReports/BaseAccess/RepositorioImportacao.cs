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
    
    public partial class RepositorioImportacao
    {
        public int rep_id { get; set; }
        public int rep_tpl_id { get; set; }
        public string rep_arquivo { get; set; }
    
        public virtual Template Template { get; set; }
    }
}
