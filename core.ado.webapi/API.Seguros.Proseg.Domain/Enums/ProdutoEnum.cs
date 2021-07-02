using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace API.Seguros.Proseg.Domain.Enums
{
    public enum ProdutoEnum
    {
        [Description("Bradesco")]
        Bradesco = 5,
        [Description("Hdi")]
        Hdi = 8,
        [Description("Mapfre")]
        Mapfre = 17,
        [Description("Tokio")]
        Tokio = 19,
        [Description("Zurich")]
        Zurich = 27,
        [Description("Porto Seguro")]
        PortoSeguro = 30,
        [Description("Itaú")]
        Itau = 31,
        [Description("Azul")]
        Azul = 32,
        [Description("Liberty")]
        Liberty = 33,
        [Description("Sompo")]
        Sompo = 108

    }
}
