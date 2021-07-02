using System.ComponentModel;

namespace API.Seguros.Proseg.Domain.Enums
{
    public enum FaixaSalarialEnum
    {
        [Description("Até R$1.000,00")]
        Ate1000 = 1,
        [Description("De R$1.000,01 até R$3.000,00")]
        De1000Ate3000 = 2,
        [Description("De R$3.000,01 até R$5.000,00")]
        De3000Ate5000 = 3,
        [Description("De R$5.000,01 até R$10.000,00")]
        De5000Ate10000 = 4,
        [Description("Acima de R$ 20.000,00")]
        Acima20000 = 5,
        [Description("Do lar, sem renda mensal a informar.")]
        DoLarSemRendaMensalInformar = 6,
    }
}
