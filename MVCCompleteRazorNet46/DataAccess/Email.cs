
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
    
public partial class Email
{

    public int ema_id { get; set; }

    public string ema_remetente { get; set; }

    public string ema_destinatario { get; set; }

    public string ema_motivo_envio { get; set; }

    public System.DateTime ema_dt_cadastro { get; set; }

    public bool ema_ativo { get; set; }

    public int ema_eml_id { get; set; }

    public string ema_remetente_alias { get; set; }



    public virtual EmailConfiguracao EmailConfiguracao { get; set; }

}

}
