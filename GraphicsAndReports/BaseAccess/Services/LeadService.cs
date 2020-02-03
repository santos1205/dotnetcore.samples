using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaseAccess
{
    public class LeadService
    {

        public static ICollection<Leads> ListarPorParams(DateTime PInicio, DateTime PFim)
        {
            var ctx = new SindicatoMedicoEntities();

            IEnumerable<Leads> lista = ctx.Leads;

            lista = lista.Where(x => x.led_timestamp >= PInicio.AddDays(-1) && x.led_timestamp <= PFim.AddDays(1));

            return lista.ToList();
        }

        public static ICollection<LeadVM> Serialize(ICollection<Leads> Leads)
        {
            List<LeadVM> lista = new List<LeadVM>();

            int maxCount = Leads.Count();

            foreach (var item in Leads)
            {
                var Lead = new LeadVM();

                Lead.IdLead = item.led_id;
                Lead.Nome = item.led_nome;
                Lead.CRM = item.led_crm;
                Lead.DtCadastro = item.led_timestamp.ToShortDateString();
                if (!string.IsNullOrEmpty(item.led_celular))
                    Lead.Celular = Regex.Replace(item.led_celular, @"[^0-9a-zA-Z]+", "");
                else
                    Lead.Celular = "";
                Lead.Email = item.led_email;

                lista.Add(Lead);
            }

            return lista;
        }
    }
}
