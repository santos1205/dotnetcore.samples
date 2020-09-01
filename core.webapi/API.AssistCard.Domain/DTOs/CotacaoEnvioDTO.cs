using System;
using System.Collections.Generic;

namespace API.Viagem.Domain.DTOs
{
    public class CotacaoEnvioDTO
    {
        public CotacaoEnvioDTO()
        {
            CotacaoEnvioPassageiroDTO = new List<CotacaoEnvioPassageiroDTO>();
        }

        public int IdCorretor { get; set; }
        public int IdEstipulante { get; set; }
        public int IdPdv { get; set; }
        public DateTime DtCotacao { get; set; }
        public int Destino { get; set; }
        public DateTime DtPartida { get; set; }
        public DateTime DtRetorno { get; set; }
        public bool Faturado { get; set; }
        public int Status { get; set; }
        public int FormaPagamento { get; set; }
        public string BandeiraCartao { get; set; }
        public int QtdParcelas { get; set; }
        public string NomeUsuarioEmissao { get; set; }

        public List<CotacaoEnvioPassageiroDTO> CotacaoEnvioPassageiroDTO { get; set; }

    }
}
