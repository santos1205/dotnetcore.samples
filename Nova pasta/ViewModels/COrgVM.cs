using QuestionarioCOrg.DataAccess;
using System;

namespace QuestionarioCOrg.ViewModels
{
    public class COrgVM
    {
        public int IdUsuario { get; set; }
        public string Item { get; set; }
        public decimal ValorAtual { get; set; }
        public decimal ValorDesejado { get; set; }


        public Media ToModel()
        {
            var model = new Media()
            {
                mda_usu_id = IdUsuario,
                mda_item = Item,
                mda_valor_atual = ValorAtual,
                mda_valor_desejado = ValorDesejado,
                mda_dt_cadastro = DateTime.Now
            };

            return model;
        }
    }
}