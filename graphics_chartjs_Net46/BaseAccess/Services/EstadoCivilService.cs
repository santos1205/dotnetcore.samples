using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAccess.Services
{
    public class EstadoCivilService
    {
        public static ICollection<EstadoCivilVM> Serialize(ICollection<EstadoCivil> EstadoCivil)
        {
            List<EstadoCivilVM> lista = new List<EstadoCivilVM>();

            int maxCount = EstadoCivil.Count();

            foreach (var item in EstadoCivil)
            {
                var Ec = new EstadoCivilVM();

                Ec.Id = item.civ_id;
                Ec.Descricao = item.civ_descricao;

                lista.Add(Ec);
            }

            return lista;
        }


        public static ICollection<EstadoCivil> ListarEstadoCivil()
        {
            var ctx = new SindicatoMedicoEntities();
            var Ec = ctx.EstadoCivil;
            return Ec.ToList();
        }
    }
}
