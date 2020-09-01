using System;
using System.Collections.Generic;

namespace API.Viagem.Domain
{
    public partial class TblViagemEmissoesRetorno
    {
        public int EmrIdEmissaoRetorno { get; set; }
        public int EmrEmiIdEmissao { get; set; }
        public int EmrGrupoVoucher { get; set; }
        public byte EmrTotalDias { get; set; }
        public DateTime EmrDtEmissaoRetorno { get; set; }
        public bool EmrBlnSucesso { get; set; }

        public virtual TblViagemEmissoes EmrEmiIdEmissaoNavigation { get; set; }
        public virtual ICollection<TblViagemVouchers> TblViagemVouchers { get; set; }
    }
}
