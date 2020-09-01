using System;
using System.Collections.Generic;

namespace API.Viagem.Domain
{
    public partial class TblViagemPassageiros
    {
        public int PsgIdPassageiro { get; set; }
        public DateTime PsgDtNascimento { get; set; }
        public string PsgNome { get; set; }
        public string PsgSobrenome { get; set; }
        public string PsgCpf { get; set; }
        public string PsgEmail { get; set; }
        public string PsgTelefone { get; set; }
        public string PsgNumeroDocumento { get; set; }
        public string PsgTipoDocumento { get; set; }
        public string PsgEndereco { get; set; }
        public string PsgNumeroEndereco { get; set; }
        public string PsgCodigoPostal { get; set; }
        public string PsgCidade { get; set; }
        public string PsgBairro { get; set; }
        public string PsgEstado { get; set; }
        public string PsgPaisDomicilio { get; set; }
        public string PsgNomeContato { get; set; }
        public string PsgTelefoneContato { get; set; }
        public string PsgDadoAdicional1 { get; set; }
        public string PsgDadoAdicional2 { get; set; }
        public bool? PsgBlnAtivo { get; set; }
        public string PsgGenero { get; set; }
        public string PsgComplemento { get; set; }
        public int? PsgNacIdNacionalidade { get; set; }
        public IEnumerable<TblViagemVouchers> TblViagemVouchers { get; set; }
        public IEnumerable<TblViagemPassageirosCotacoes> TblViagemPassageirosCotacoes { get; set; }
        public IEnumerable<TblViagemCotacoesResultadosPassageiros> TblViagemCotacoesResultadosPassageiros { get; set; }
    }
}
