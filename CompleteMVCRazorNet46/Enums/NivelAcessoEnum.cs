using System.ComponentModel;

namespace QuestionarioCOrg.Enums
{
    public enum NivelAcessoEnum
    {        
        [Description("respondente")]
        Respondente = 7,
        [Description("gestor")]
        Gestor = 1,
        [Description("administrativo")]
        Administrativo = 6        
    }
}