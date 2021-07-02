namespace API.Seguros.Proseg.Domain.DTOs
{
    public class RetornoSeguroAutoDTO
    {    
        public string Cli_Cpf_Cgc { get; set; }        
        public string Placa { get; set; }
        public string ModeloVeiculo { get; set; }
        public string NomeSegurado { get; set; }
        public string Celular { get; set; }        
        public string DataInicioVigencia { get; set; }
        public string DataFinalVigencia { get; set; }
        public string NumeroApolice { get; set; }
        public string NomeSeguradora { get; set; }
        public string ValorSeguro { get; set; }
        public string ContatoSeguradora { get; set; }
        public string ContatoCorretora { get; set; }     
        public string CEP { get; set; }
    }
}
