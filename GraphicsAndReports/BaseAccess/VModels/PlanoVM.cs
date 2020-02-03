using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAccess.VModels
{
    public class PlanoVM
    {
        public int IdPlano { get; set; }
        public string Descricao { get; set; }
        public int? TipoPlano { get; set; }
    }
}
