using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAccess
{
    public class TesteService
    {
        public static void Consultar()
        {
            var ctx = new SindicatoMedicoEntities();
            var segs = ctx.Segurado.ToList();
        }
    }
}
