using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAccess.Services
{
    public class BancoService
    {
        public static ICollection<BancoVM> Serialize(ICollection<Banco> Bancos)
        {
            List<BancoVM> lista = new List<BancoVM>();


            foreach (var item in Bancos)
            {
                var Banco = new BancoVM();

                Banco.CodBanco = item.ban_id;
                Banco.Nome = item.ban_descricao;
                
                lista.Add(Banco);
            }

            return lista;
        }
    }
}
