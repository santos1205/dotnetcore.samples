using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAccess.VModels
{
    public class HistoricoEventoVM
    {
        public int Id { get; set; }
        public string DataEvento { get; set; }
        public string Usuario { get; set; }
        public string NomeArquivo { get; set; }
        public string NomeTemplate { get; set; }
        public string Status { get; set; }
        public string MesRef { get; set; }
    }
}
