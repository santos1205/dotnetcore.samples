using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Seguros.Proseg.Domain.Constants
{
    public class EndpointsConstant
    { 
        public const string AUTO = "http://api.seguros.proseg.com.br/seguros/segurosativos/auto/{cpf}";
        public const string VIAGEM = "http://api.seguros.proseg.com.br/seguros/segurosativos/viagem/{cpf}";
        public const string BIKE = "http://api.seguros.proseg.com.br/seguros/segurosativos/bike/{cpf}";
        public const string PET = "http://api.seguros.proseg.com.br/seguros/segurosativos/pet/{cpf}";
        public const string RESIDENCIAL = "http://api.seguros.proseg.com.br/seguros/segurosativos/residencial/{cpf}";
    }
}
