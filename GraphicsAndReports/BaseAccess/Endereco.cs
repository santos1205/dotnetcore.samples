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
    
    public partial class Endereco
    {
        public int end_id { get; set; }
        public string end_par_id { get; set; }
        public string end_cpf { get; set; }
        public string end_endereco { get; set; }
        public string end_bairro { get; set; }
        public string end_cidade { get; set; }
        public string end_complemento { get; set; }
        public string end_estado { get; set; }
        public string end_cep { get; set; }
        public bool end_ativo { get; set; }
        public Nullable<int> end_seg_id { get; set; }
        public Nullable<int> end_dep_id { get; set; }
    
        public virtual Parentesco Parentesco { get; set; }
    }
}
