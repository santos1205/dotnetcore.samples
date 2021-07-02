using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Seguros.Proseg.Domain.Enums
{
    public enum EstadoCivilEnum
    {

        [Description("Solteiro(a)")]
        S = 1,
        [Description("Casado(a)")]
        C = 2,
        [Description("Viúvo(a)")]
        V = 3,
        [Description("Divorciado(a)")]
        D = 4,
        [Description("Marital")]
        M = 5,
        [Description("Separado")]
        P = 6,
    }
}
