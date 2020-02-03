using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAccess.Services
{
    public class DependenteService
    {
        public static IEnumerable<DependenteVM> Serialize(IEnumerable<Dependente> Dependentes)
        {
            List<DependenteVM> lista = new List<DependenteVM>();

            int maxCount = Dependentes.Count();

            foreach (var item in Dependentes)
            {
                var DepVM = new DependenteVM();

                DepVM.IdDependente = item.dep_id;
                DepVM.Nome = item.dep_nome;
                DepVM.Cpf = item.dep_cpf;                
                
                lista.Add(DepVM);
            }

            return lista;
        }
    }
}
