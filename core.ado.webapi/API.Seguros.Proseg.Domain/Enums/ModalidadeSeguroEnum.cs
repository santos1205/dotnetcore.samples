using System.ComponentModel;

namespace API.Seguros.Proseg.Domain.Enums
{
    public enum ModalidadeSeguroEnum
    {
        [Description("Residencial")]
        Residencial = 10114,
        [Description("Viagem")]
        Viagem = 11369,
        [Description("Bike")]
        Bike = 10171,
        [Description("Pet")]
        Pet = 11253,
    }
}
