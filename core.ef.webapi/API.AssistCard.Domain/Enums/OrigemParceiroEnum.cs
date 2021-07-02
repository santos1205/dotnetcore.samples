using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace API.Viagem.Domain.Enums
{
    public enum OrigemParceiroEnum
    {
        [Description("MultiCalculo")]
        MultiCalculo = 1,
        [Description("CorretoraPremium")]
        CorretoraPremium = 2,
    }
}
