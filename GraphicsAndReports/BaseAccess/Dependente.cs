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
    
    public partial class Dependente
    {
        public int dep_id { get; set; }
        public string dep_cpf_titular { get; set; }
        public string dep_nome { get; set; }
        public string dep_cpf { get; set; }
        public string dep_par_id { get; set; }
        public int dep_tpa_id { get; set; }
        public int dep_pla_id { get; set; }
        public System.DateTime dep_data_nascimento { get; set; }
        public decimal dep_valor_faixa { get; set; }
        public string dep_email { get; set; }
        public Nullable<System.DateTime> dep_dt_cadastro { get; set; }
        public Nullable<System.DateTime> dep_dt_atualizacao { get; set; }
        public Nullable<int> dep_prf_id { get; set; }
        public Nullable<int> dep_civ_id { get; set; }
        public Nullable<System.DateTime> dep_dt_filiacao { get; set; }
        public string dep_sexo { get; set; }
        public string dep_nacionalidade { get; set; }
        public string dep_pispasep { get; set; }
        public string dep_cns { get; set; }
        public string dep_dn { get; set; }
        public Nullable<System.DateTime> dep_inicio_vigencia { get; set; }
        public Nullable<System.DateTime> dep_fim_vigencia { get; set; }
        public string dep_nome_mae { get; set; }
        public bool dep_ativo { get; set; }
        public string dep_numero_carteira { get; set; }
        public Nullable<decimal> dep_valor_odonto { get; set; }
    
        public virtual EstadoCivil EstadoCivil { get; set; }
        public virtual Profissao Profissao { get; set; }
        public virtual Tipo_parentesco Tipo_parentesco { get; set; }
    }
}
