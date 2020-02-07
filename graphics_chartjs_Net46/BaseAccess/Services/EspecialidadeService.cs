using BaseAccess.VModels;
using System.Collections.Generic;
using System.Linq;

namespace BaseAccess.Services
{
    public class EspecialidadeService
    {
        public static ICollection<EspecialidadeVM> Serialize(ICollection<Especialidade> Especialidade)
        {
            List<EspecialidadeVM> lista = new List<EspecialidadeVM>();

            int maxCount = Especialidade.Count();

            foreach (var item in Especialidade)
            {
                var Esp = new EspecialidadeVM();

                Esp.IdEspecialidade = item.esp_id;
                Esp.Descricao = item.esp_descricao;                
                
                lista.Add(Esp);
            }

            return lista;
        }
    }
}
