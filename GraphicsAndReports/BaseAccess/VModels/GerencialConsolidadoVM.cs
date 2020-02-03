using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAccess.VModels
{
    public class GerencialConsolidadoVM
    {
        public int TitularQuantidadeSaude { get; set; }
        public int TitularQuantidadeOdonto { get; set; }
        public int DependenteQuantidadeSaude { get; set; }
        public int DependenteQuantidadeOdonto { get; set; }

        public GerencialConsolidadoVM()
        {
            TitularQuantidadeSaude = 0;
            TitularQuantidadeOdonto = 0;
            DependenteQuantidadeSaude = 0;
            DependenteQuantidadeOdonto = 0;
        }
    }
}
