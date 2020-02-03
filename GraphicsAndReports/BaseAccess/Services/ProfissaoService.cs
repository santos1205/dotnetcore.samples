using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAccess.Services
{
    public class ProfissaoService
    {
        public static ICollection<ProfissaoVM> Serialize(IEnumerable<Profissao> Profissoes)
        {
            List<ProfissaoVM> lista = new List<ProfissaoVM>();

            int maxCount = Profissoes.Count();

            foreach (var item in Profissoes)
            {
                var Prf = new ProfissaoVM();

                Prf.Id = item.prf_id;
                Prf.Nome = item.prf_descricao;

                lista.Add(Prf);
            }

            return lista;
        }
    }
}
