using BaseAccess.VModels;
using System.Collections.Generic;
using System.Linq;

namespace BaseAccess
{
    public class AcaoService
    {
        public static AcaoVM Serialize(Acao Acao)
        {
            var ctx = new SindicatoMedicoEntities();
            var VM = new AcaoVM();

            VM.Id = Acao.aco_id;
            VM.Nome = Acao.aco_nome;

            return VM;
        }

        public static ICollection<AcaoVM> Serialize(ICollection<Acao> Acoes)
        {
            List<AcaoVM> lista = new List<AcaoVM>();

            int maxCount = Acoes.Count();

            foreach (var item in Acoes)
            {
                var Acao = new AcaoVM();

                Acao.Id = item.aco_id;
                Acao.Nome = item.aco_nome;
                
                lista.Add(Acao);
            }

            return lista;
        }
    }
}
