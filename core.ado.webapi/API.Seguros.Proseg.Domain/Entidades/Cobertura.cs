using API.Seguros.Proseg.Domain.Constants;
using API.Seguros.Proseg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vayon.MultiCalculo.Container;

namespace API.Seguros.Proseg.Domain.Entidades
{
    [Serializable]
    public class Cobertura : CoberturaAdicional
    {
        public int CodigoIncendioRoubo { get; set; }
        public string DescricaoIncendioRoubo { get; set; }

        public int CodigoDanosMateriais { get; set; }
        public string DescricaoDanosMateriais { get; set; }

        public int CodigoDanosCorporais { get; set; }
        public string DescricaoDanosCorporais { get; set; }

        public int CodigoDanosMorais { get; set; }
        public string DescricaoDanosMorais { get; set; }

        public int CodigoMortePassageiro { get; set; }
        public string DescricaoMortePassageiro { get; set; }

        public int CodigoInvalidezPassageiro { get; set; }
        public string DescricaoInvalidezPassageiro { get; set; }

        public int CodigoEmergencia24h { get; set; }
        public string DescricaoEmergencia24h { get; set; }

        public int CodigoCarroReserva { get; set; }
        public string DescricaoCarroReserva { get; set; }

        public int CodigoAssistenciaVidros { get; set; }
        public string DescricaoAssistenciaVidros { get; set; }

        public int CodigoKitGas { get; set; }
        public string DescricaoKitGas { get; set; }

        public int CodigoPerdaIntegral { get; set; }
        public string DescricaoPerdaIntegral { get; set; }

        public List<CoberturaAdicional> CoberturasEstipulante(int idEstipulante)
        {
            List<CoberturaAdicional> listaCoberturas = new List<CoberturaAdicional>();

            dynamic estipulante = new ExpandoObject();

            if (idEstipulante == (int)EstipulanteEnum.C6Bank)
            {
                estipulante = new FHEConstant();
            }

            var cobertura = new CoberturaAdicional()
            {
                Codigo = 1,
                Descricao = "COLISÃO INCÊNDIO E ROUBO",
                Contratada = true,
                Tipo = new TipoCobertura() { Codigo = estipulante.CodigoIncendioRoubo, Descricao = estipulante.DescricaoIncendioRoubo },
                ValorIS = 0,
            };
            listaCoberturas.Add(cobertura);

            cobertura = new CoberturaAdicional()
            {
                Codigo = 2,
                Descricao = "RCF – DANOS MATERIAIS",
                Contratada = true,
                Tipo = new TipoCobertura() { Codigo = estipulante.CodigoDanosMateriais, Descricao = estipulante.DescricaoDanosMateriais },
                ValorIS = Convert.ToInt32(estipulante.CodigoDanosMateriais)
            };
            listaCoberturas.Add(cobertura);

            cobertura = new CoberturaAdicional()
            {
                Codigo = 3,
                Descricao = "RCF – DANOS CORPORAIS",
                Contratada = true,
                Tipo = new TipoCobertura() { Codigo = estipulante.CodigoDanosCorporais, Descricao = estipulante.DescricaoDanosCorporais },
                ValorIS = Convert.ToInt32(estipulante.CodigoDanosCorporais)
            };
            listaCoberturas.Add(cobertura);

            cobertura = new CoberturaAdicional()
            {
                Codigo = 4,
                Descricao = "DANOS MORAIS",
                Contratada = true,
                Tipo = new TipoCobertura() { Codigo = estipulante.CodigoDanosMorais, Descricao = estipulante.DescricaoDanosMorais },
                ValorIS = Convert.ToInt32(estipulante.CodigoDanosMorais)
            };
            listaCoberturas.Add(cobertura);

            cobertura = new CoberturaAdicional()
            {
                Codigo = 5,
                Descricao = "APP - MORTE POR PASSAGEIRO",
                Contratada = true,
                Tipo = new TipoCobertura() { Codigo = estipulante.CodigoMortePassageiro, Descricao = estipulante.DescricaoMortePassageiro },
                ValorIS = Convert.ToInt32(estipulante.CodigoMortePassageiro)
            };
            listaCoberturas.Add(cobertura);

            cobertura = new CoberturaAdicional()
            {
                Codigo = 6,
                Descricao = "APP - INVALIDEZ POR PASSAGEIRO",
                Contratada = true,
                Tipo = new TipoCobertura() { Codigo = estipulante.CodigoInvalidezPassageiro, Descricao = estipulante.DescricaoInvalidezPassageiro },
                ValorIS = Convert.ToInt32(estipulante.CodigoInvalidezPassageiro)
            };
            listaCoberturas.Add(cobertura);

            cobertura = new CoberturaAdicional()
            {
                Codigo = 7,
                Descricao = "ASSISTÊNCIA 24 HORAS",
                Contratada = true,
                Tipo = new TipoCobertura() { Codigo = estipulante.CodigoEmergencia24h, Descricao = estipulante.DescricaoEmergencia24h },
                ValorIS = Convert.ToInt32(estipulante.CodigoEmergencia24h)
            };

            listaCoberturas.Add(cobertura);
            cobertura = new CoberturaAdicional()
            {
                Codigo = 8,
                Descricao = "CARRO RESERVA",
                Contratada = true,
                Tipo = new TipoCobertura() { Codigo = estipulante.CodigoCarroReserva, Descricao = estipulante.DescricaoCarroReserva },
                ValorIS = Convert.ToInt32(estipulante.CodigoCarroReserva)
            };

            listaCoberturas.Add(cobertura);
            cobertura = new CoberturaAdicional()
            {
                Codigo = 9,
                Descricao = "VIDROS",
                Contratada = true,
                Tipo = new TipoCobertura() { Codigo = estipulante.CodigoAssistenciaVidros, Descricao = estipulante.DescricaoAssistenciaVidros },
                ValorIS = Convert.ToInt32(estipulante.CodigoAssistenciaVidros)
            };

            //listaCoberturas.Add(cobertura);
            //cobertura = new CoberturaAdicional()
            //{
            //    Codigo = 61,
            //    Descricao = "KIT GÁS",
            //    Contratada = true,
            //    Tipo = new TipoCobertura() { Codigo = estipulante.CodigoKitGas, Descricao = estipulante.DescricaoKitGas },
            //    ValorIS = 0
            //};

            listaCoberturas.Add(cobertura);
            cobertura = new CoberturaAdicional()
            {
                Codigo = 36,
                Descricao = "PERDA INTEGRAL",
                Contratada = true,
                Tipo = new TipoCobertura() { Codigo = estipulante.CodigoPerdaIntegral, Descricao = estipulante.DescricaoPerdaIntegral },
                ValorIS = 0
            };

            listaCoberturas.Add(cobertura);

            return listaCoberturas;
        }
    }
}
