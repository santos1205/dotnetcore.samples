using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Viagem.Domain.Enums
{
    public enum FormaPagamentoEnum
    {
        [Description("Cartão de Crédito")]
        CartaoDeCredito = 1,
        [Description("Faturado")]
        Faturado = 2,
    }
}
