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
    
    public partial class LayoutTemplate
    {
        public int lay_id { get; set; }
        public int lay_tpl_id { get; set; }
        public short lay_posicao_campo { get; set; }
        public string lay_nome_campo { get; set; }
        public short lay_tamanho_campo { get; set; }
        public string lay_formato_campo { get; set; }
        public bool lay_obrigatoriedade { get; set; }
        public bool lay_ativo { get; set; }
        public System.DateTime lay_timestamp { get; set; }
    
        public virtual Template Template { get; set; }
    }
}
