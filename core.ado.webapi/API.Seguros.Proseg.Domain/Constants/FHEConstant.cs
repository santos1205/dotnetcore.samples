using API.Seguros.Proseg.Domain.Entidades;
using Vayon.MultiCalculo.Container.Auto;

namespace API.Seguros.Proseg.Domain.Constants
{
    public class FHEConstant : Cobertura
    {
        public class SegurosDadosBasicoConstant
        {
            public const int usrClienteID = 7;
            public const string Nome = "FHE";
        }


        public FHEConstant()
        {
            CodigoIncendioRoubo = _CodigoIncendioRoubo;
            DescricaoIncendioRoubo = _DescricaoIncendioRoubo;

            CodigoDanosCorporais = _CodigoDanosCorporais;
            DescricaoDanosCorporais = _DescricaoDanosCorporais;

            CodigoDanosMateriais = _CodigoDanosMateriais;
            DescricaoDanosMateriais = _DescricaoDanosMorais;

            CodigoDanosMorais = _CodigoDanosMorais;
            DescricaoDanosMorais = _DescricaoDanosMorais;

            CodigoMortePassageiro = _CodigoMortePassageiro;
            DescricaoMortePassageiro = _DescricaoMortePassageiro;

            CodigoInvalidezPassageiro = _CodigoInvalidezPassageiro;
            DescricaoInvalidezPassageiro = _DescricaoInvalidezPassageiro;

            CodigoEmergencia24h = _CodigoEmergencia24h;
            DescricaoEmergencia24h = _DescricaoEmergencia24h;

            CodigoCarroReserva = _CodigoCarroReserva;
            DescricaoCarroReserva = _DescricaoCarroReserva;

            CodigoAssistenciaVidros = _CodigoAssistenciaVidros;
            DescricaoAssistenciaVidros = _DescricaoAssistenciaVidros;

            CodigoKitGas = _CodigoKitGas;
            DescricaoKitGas = _DescricaoKitGas;

            CodigoPerdaIntegral = _CodigoPerdaIntegral;
            DescricaoPerdaIntegral = _DescricaoPerdaIntegral;
        }


        public const int _CodigoIncendioRoubo = 1;
        public const string _DescricaoIncendioRoubo = "Colisão Incêndio e Roubo";

        public const int _CodigoDanosMateriais = 100000;
        public const string _DescricaoDanosMateriais = "100.000";

        public const int _CodigoDanosCorporais = 100000;
        public const string _DescricaoDanosCorporais = "100.000";

        public const int _CodigoDanosMorais = 10000;
        public const string _DescricaoDanosMorais = "10.000";

        public const int _CodigoMortePassageiro = 10000;
        public const string _DescricaoMortePassageiro = "10.000";

        public const int _CodigoInvalidezPassageiro = 10000;
        public const string _DescricaoInvalidezPassageiro = "10.000";

        public const int _CodigoEmergencia24h = 200;
        public const string _DescricaoEmergencia24h = "200";

        public const int _CodigoCarroReserva = 7;
        public const string _DescricaoCarroReserva = "07 dias";

        public const int _CodigoAssistenciaVidros = 1;
        public const string _DescricaoAssistenciaVidros = "Completo";

        public const int _CodigoKitGas = 2;
        public const string _DescricaoKitGas = "Kit Gas";

        public const int _CodigoPerdaIntegral = 1;
        public const string _DescricaoPerdaIntegral = "Perda Intergral";

    }

    public class FHEAntiFurtoConstant
    {
        public AntiFurto AntiFurto1 { get; set; }
        public AntiFurto AntiFurto2 { get; set; }

        public FHEAntiFurtoConstant()
        {
            AntiFurto1 = new AntiFurto()
            {
                CodigoTipoAntiFurto = CodigoTipoAntiFurto1,
                Codigo = Codigo1,
                Descricao = Descricao1
            };

            AntiFurto2 = new AntiFurto()
            {
                CodigoTipoAntiFurto = CodigoTipoAntiFurto2,
                Codigo = Codigo2,
                Descricao = Descricao2
            };
        }


        public const int CodigoTipoAntiFurto1 = 1;
        public const int Codigo1 = 1;
        public const string Descricao1 = "ALARME SONORO";

        public const int CodigoTipoAntiFurto2 = 0;
        public const int Codigo2 = 0;
        public const string Descricao2 = "Não Possui";

    }
}

