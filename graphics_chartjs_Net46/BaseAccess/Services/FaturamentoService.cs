using BaseAccess.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace BaseAccess.Services
{
    public class FaturamentoService
    {

        public static ICollection<Pagamento> ListarPorParams(
            DateTime DtInicial,
            DateTime DtFinal,
            string nrCarteira,
            int? IdPlano,
            int? crm,
            string cpf,
            string nome,
            Usuario Usuario = null
        )
        {
            var ctx = new SindicatoMedicoEntities();
            var Segurados = new List<Segurado>();
            var pags = new List<Pagamento>();
            var Paga = new List<Pagamento>();

            if (Usuario != null)
                LogService.CapturaOpcaoLog(Usuario, AcaoEnum.ConsultarFaturamento.ToString());
            else
                return new List<Pagamento>();

            try
            {
                if (IdPlano == 1) // apenas odonto
                    Paga = ctx.Pagamento.Where(x => x.Plano.pla_tpp_id == 1 && x.Segurado.seg_ativo == true).ToList();

                if (IdPlano == 2 || IdPlano == null) // se for odonto/saude ou se não estiver preenchidp
                    Paga = ctx.Pagamento.Where(x => x.Segurado.seg_ativo == true).ToList();

                if (IdPlano == 3) // Apenas odonto
                    Paga = ctx.Pagamento.Where(x => x.Plano.pla_tpp_id == 2 && x.Segurado.seg_ativo == true).ToList();

                foreach (var pag in Paga)
                {
                    if (DtInicial != null && DtFinal != null)
                        if (DtInicial != DateTime.MinValue && DtFinal != DateTime.MinValue)
                            // Se a data de vencimento n estiver no período, passa para o proximo registro.
                            if (!(pag.pag_data_vencimento >= DtInicial && pag.pag_data_vencimento <= DtFinal))
                                continue;

                    if (DtInicial != null && DtFinal == null)
                        // Se a data de vencimento n estiver no período, passa para o proximo registro.
                        if (!(pag.pag_data_vencimento >= DtInicial))
                            continue;

                    if (DtInicial == null && DtFinal != null)
                        // Se a data de vencimento n estiver no período, passa para o proximo registro.
                        if (!(pag.pag_data_vencimento <= DtFinal))
                            continue;

                    pags.Add(pag);
                }

            }
            catch (DbEntityValidationException e)
            {
                string msgError = "Erro durante a consulta faturamento - ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;
                throw new Exception(msgError);
            }
            return pags;
        }
    }
}
