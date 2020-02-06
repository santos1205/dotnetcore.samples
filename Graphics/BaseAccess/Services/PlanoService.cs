using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace BaseAccess
{
    public class PlanoService
    {
        public static ICollection<PlanoVM> Serialize(ICollection<Plano> Planos)
        {
            List<PlanoVM> lista = new List<PlanoVM>();


            int maxCount = Planos.Count();

            foreach (var item in Planos)
            {
                var iPlano = new PlanoVM();

                iPlano.IdPlano = item.pla_id;
                iPlano.Descricao = item.pla_descricao;
                iPlano.TipoPlano = item.pla_tpp_id;

                lista.Add(iPlano);
            }

            return lista;
        }

        public static void CadastrarPlanoSegurado(PlanoSegurado PlanoSegurado)
        {
            var ctx = new SindicatoMedicoEntities();

            try
            {
                PlanoSegurado.pls_timestamp = DateTime.Now;
                ctx.PlanoSegurado.Add(PlanoSegurado);
                ctx.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string msgError = "Erro durante o cadastro - ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;
            }
        }

        public static List<PlanoSeguradoVM> ConsultarPlanoSeguradoPorId(int idSegurado)
        {
            var ctx = new SindicatoMedicoEntities();
            var listaPlanos = new List<PlanoSeguradoVM>();

            var planos = ctx.PlanoSegurado.Where(x => x.pls_seg_id == idSegurado).ToList();

            foreach (var item in planos)
            {
                var planosSegurado = new PlanoSeguradoVM();

                planosSegurado.IdPlanoSegurado = item.pls_id;
                planosSegurado.IdSegurado = item.pls_seg_id;
                planosSegurado.IdPlano = item.pls_pla_id;

                listaPlanos.Add(planosSegurado);
            }
            return listaPlanos;
        }

        public static ICollection<PlanoVM> SerializarPlanosSegurado(List<PlanoSeguradoVM> listaPlanos)
        {
            var ctx = new SindicatoMedicoEntities();
            var listaPlanosSegurado = new List<PlanoVM>();

            for (int i = 0; i < listaPlanos.Count; i++)
            {
                var indiceLista = listaPlanos[i];
                var consultaPLS = ctx.Plano.Where(x => x.pla_id == indiceLista.IdPlano);

                foreach (var plano in consultaPLS)
                {
                    var listaPlanoVM = new PlanoVM();

                    listaPlanoVM.IdPlano = plano.pla_id;
                    listaPlanoVM.Descricao = plano.pla_descricao;
                    listaPlanoVM.TipoPlano = plano.pla_tpp_id;

                    listaPlanosSegurado.Add(listaPlanoVM);
                }
            }

            return listaPlanosSegurado;
        }

    }
}
