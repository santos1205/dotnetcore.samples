using System;
using System.ComponentModel;
using System.Reflection;

namespace API.Seguros.Proseg.Domain.Enums
{
    public enum CombustivelEnum
    {
        [Description("Álcool")]
        AA,
        [Description("Flex")]
        AG,
        [Description("Diesel")]
        DD,
        [Description("Hibrido")]
        GE,
        [Description("Gasolina")]
        GG,
        [Description("Tetra Fuel")]
        GX,
    }
}