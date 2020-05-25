//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuestionarioCOrg.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.Media = new HashSet<Media>();
            this.RespostaUsuario = new HashSet<RespostaUsuario>();
            this.Notificacoes = new HashSet<Notificacoes>();
        }
    
        public int usu_id { get; set; }
        public string usu_nome { get; set; }
        public string usu_cargo { get; set; }
        public System.DateTime usu_dt_cadastro { get; set; }
        public string usu_cpf { get; set; }
        public string usu_email { get; set; }
        public string usu_telefone { get; set; }
        public System.DateTime usu_dt_nascimento { get; set; }
        public bool usu_ativo { get; set; }
        public int usu_nvl_id { get; set; }
        public int usu_emp_id { get; set; }
        public int usu_dpt_id { get; set; }
        public string usu_senha { get; set; }
        public Nullable<System.DateTime> usu_aprovado { get; set; }
    
        public virtual Departamento Departamento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Media> Media { get; set; }
        public virtual NivelAcesso NivelAcesso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RespostaUsuario> RespostaUsuario { get; set; }
        public virtual Empresa Empresa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notificacoes> Notificacoes { get; set; }
    }
}
