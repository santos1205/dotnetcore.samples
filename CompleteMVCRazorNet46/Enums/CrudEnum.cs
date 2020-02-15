using System.ComponentModel;

namespace QuestionarioCOrg.Enums
{
    public enum CrudEnum    
    {
        [Description("Questionario")]
        Questionario = 1,
        [Description("Classificacao")]
        Classificacao = 2,
        [Description("Pergunta")]
        Perguntas = 3,
        [Description("Resposta")]
        Respostas = 4,
        [Description("Empresa")]
        Empresa = 5
    }
}