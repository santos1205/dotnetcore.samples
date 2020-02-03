using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAccess.Services
{
    public class MelhorDiaPagamentoServices
    {
        public static ICollection<MelhorDiaPagVM> Serialize(IEnumerable<MelhorDiaPagamento> MelhorDiaPagamento)
        {
            List<MelhorDiaPagVM> lista = new List<MelhorDiaPagVM>();

            int maxCount = MelhorDiaPagamento.Count();

            foreach (var item in MelhorDiaPagamento)
            {
                var MDPag = new MelhorDiaPagVM();

                MDPag.Id = item.mdp_id;
                MDPag.MelhorDia = item.mdp_dia.ToString();

                lista.Add(MDPag);
            }

            return lista;
        }
    }
}
