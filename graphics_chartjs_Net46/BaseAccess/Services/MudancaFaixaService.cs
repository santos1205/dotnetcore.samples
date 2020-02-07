using BaseAccess.Enums;
using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseAccess.Services
{
    public class MudancaFaixaService
    {
        public static ICollection<SeguradoReajusteVM> ListarSeguradosMFPorParams(
            DateTime DtInicial,
            DateTime DtFinal,
            int? crm,
            int? IdPlano,
            string cpf,
            string nome,
            Usuario Usuario = null
        )
        {
            var ctx = new SindicatoMedicoEntities();
            var Titulares = new List<Segurado>();

            if (Usuario != null)
                LogService.CapturaOpcaoLog(Usuario, AcaoEnum.ConsultarMudancaFaixa.ToString());


            IEnumerable<ReajusteFaixaEtaria> listaReajusteFaixaEtaria = ctx.ReajusteFaixaEtaria;

            if (DtInicial > DateTime.Parse("01-01-1900") && DtFinal > DateTime.Parse("01-01-1900"))            
                listaReajusteFaixaEtaria = listaReajusteFaixaEtaria.Where(x => x.rfe_data_reajuste >= DtInicial && x.rfe_data_reajuste <= DtFinal);

            if(IdPlano == 1)
                listaReajusteFaixaEtaria = listaReajusteFaixaEtaria.Where(x => x.Plano.pla_tpp_id == 1);

            if (IdPlano == 2)
                listaReajusteFaixaEtaria = listaReajusteFaixaEtaria.Where(x => x.Plano.pla_tpp_id == 2);

            // Filtra os segurados, pois a tabela no banco n tem constraint FK para os segurados titulares e dependentes.
            List<SeguradoReajusteVM> SeguradosVM = new List<SeguradoReajusteVM>();
            foreach (var reajuste in listaReajusteFaixaEtaria)
            {
                SeguradoReajusteVM SegVM = new SeguradoReajusteVM();                
                Dependente Dep = new Dependente();
                var Titular = ctx.Segurado.FirstOrDefault(x => x.seg_id == reajuste.rfe_id_seg_dep);
                //Verifica se o id é do Titular, senão consulta na tabela de dependentes
                if (Titular == null)                
                {
                    Dep = ctx.Dependente.FirstOrDefault(x => x.dep_id == reajuste.rfe_id_seg_dep);
                    SegVM = Serialize(Dep);
                }
                else
                    SegVM = Serialize(Titular);

                SegVM.Plano = ctx.Plano.FirstOrDefault(x => x.pla_id == reajuste.rfe_pla_id).pla_nome;
                SegVM.PremioAnterior = reajuste.rfe_valor_anterior.ToString();
                SegVM.PremioAtual = reajuste.rfe_valor_atualizado.ToString();

                // Filtro por crm
                if (crm != null && crm != 0)
                {
                    if (SegVM.Parentesco == "D")
                    {
                        // filtra pelo titular
                        var titularDep = ctx.Segurado.FirstOrDefault(x => x.seg_cpf == SegVM.CPFTitular);
                        if (titularDep.seg_crm != crm)
                            continue;
                    }else if (Titular.seg_crm != crm)                    
                        continue;
                }
                // Filtro por cpf
                if (!string.IsNullOrEmpty(cpf))
                {
                    if (SegVM.Parentesco == "D")
                    {
                        // filtra pelo titular
                        var titularDep = ctx.Segurado.FirstOrDefault(x => x.seg_cpf == SegVM.CPFTitular);
                        if (titularDep.seg_cpf != cpf)
                            continue;
                    }
                    else if (Titular.seg_cpf != cpf)
                        continue;
                }
                // Filtro por nome
                if (!string.IsNullOrEmpty(nome))
                {
                    if (SegVM.Parentesco == "D")
                    {
                        // filtra pelo titular
                        var titularDep = ctx.Segurado.FirstOrDefault(x => x.seg_cpf == SegVM.CPFTitular);
                        if (!titularDep.seg_nome.ToLower().StartsWith(nome.ToLower()))
                            continue;
                    }
                    else if (!Titular.seg_nome.ToLower().StartsWith(nome.ToLower()))
                        continue;
                }

                SeguradosVM.Add(SegVM);
            }
            return SeguradosVM;
        }

        public static ICollection<SeguradoReajusteVM> OrdernarTitularDependentesMF(ICollection<SeguradoReajusteVM> SeguradosVM)
        {
            List<SeguradoReajusteVM> SeguradosOrdenados = new List<SeguradoReajusteVM>();
            var ctx = new SindicatoMedicoEntities();
            foreach (var segurado in SeguradosVM)
            {
                //Se for dependente adiciona titular antes
                if (segurado.Parentesco == "D")
                {
                    var Titular = ctx.Segurado.FirstOrDefault(x => x.seg_cpf == segurado.CPFTitular);
                    SeguradosOrdenados.Add(Serialize(Titular));
                    // dps de adicionar o titular, add o segurado dependente
                    SeguradosOrdenados.Add(segurado);
                }else                
                    SeguradosOrdenados.Add(segurado);
                
            }
            return SeguradosOrdenados;
        }

        public static SeguradoReajusteVM Serialize(Segurado Titular)
        {
            var SeguradoVM = new SeguradoReajusteVM()
            {
                IdSegurado = Titular.seg_id,
                Nome = Titular.seg_nome,
                CPF = Titular.seg_cpf,
                Parentesco = "T",
                Aniversario = Titular.seg_data_nascimento.Day.ToString("00") + "/" + Titular.seg_data_nascimento.Month.ToString("00"),
                Idade = SeguradoService.CarregaIdade(Titular.seg_data_nascimento).ToString()
            };

            return SeguradoVM;
        }

        public static SeguradoReajusteVM Serialize(Dependente Dep)
        {
            var SeguradoVM = new SeguradoReajusteVM()
            {
                IdSegurado = Dep.dep_id,
                Nome = Dep.dep_nome,
                Parentesco = "D",
                CPF = Dep.dep_cpf,
                CPFTitular = Dep.dep_cpf_titular,
                Aniversario = Dep.dep_data_nascimento.Day.ToString("00") + "/" + Dep.dep_data_nascimento.Month.ToString("00"),
                Idade = SeguradoService.CarregaIdade(Dep.dep_data_nascimento).ToString()
            };

            return SeguradoVM;
        }

        private static int CalcularIdade(DateTime DtNascimento)
        {
            var ano = DtNascimento.Year;
            var anoVigente = DateTime.Now.Year;
            var idade = anoVigente - ano;

            return idade;
        }
    }
}
