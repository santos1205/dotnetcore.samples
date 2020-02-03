using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAccess.Services
{
    public class SeguradoraService
    {
        public static ICollection<SeguradoraVM> Serialize(ICollection<Seguradora> Seguradora)
        {
            List<SeguradoraVM> lista = new List<SeguradoraVM>();
                        
            foreach (var item in Seguradora)
            {
                var Seg = new SeguradoraVM();

                Seg.IdSeguradora = item.cia_id;
                Seg.Nome = item.cia_descricao;

                lista.Add(Seg);
            }

            return lista;
        }
    }
}
