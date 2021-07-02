using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Seguros.Proseg.Domain.Enums
{
    public enum AutoFranquiaEnum
    {

        [Description("BÁSICA")]
        BASICA = 1,
        [Description("FACULTATIVA")]
        FACULTATIVA = 2,
        [Description("REDUZIDA")]
        REDUZIDA = 3,
    }
}
