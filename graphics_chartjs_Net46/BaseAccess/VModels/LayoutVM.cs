using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAccess.VModels
{
    public class LayoutVM
    {
        public int IdLayout { get; set; }
        public int IdTemplate { get; set; }
        public string NomeTemplate { get; set; }
        public short PosicaoCampo { get; set; }
        public string NomeCampo { get; set; }
        public short TamanhoCampo { get; set; }
        public string FormatoCampo { get; set; }
        public string ObrigatoriedadeCampo { get; set; }
        public bool Ativo { get; set; }
    }
}
