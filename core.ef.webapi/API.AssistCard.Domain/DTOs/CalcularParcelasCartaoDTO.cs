using System;
using System.Collections.Generic;
using System.Text;

namespace API.Viagem.Domain.DTOs
{
    public class CalcularParcelasCartaoDTO
    {
        public string BandeiraCartao { get; set; }
        public string Parcelas { get; set; }
        public string CodigoProduto { get; set; }
        public string CodigoRate { get; set; }
        public string TotalDias { get; set; }
        public string ValorAPagar { get; set; }
    }
}
