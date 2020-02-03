using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAccess.VModels
{
    public class LogVM
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Departamento { get; set; }
        public string ActLog { get; set; }
        public string NomeSegurado { get; set; }
        public string Data { get; set; }
    }
}
