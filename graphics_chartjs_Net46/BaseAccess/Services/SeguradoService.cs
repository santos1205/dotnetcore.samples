using BaseAccess.Enums;
using BaseAccess.VModels;
using slnSindicatoMedico.VModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace BaseAccess
{
    public class SeguradoService
    {
        public static object HttpContext { get; private set; }

        public static ICollection<Segurado> ListarPorParams(int crm, string nome, string cpf, string ativo, string email, string telefone)
        {
            var ctx = new SindicatoMedicoEntities();

            IEnumerable<Segurado> lista = ctx.Segurado;
            bool blAtivo = true;


            if (!string.IsNullOrEmpty(telefone))
            {
                //Verifica se é celular
                if (telefone.Length == 11)
                {
                    string auxDDD = telefone.Substring(0, 2);
                    string celSemDDD = telefone.Substring(2, 9);
                    // necessário carregar o contato, para pegar o cpf do segurado
                    var aux_cnt = ctx.Contato.Where(x => x.cnt_celular.Equals(celSemDDD) && x.cnt_ddd.Equals(auxDDD)).FirstOrDefault();

                    if (aux_cnt != null)
                    {
                        lista = lista.Where(x => x.seg_cpf.Equals(aux_cnt.cnt_cpf));
                        if (lista.Count() > 0)
                            return lista.ToList();
                    }
                    else
                        return new List<Segurado>();
                }

                // Verifica se é telefone fixo
                if (telefone.Length == 10)
                {
                    string auxDDD = telefone.Substring(0, 2);
                    string foneSemDDD = telefone.Substring(2, 8);
                    // necessário carregar o contato, para pegar o cpf do segurado
                    var aux_cnt = ctx.Contato.Where(x => x.cnt_fone.Equals(foneSemDDD) && x.cnt_ddd.Equals(auxDDD)).FirstOrDefault();

                    if (aux_cnt != null)
                    {
                        lista = lista.Where(x => x.seg_cpf == aux_cnt.cnt_cpf);
                        if (lista.Count() > 0)
                            return lista.ToList();
                    }
                    else
                        return new List<Segurado>();
                }
            }

            if (!string.IsNullOrEmpty(ativo))
            {
                blAtivo = ativo == "1" ? true : false;
                lista = ctx.Segurado.Where(x => x.seg_ativo == blAtivo);
            }


            if (crm != 0)
                lista = lista.Where(x => x.seg_crm == crm);
            if (!string.IsNullOrEmpty(nome))
                lista = lista.Where(x => x.seg_nome.ToLower().StartsWith(nome.ToLower()));
            if (!string.IsNullOrEmpty(cpf))
                lista = lista.Where(x => x.seg_cpf.Equals(cpf));
            if (!string.IsNullOrEmpty(email))
                lista = lista.Where(x => x.seg_email == email);


            return lista.ToList();
        }

        public static bool CadastrarTitular(Usuario Usuario, Segurado Segurado)
        {
            var ctx = new SindicatoMedicoEntities();
            
            try
            {
                if (Usuario == null)
                    return false;

                // Verifica se titular já está cadastrado. Em caso positivo, atualiza os dados.
                var c_titular = ctx.Segurado.FirstOrDefault(x => x.seg_cpf.Equals(Segurado.seg_cpf));
                if (c_titular != null)
                {
                    Segurado.seg_id = c_titular.seg_id;
                    SalvarDadosTitular(Usuario, Segurado);
                }
                else
                {
                    Segurado.seg_tpa_id = ctx.Tipo_parentesco.FirstOrDefault(x => x.tpa_par_id == "T").tpa_id;
                    Segurado.seg_dt_cadastro = DateTime.Now;
                    Segurado.seg_par_id = "T";
                    ctx.Segurado.Add(Segurado);
                    ctx.SaveChanges();

                    LogService.CapturaOpcaoLog(Usuario, AcaoEnum.CadastrarSegurado.ToString(), Segurado.seg_id);
                }

                return true;
            }
            catch (DbEntityValidationException e)
            {
                string msgError = "Erro durante o cadastro - ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;
                throw new Exception("erro cadastro titular: " + msgError);
            }

            return false;
        }

        public static void RemoverDependentePorId(int Id)
        {
            var ctx = new SindicatoMedicoEntities();

            var Dep = ctx.Dependente.Find(Id);
            ctx.Dependente.Remove(Dep);
            ctx.SaveChanges();
        }

        public static bool CadastrarDependente(Usuario Usuario, Dependente Dependente)
        {
            var ctx = new SindicatoMedicoEntities();

            try
            {
                Dependente.dep_tpa_id = ctx.Tipo_parentesco.FirstOrDefault(x => x.tpa_par_id == "D").tpa_id;
                Dependente.dep_dt_cadastro = DateTime.Now;
                ctx.Dependente.Add(Dependente);
                ctx.SaveChanges();
                if (Usuario != null)
                    LogService.CapturaOpcaoLog(Usuario, AcaoEnum.CadastrarDependente.ToString(), Dependente.dep_id);
                return true;
            }
            catch (DbEntityValidationException e)
            {
                string msgError = "Erro durante o cadastro - ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;

                throw new Exception("erro adicionar dependente: " + msgError);
            }

            return false;
        }


        public static void SalvarEnderecoSegurado(Endereco Endereco)
        {
            var ctx = new SindicatoMedicoEntities();
            var EnderecoToSave = new Endereco();


            try
            {
                //Verifica se é endereço titular ou dependente
                if (Endereco.end_seg_id != null)
                {
                    // Verifica se endereço do titular existe. Se sim, atualiza. Senão, cadastra.
                    EnderecoToSave = ctx.Endereco.FirstOrDefault(x => x.end_seg_id == Endereco.end_seg_id);
                    if (EnderecoToSave != null)
                    {
                        EnderecoToSave.end_endereco = Endereco.end_endereco;
                        EnderecoToSave.end_cidade = Endereco.end_cidade;
                        EnderecoToSave.end_estado = Endereco.end_estado;
                        EnderecoToSave.end_bairro = Endereco.end_bairro;
                    }
                    else
                        ctx.Endereco.Add(Endereco);
                }

                //Verifica se é endereço titular ou dependente
                if (Endereco.end_dep_id != null)
                {
                    // Verifica se endereço do titular existe. Se sim, atualiza. Senão, cadastra.
                    EnderecoToSave = ctx.Endereco.FirstOrDefault(x => x.end_dep_id == Endereco.end_dep_id);
                    if (EnderecoToSave != null)
                    {
                        EnderecoToSave.end_endereco = Endereco.end_endereco;
                        EnderecoToSave.end_cidade = Endereco.end_cidade;
                        EnderecoToSave.end_estado = Endereco.end_estado;
                        EnderecoToSave.end_bairro = Endereco.end_bairro;
                    }
                    else
                        ctx.Endereco.Add(Endereco);
                }

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

        public static void CadastrarEnderecoDependente(Endereco Endereco)
        {
            var ctx = new SindicatoMedicoEntities();
            Endereco.end_seg_id = Endereco.end_seg_id;
            Endereco.end_dep_id = Endereco.end_dep_id;

            try
            {
                ctx.Endereco.Add(Endereco);
                ctx.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string msgError = "Erro durante o cadastro - ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;
                throw new Exception("erro cad. end. dep.: " + msgError);
            }
        }

        public static void SalvarContatoSegurado(Contato Contato)
        {
            var ctx = new SindicatoMedicoEntities();

            var auxarrTelefone = Contato.cnt_fone.Split(' ');
            var auxarrCelular = Contato.cnt_celular.Split(' ');
            var CntToSave = new Contato();
            bool IsUpdate = false;

            if (Contato.cnt_seg_id != null)
            {
                CntToSave = ctx.Contato.FirstOrDefault(x => x.cnt_seg_id == Contato.cnt_seg_id);
                if (CntToSave != null)
                    IsUpdate = true;
                else
                    CntToSave = new Contato();
            }

            if ((auxarrTelefone[0].ToString()) != "" && (auxarrCelular[0].ToString()) != "")
            {
                CntToSave.cnt_ddd = auxarrTelefone[0].ToString().Replace("(", "").Replace(")", "");
                CntToSave.cnt_fone = auxarrTelefone[1].ToString().Replace("-", "");
                CntToSave.cnt_ddd_celular = auxarrCelular[0].ToString().Replace("(", "").Replace(")", "");
                CntToSave.cnt_celular = auxarrCelular[1].ToString().Replace("-", "");
            }
            else if ((auxarrTelefone[0].ToString()) != "" && (auxarrCelular[0].ToString()) == "")
            {
                CntToSave.cnt_ddd = auxarrTelefone[0].ToString().Replace("(", "").Replace(")", "");
                CntToSave.cnt_fone = auxarrTelefone[1].ToString().Replace("-", "");
            }
            else if ((auxarrCelular[0].ToString()) != "" && (auxarrTelefone[0].ToString()) == "")
            {
                CntToSave.cnt_ddd = auxarrTelefone[0].ToString().Replace("(", "").Replace(")", "");
                CntToSave.cnt_celular = auxarrCelular[1].ToString().Replace("-", "");
            }
            else return;

            if (Contato.cnt_seg_id != null)
                CntToSave.cnt_par_id = "T";
            if (Contato.cnt_dep_id != null)
                CntToSave.cnt_par_id = "D";
            CntToSave.cnt_cpf = Contato.cnt_cpf;
            CntToSave.cnt_seg_id = Contato.cnt_seg_id;
            CntToSave.cnt_dep_id = Contato.cnt_dep_id;

            try
            {
                // Caso flag isUpdate seja falsa, significa que irá cadastrar um novo registro.
                if (!IsUpdate)
                    ctx.Contato.Add(CntToSave);
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


        public static void CadastrarContatoDependente(Contato Contato)
        {
            var ctx = new SindicatoMedicoEntities();

            var auxarrTelefone = Contato.cnt_fone.Split(' ');
            var auxarrCelular = Contato.cnt_celular.Split(' ');

            try
            {

                if ((auxarrTelefone[0].ToString()) != "" && (auxarrCelular[0].ToString()) != "")
                {
                    Contato.cnt_ddd = auxarrTelefone[0].ToString().Replace("(", "").Replace(")", "");
                    Contato.cnt_ddd_celular = auxarrCelular[0].ToString().Replace("(", "").Replace(")", "");
                    Contato.cnt_fone = auxarrTelefone[1].ToString().Replace("-", "");
                    Contato.cnt_celular = auxarrCelular[1].ToString().Replace("-", "");
                }
                else if ((auxarrTelefone[0].ToString()) != "" && (auxarrCelular[0].ToString()) == "")
                {
                    Contato.cnt_ddd = auxarrTelefone[0].ToString().Replace("(", "").Replace(")", "");
                    Contato.cnt_fone = auxarrTelefone[1].ToString().Replace("-", "");
                }
                else if ((auxarrCelular[0].ToString()) != "" && (auxarrTelefone[0].ToString()) == "")
                {
                    Contato.cnt_ddd = auxarrTelefone[0].ToString().Replace("(", "").Replace(")", "");
                    Contato.cnt_celular = auxarrCelular[1].ToString().Replace("-", "");
                }
                else return;

                Contato.cnt_seg_id = Contato.cnt_seg_id;
                Contato.cnt_dep_id = Contato.cnt_dep_id;


                ctx.Contato.Add(Contato);
                ctx.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string msgError = "Erro durante o cadastro - ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;
                throw new Exception("erro cad. contato dep. " + msgError);
            }
        }

        public static bool VerificarUnicidadeCPF(string cpf)
        {
            var ctx = new SindicatoMedicoEntities();
            bool segCadastrado;
            bool depCadastrado;

            segCadastrado = ctx.Segurado.Any(x => x.seg_cpf == cpf);
            depCadastrado = ctx.Dependente.Any(x => x.dep_cpf == cpf);

            if (segCadastrado || depCadastrado) return true;
            else return false;
        }


        public static bool VerificarCPFSegurado(string cpf)
        {
            var ctx = new SindicatoMedicoEntities();

            bool cadastrado = ctx.Segurado.Any(x => x.seg_cpf == cpf);

            return cadastrado;
        }

        public static ICollection<Especialidade> ListarEspecialidade()
        {
            var ctx = new SindicatoMedicoEntities();
            var Esp = ctx.Especialidade;
            return Esp.ToList();
        }


        public static ICollection<Seguradora> ListarSeguradoras()
        {
            var ctx = new SindicatoMedicoEntities();
            var Seguradoras = ctx.Seguradora;
            return Seguradoras.ToList();
        }

        public static ICollection<Plano> ListarPlanos()
        {
            var ctx = new SindicatoMedicoEntities();
            var Planos = ctx.Plano.Where(x => x.pla_ativo == true).ToList();

            var listaOutPut = new List<Plano>();

            foreach (var item in Planos)
            {
                listaOutPut.Add(item);
            }

            return listaOutPut;
        }


        public static ICollection<Banco> ListarBancos()
        {
            var ctx = new SindicatoMedicoEntities();
            var Bancos = ctx.Banco.Where(x => x.ban_ativo == true);

            var listaOutPut = new List<Banco>();

            foreach (var item in Bancos)
            {
                listaOutPut.Add(item);
            }

            return listaOutPut;
        }

        public static void SalvarDadosTitular(Usuario Usuario, Segurado Segurado)
        {
            var ctx = new SindicatoMedicoEntities();

            try
            {
                var SegUpd = ctx.Segurado.FirstOrDefault(x => x.seg_cpf == Segurado.seg_cpf);

                if (SegUpd != null)
                {
                    // Captura log - salvar titular
                    if (Usuario != null)
                        LogService.CapturaOpcaoLog(Usuario, AcaoEnum.AlteracaoDadosSegurado.ToString(), Segurado.seg_id);

                    string auxSexo = Segurado.seg_sexo;
                    int? auxEstadoCivil = Segurado.seg_civ_id == 0 ? 1 : Segurado.seg_civ_id;

                    SegUpd.seg_crm = Segurado.seg_crm;
                    SegUpd.seg_crm_estado = Segurado.seg_crm_estado;
                    SegUpd.seg_esp_id = Segurado.seg_esp_id;
                    SegUpd.seg_data_nascimento = Segurado.seg_data_nascimento;
                    SegUpd.seg_sexo = auxSexo;
                    SegUpd.seg_civ_id = auxEstadoCivil;
                    SegUpd.seg_cpf = Segurado.seg_cpf;
                    SegUpd.seg_nome_mae = Segurado.seg_nome_mae;
                    SegUpd.seg_nacionalidade = Segurado.seg_nacionalidade;
                    SegUpd.seg_pispasep = Segurado.seg_pispasep;
                    SegUpd.seg_cns = Segurado.seg_cns;
                    SegUpd.seg_dn = Segurado.seg_dn;
                    SegUpd.seg_dt_filiacao = Segurado.seg_dt_filiacao;
                    SegUpd.seg_inicio_vigencia = Segurado.seg_inicio_vigencia;
                    SegUpd.seg_num_proposta = Segurado.seg_num_proposta;
                    SegUpd.seg_num_filiacao = Segurado.seg_num_filiacao;
                    SegUpd.seg_numero_carteira = Segurado.seg_numero_carteira;
                    SegUpd.seg_mdp_id = Segurado.seg_mdp_id;
                }
                               

                ctx.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string msgError = "erro salvar titular: ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;
                throw new Exception(msgError);
            }
        }

        public static void SalvarDadosDependente(Usuario Usuario, Dependente Dependente)
        {
            var ctx = new SindicatoMedicoEntities();

            try
            {
                // Captura log - salvar dependente
                if (Usuario != null)
                    LogService.CapturaOpcaoLog(Usuario, AcaoEnum.AlteracaoDadosDependente.ToString(), Dependente.dep_id);

                var Dep_upd = ctx.Dependente.FirstOrDefault(x => x.dep_id == Dependente.dep_id);

                Dep_upd.dep_civ_id = Dependente.dep_civ_id == 0 ? 1 : Dependente.dep_civ_id;
                Dep_upd.dep_sexo = Dependente.dep_sexo;
                Dep_upd.dep_data_nascimento = Dependente.dep_data_nascimento;
                Dep_upd.dep_prf_id = Dependente.dep_prf_id;
                Dep_upd.dep_cpf = Dependente.dep_cpf;
                Dep_upd.dep_nacionalidade = Dependente.dep_nacionalidade;
                Dep_upd.dep_nome_mae = Dependente.dep_nome_mae;
                Dep_upd.dep_cns = Dependente.dep_cns;
                Dep_upd.dep_pispasep = Dependente.dep_pispasep;
                Dep_upd.dep_cns = Dependente.dep_cns;
                Dep_upd.dep_dn = Dependente.dep_dn;
                Dep_upd.dep_numero_carteira = Dependente.dep_numero_carteira;
                Dep_upd.dep_email = Dependente.dep_email;
                Dep_upd.dep_inicio_vigencia = Dependente.dep_inicio_vigencia;
                Dep_upd.dep_fim_vigencia = Dependente.dep_fim_vigencia;
                
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

        public static IEnumerable<Profissao> ListarProfissoes()
        {
            var ctx = new SindicatoMedicoEntities();
            var prs = ctx.Profissao;
            return prs.ToList().OrderBy(x => x.prf_descricao);
        }

        public static IEnumerable<MelhorDiaPagamento> ListarMelhorDiaPagamento()
        {
            var ctx = new SindicatoMedicoEntities();
            var dia = ctx.MelhorDiaPagamento;
            return dia.ToList();
        }

        public static void SalvarPlanoTitular(IEnumerable<PlanoSegurado> PlanosSegurado, Usuario Usuario = null, bool CadastroTitular = false)
        {
            if (Usuario == null)
                return;
            var ctx = new SindicatoMedicoEntities();
            var PlanoSaude = new PlanoSegurado();
            var PlanoOdonto = new PlanoSegurado();
            int SegId = 0;
            bool ativarLog = false;

            // Carrega os respectivos planos
            int auxcont = 1;
            foreach (var pln in PlanosSegurado)
            {
                if (auxcont == 1)
                {
                    PlanoSaude.pls_seg_id = pln.pls_seg_id;
                    PlanoSaude.pls_pla_id = pln.pls_pla_id == 0 ? 99999 : pln.pls_pla_id;
                    PlanoSaude.pls_timestamp = DateTime.Now;
                    PlanoSaude.pls_ativo = true;
                    PlanoSaude.pls_par_id = pln.pls_par_id;
                }
                else
                {
                    PlanoOdonto.pls_pla_id = pln.pls_pla_id == 0 ? 10000 : pln.pls_pla_id;
                    PlanoOdonto.pls_seg_id = pln.pls_seg_id;
                    PlanoOdonto.pls_timestamp = DateTime.Now;
                    PlanoOdonto.pls_ativo = true;
                    PlanoOdonto.pls_par_id = pln.pls_par_id;
                }

                SegId = pln.pls_seg_id;
                auxcont++;
            }

            var PlanosSeguradoDB = ctx.PlanoSegurado.Where(x => x.pls_seg_id == SegId).ToList();

            try
            {
                if (PlanosSeguradoDB != null)
                    if (PlanosSeguradoDB.Count() > 0)
                    {
                        // Caso tenha mais de um plano cadastrado.
                        if (PlanosSeguradoDB.Count() > 1)
                        {
                            auxcont = 1;
                            foreach (var pln in PlanosSeguradoDB)
                            {
                                if (auxcont == 1)
                                {
                                    if (pln.pls_pla_id != PlanoSaude.pls_pla_id)
                                        ativarLog = true;
                                    pln.pls_pla_id = PlanoSaude.pls_pla_id;
                                }
                                else
                                {
                                    pln.pls_pla_id = PlanoOdonto.pls_pla_id;
                                    if (pln.pls_pla_id != PlanoSaude.pls_pla_id)
                                        ativarLog = true;
                                }
                                pln.pls_timestamp = DateTime.Now;
                                auxcont++;
                            }
                        }
                        // caso tenha apenas um plano cadastrado
                        else if (PlanosSeguradoDB.Count() == 1)
                        {
                            //caso tenha somente saúde, atualiza saúde e cadastra o odonto
                            if (PlanosSeguradoDB.FirstOrDefault().Plano.pla_tpp_id == 1)
                            {
                                PlanosSeguradoDB.FirstOrDefault().pls_pla_id = PlanoSaude.pls_pla_id;
                                ctx.PlanoSegurado.Add(PlanoOdonto);
                            }
                            //caso tenha somente odonto, atualiza odonto e cadastra saúde
                            if (PlanosSeguradoDB.FirstOrDefault().Plano.pla_tpp_id == 2)
                            {
                                PlanosSeguradoDB.FirstOrDefault().pls_pla_id = PlanoOdonto.pls_pla_id;
                                ctx.PlanoSegurado.Add(PlanoSaude);
                            }
                        }
                    }
                    else
                    {
                        ativarLog = true;
                        if (PlanoSaude.pls_pla_id != 99999)
                            ctx.PlanoSegurado.Add(PlanoSaude);
                        if (PlanoOdonto.pls_pla_id != 10000)
                            ctx.PlanoSegurado.Add(PlanoOdonto);
                    }

                // Carregar o valor do plano e gravar em Segurado.
                var PFEtaria = ctx.PlanoFaixaEtaria.FirstOrDefault(x => x.pla_id == PlanoSaude.pls_pla_id);
                var Segurado = ctx.Segurado.FirstOrDefault(x => x.seg_id == PlanoSaude.pls_seg_id);
                decimal VlrPlano = 0;
                if (Segurado != null)
                {
                    // Recuperando o valor do plano pela faixa etária
                    int IdadeSegurado = CarregaIdade(Segurado.seg_data_nascimento);
                    VlrPlano = CarregaValorPlanoSaudeFaixaEtaria(PlanoSaude.pls_pla_id, IdadeSegurado);
                    Segurado.seg_valor_faixa = VlrPlano;
                    // Registro da tabela histórico
                    var historico = new ReajusteFaixaEtaria() {
                        rfe_id_seg_dep = Segurado.seg_id,
                        rfe_valor_anterior = PFEtaria.pfe_premio_total,
                        rfe_valor_atualizado = VlrPlano,
                        rfe_pla_id = PlanoSaude.pls_pla_id,
                        rfe_par_id = "T",
                        rfe_data_reajuste = DateTime.Now,
                        rfe_ativo = true
                    };
                    ctx.ReajusteFaixaEtaria.Add(historico);
                }

                //Alterando valores do plano para os respectivos dependentes
                var deps = ctx.Dependente.Where(x => x.dep_cpf_titular.Equals(Segurado.seg_cpf)).ToList();
                foreach(var dp in deps)
                {
                    // verifica valor p faixa etaria
                    int idadedep = CarregaIdade(dp.dep_data_nascimento);
                    decimal vlrAnterior = dp.dep_valor_faixa;
                    dp.dep_valor_faixa = CarregaValorPlanoSaudeFaixaEtaria(PlanoSaude.pls_pla_id, idadedep);
                    // Registro da tabela histórico
                    var historico = new ReajusteFaixaEtaria()
                    {
                        rfe_id_seg_dep = dp.dep_id,
                        rfe_valor_anterior = vlrAnterior,
                        rfe_valor_atualizado = dp.dep_valor_faixa,
                        rfe_pla_id = PlanoSaude.pls_pla_id,
                        rfe_par_id = "D",
                        rfe_data_reajuste = DateTime.Now,
                        rfe_ativo = true
                    };
                    ctx.ReajusteFaixaEtaria.Add(historico);

                    // Salvar na tabela PlanoSegurado
                    var PlanoSegurado = ctx.PlanoSegurado.Where(x => x.pls_seg_id == dp.dep_id && x.pls_par_id == "D").FirstOrDefault();
                    if(PlanoSegurado != null)
                    {
                        PlanoSegurado.pls_pla_id = PlanoSaude.pls_pla_id;
                        PlanoSegurado.pls_timestamp = DateTime.Now;
                    }else
                    {
                        PlanoSegurado = new PlanoSegurado()
                        {
                            pls_seg_id = dp.dep_id,
                            pls_pla_id = PlanoSaude.pls_pla_id,
                            pls_timestamp = DateTime.Now,
                            pls_ativo = true,
                            pls_par_id = "D"
                        };
                        ctx.PlanoSegurado.Add(PlanoSegurado);
                    }
                }
                    
                                
                ctx.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string msgError = "Erro durante o cadastro - ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;
            }




            if (ativarLog && Usuario != null && CadastroTitular == false)
                // Captura log - alterar plano
                LogService.CapturaOpcaoLog(Usuario, AcaoEnum.AlteracaoPlano.ToString(), SegId);
        }

        public static void SalvarPlanoDependente(PlanoSegurado PlanoDependente, Usuario Usuario = null)
        {
            if (Usuario == null)
                return;
            var ctx = new SindicatoMedicoEntities();
            var PlanoOdonto = new PlanoSegurado();
            var PlanoSaudeDependente = new PlanoSegurado();
            try
            { 
                PlanoOdonto.pls_pla_id = PlanoDependente.pls_pla_id == 0 ? 10000 : PlanoDependente.pls_pla_id;
                PlanoOdonto.pls_seg_id = PlanoDependente.pls_seg_id;
                PlanoOdonto.pls_timestamp = DateTime.Now;
                PlanoOdonto.pls_ativo = true;
                PlanoOdonto.pls_par_id = PlanoDependente.pls_par_id;

                if (PlanoOdonto.pls_pla_id != 10000)
                    ctx.PlanoSegurado.Add(PlanoOdonto);


                // gravar valor plano faixa etaria (saúde)
                // carrega plano de saúde do titular
                var Dep = ctx.Dependente.FirstOrDefault(x => x.dep_id == PlanoDependente.pls_seg_id);
                var Titular = ctx.Segurado.Where(x => x.seg_cpf.Equals(Dep.dep_cpf_titular)).FirstOrDefault();
                var IdPlano = ctx.PlanoSegurado.Where(x => x.pls_seg_id == Titular.seg_id && x.Plano.pla_tpp_id == 1).FirstOrDefault().pls_pla_id;
                int IdadeDep = CarregaIdade(Dep.dep_data_nascimento);
                decimal vlrPlanoSaude = CarregaValorPlanoSaudeFaixaEtaria(IdPlano, IdadeDep);
                Dep.dep_valor_faixa = vlrPlanoSaude;

                // salvar na tabela PlanoSegurado
                var PlanoSegurado = ctx.PlanoSegurado.Where(x => x.pls_seg_id == PlanoDependente.pls_seg_id && x.pls_par_id == "D").FirstOrDefault();
                if(PlanoSegurado != null)
                {
                    PlanoSegurado.pls_pla_id = IdPlano;
                    PlanoSegurado.pls_timestamp = DateTime.Now;
                }else
                {
                    PlanoSegurado = new PlanoSegurado()
                    {
                        pls_seg_id = PlanoDependente.pls_seg_id,
                        pls_pla_id = IdPlano,
                        pls_timestamp = DateTime.Now,
                        pls_ativo = true,
                        pls_par_id = PlanoDependente.pls_par_id
                };

                    ctx.PlanoSegurado.Add(PlanoSegurado);
                }

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


        public static void SalvarMelhorDiaPagamento(Usuario Usuario, Segurado Segurado)
        {
            if (Usuario == null)
                return;

            var ctx = new SindicatoMedicoEntities();
            

            var seg = ctx.Segurado.FirstOrDefault(x => x.seg_id == Segurado.seg_id);
            seg.seg_mdp_id = Segurado.seg_mdp_id;

            ctx.SaveChanges();
        }


        public static void SalvarFormaPagamento(Usuario Usuario, Segurado Segurado, bool CadastroTitular = false)
        {
            if (Usuario == null)
                return;
            var ctx = new SindicatoMedicoEntities();
            bool ativaLog = false;

            var segFPag = ctx.Segurado.FirstOrDefault(x => x.seg_id == Segurado.seg_id);

            int? forPagId = ctx.Segurado.FirstOrDefault(x => x.seg_id == Segurado.seg_id).seg_for_id;

            if (Segurado.seg_for_id != forPagId)
                ativaLog = true;

            int idSegurado = Segurado.seg_id;
            if (idSegurado == 0)
                idSegurado = ctx.Segurado.FirstOrDefault(x => x.seg_cpf.Equals(Segurado.seg_cpf)).seg_id;

            ctx.Segurado.FirstOrDefault(x => x.seg_id == idSegurado).seg_for_id = Segurado.seg_for_id;
            ctx.SaveChanges();

            if (ativaLog && CadastroTitular == false)
                // registra log
                LogService.CapturaOpcaoLog(Usuario, AcaoEnum.AlteracaoFormaPagamento.ToString(), Segurado.seg_id);
        }

        public static void SalvarDadosCobranca(DadosCobranca DadosCobranca, Usuario Usuario = null)
        {
            var ctx = new SindicatoMedicoEntities();

            // Captura log - salvar dependente
            if (Usuario != null)
                LogService.CapturaOpcaoLog(Usuario, AcaoEnum.AlteracaoFormaPagamento.ToString(), DadosCobranca.dco_seg_id);
            else
                return;
            var DCobranca = ctx.DadosCobranca.Where(x => x.dco_seg_id == DadosCobranca.dco_seg_id).FirstOrDefault();

            // Se existir atualiza, senão cadastra novo
            if (DCobranca != null)
            {
                DCobranca.dco_ban_id = DadosCobranca.dco_ban_id;
                DCobranca.dco_agencia = DadosCobranca.dco_agencia;
                DCobranca.dco_conta = DadosCobranca.dco_conta;
            }
            else
            {
                DadosCobranca n_dCobranca = new DadosCobranca()
                {
                    dco_seg_id = DadosCobranca.dco_seg_id,
                    dco_agencia = DadosCobranca.dco_agencia,
                    dco_conta = DadosCobranca.dco_conta,
                    dco_ban_id = DadosCobranca.dco_ban_id
                };

                ctx.DadosCobranca.Add(n_dCobranca);
            }
            ctx.SaveChanges();
        }

        public static void SalvarContato(Contato Contato)
        {
            var ctx = new SindicatoMedicoEntities();

            try
            {
                string fone = Contato.cnt_fone;
                string celular = Contato.cnt_celular;
                string ddd = "";
                string dddCelular = "";
                if (fone.Length > 9)
                {
                    fone = Contato.cnt_fone.Substring(2, 8);
                    ddd = Contato.cnt_fone.Substring(0, 2);
                }

                if (celular.Length > 9)
                {
                    celular = Contato.cnt_celular.Substring(2, 9);
                    dddCelular = Contato.cnt_fone.Substring(0, 2);
                }

                // Verificar se é contato de titular ou dependente.
                var cnt = new Contato();
                if (Contato.cnt_par_id == "T")
                    cnt = ctx.Contato.FirstOrDefault(x => x.cnt_seg_id == Contato.cnt_seg_id);
                if (Contato.cnt_par_id == "D")
                    cnt = ctx.Contato.FirstOrDefault(x => x.cnt_dep_id == Contato.cnt_dep_id);

                if (cnt != null)
                {
                    cnt.cnt_fone = fone;
                    cnt.cnt_celular = celular;
                    cnt.cnt_ddd = ddd;
                    cnt.cnt_ddd_celular = dddCelular;
                }
                else
                {
                    var n_cnt = new Contato();
                    n_cnt.cnt_cpf = Contato.cnt_cpf;
                    n_cnt.cnt_dep_id = Contato.cnt_dep_id;
                    n_cnt.cnt_par_id = Contato.cnt_par_id;
                    n_cnt.cnt_ddd = ddd;
                    n_cnt.cnt_ddd_celular = dddCelular;
                    n_cnt.cnt_fone = fone;
                    n_cnt.cnt_celular = celular;
                    n_cnt.cnt_par_id = Contato.cnt_par_id;
                    ctx.Contato.Add(n_cnt);
                }

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

        public static void AtualizarStatus(SeguradoVM Segurado, Usuario Usuario = null)
        {
            var ctx = new SindicatoMedicoEntities();
            bool segAtivo = Segurado.Status.Equals("Ativo") ? true : false;

            //Gravação do Log Titular Ativa/Desativa Status
            // Verifica se é ativação ou desativação do segurado
            if (Segurado.Status == "Ativo")
            {
                // Verifica se o dado realmente foi alterado. Só grava log na alteração do dado
                if (ctx.Segurado.Where(x => x.seg_id == Segurado.IdSegurado).FirstOrDefault().seg_ativo == false)
                    LogService.CapturaOpcaoLog(Usuario, AcaoEnum.AtivacaoSegurado.ToString(), Segurado.IdSegurado);
            }
            else
            {
                // Verifica se o dado realmente foi alterado. Só grava log na alteração do dado
                if (ctx.Segurado.Where(x => x.seg_id == Segurado.IdSegurado).FirstOrDefault().seg_ativo == true)
                    LogService.CapturaOpcaoLog(Usuario, AcaoEnum.DesativacaoSegurado.ToString(), Segurado.IdSegurado);
            }


            ctx.Segurado.Where(x => x.seg_id == Segurado.IdSegurado).FirstOrDefault().seg_ativo = segAtivo;
            ctx.Segurado.Where(x => x.seg_id == Segurado.IdSegurado).FirstOrDefault().seg_dt_atualiza_status = DateTime.Now;
            ctx.Segurado.Where(x => x.seg_id == Segurado.IdSegurado).FirstOrDefault().seg_dt_cadastro = DateTime.Now;

            foreach (var item in Segurado.Dependentes)
            {
                //Gravação do Log Dependentes Ativa/Desativa Status
                // Verifica se é ativação ou desativação do dependente
                if (item.Status == "A")
                {
                    // Verifica se o dado realmente foi alterado. Só grava log na alteração do dado
                    if (ctx.Dependente.Where(x => x.dep_id == item.IdDependente).FirstOrDefault().dep_ativo == false)
                        LogService.CapturaOpcaoLog(Usuario, AcaoEnum.AtivacaoDependente.ToString(), item.IdDependente);
                }
                else
                {
                    // Verifica se o dado realmente foi alterado. Só grava log na alteração do dado
                    if (ctx.Dependente.Where(x => x.dep_id == item.IdDependente).FirstOrDefault().dep_ativo == true)
                        LogService.CapturaOpcaoLog(Usuario, AcaoEnum.DesativacaoDependente.ToString(), item.IdDependente);
                }

                bool depAtivo = item.Status == "A" ? true : false;
                ctx.Dependente.Where(x => x.dep_id == item.IdDependente).FirstOrDefault().dep_ativo = depAtivo;
                ctx.Dependente.Where(x => x.dep_id == item.IdDependente).FirstOrDefault().dep_dt_atualizacao = DateTime.Now;
                ctx.Dependente.Where(x => x.dep_id == item.IdDependente).FirstOrDefault().dep_dt_cadastro = DateTime.Now;
            }

            ctx.SaveChanges();
        }

        public static void AtualizarEnderecoSegurado(Endereco Endereco)
        {
            var ctx = new SindicatoMedicoEntities();
            var end = new Endereco();


            try
            {
                // Verifica se endereço é de dependente ou titular.
                if (Endereco.end_par_id == "T")
                    end = ctx.Endereco.FirstOrDefault(x => x.end_seg_id == Endereco.end_seg_id);

                if (Endereco.end_par_id == "D")
                    end = ctx.Endereco.FirstOrDefault(x => x.end_dep_id == Endereco.end_dep_id);

                if (end != null)
                {
                    end.end_endereco = Endereco.end_endereco;
                    end.end_cidade = Endereco.end_cidade;
                    end.end_bairro = Endereco.end_bairro;
                    end.end_estado = Endereco.end_estado;
                    end.end_cep = Endereco.end_cep;
                }
                else
                {
                    var n_end = new Endereco()
                    {
                        end_endereco = Endereco.end_endereco,
                        end_cidade = Endereco.end_cidade,
                        end_cpf = Endereco.end_cpf,
                        end_bairro = Endereco.end_bairro,
                        end_cep = Endereco.end_cep,
                        end_estado = Endereco.end_estado,
                        end_complemento = "",
                        end_par_id = Endereco.end_par_id,
                        end_ativo = true
                    };

                    // Verifica se é dependente ou titular
                    if (Endereco.end_par_id == "T")
                        n_end.end_seg_id = Endereco.end_seg_id;
                    if (Endereco.end_par_id == "D")
                        n_end.end_dep_id = Endereco.end_dep_id;

                    ctx.Endereco.Add(n_end);

                }
                ctx.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string msgError = "Erro durante o cadastro - ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;
                throw new Exception("erro salvar endereço: " + msgError);
            }



        }

        public static void AtualizarPlanoOdontoDependente(PlanoSegurado Plano)
        {
            var ctx = new SindicatoMedicoEntities();
            var Pla = new PlanoSegurado();
            try
            {
                 Pla = ctx.PlanoSegurado.FirstOrDefault(x => x.pls_seg_id == Plano.pls_seg_id);

                if (Pla != null)
                {
                    Pla.pls_pla_id = Plano.pls_pla_id == 0 ? 10000 : Plano.pls_pla_id;
                    Pla.pls_seg_id = Plano.pls_seg_id;
                    Pla.pls_timestamp = DateTime.Now;
                    Pla.pls_ativo = true;
                    Pla.pls_par_id = Plano.pls_par_id;
                }
                else
                {
                    var n_Pla = new PlanoSegurado()
                    {
                        pls_pla_id = Plano.pls_pla_id == 0 ? 10000 : Plano.pls_pla_id,
                        pls_seg_id = Plano.pls_seg_id,
                        pls_timestamp = DateTime.Now,
                        pls_ativo = true,
                        pls_par_id = Plano.pls_par_id
                };

                    ctx.PlanoSegurado.Add(n_Pla);

                }
                ctx.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string msgError = "Erro durante o cadastro - ";
                foreach (var eve in e.EntityValidationErrors)
                    foreach (var ve in eve.ValidationErrors)
                        msgError += ve.ErrorMessage;
                throw new Exception("erro salvar Plano Odontologico do segurado: " + msgError);
            }



        }

        public static void AtualizarContatosSegurado(Usuario Usuario, ContatoVM Contato)
        {
            var ctx = new SindicatoMedicoEntities();

            // Captura log - atualizar contatos
            if (Usuario != null)
                LogService.CapturaOpcaoLog(Usuario, AcaoEnum.AlteracaoContato.ToString(), Contato.Cpf);
            else
                return;

            var cnt = ctx.Contato.FirstOrDefault(x => x.cnt_seg_id == (int)Contato.IdTitular);

            if (cnt != null)
            {
                if (!string.IsNullOrEmpty(Contato.Telefone))
                {
                    var auxarrTelefone = Contato.Telefone.Split(' ');
                    string auxfone = auxarrTelefone[1].ToString().Replace("-", "");
                    cnt.cnt_fone = auxfone;
                    string auxddd = auxarrTelefone[0].ToString().Replace("(", "").Replace(")", "");
                    cnt.cnt_ddd = auxddd;
                }
                else
                {
                    cnt.cnt_fone = "";
                    cnt.cnt_ddd = "";
                }

                if (!string.IsNullOrEmpty(Contato.Celular))
                {
                    var auxarrCelular = Contato.Celular.Split(' ');
                    string auxcelular = auxarrCelular[1].ToString().Replace("-", "");
                    string auxdddcelular = auxarrCelular[0].ToString().Replace("(", "").Replace(")", "");

                    cnt.cnt_ddd_celular = auxdddcelular;
                    cnt.cnt_celular = auxcelular;
                }
                else
                {
                    cnt.cnt_ddd_celular = "";
                    cnt.cnt_celular = "";
                }

                // O campo email está na entidade do segurado. Logo, necessário atualizá-la.
                ctx.Segurado.FirstOrDefault(x => x.seg_cpf == Contato.Cpf).seg_email = Contato.Email;
            }
            else
            {
                var n_cnt = new Contato();
                if (!string.IsNullOrEmpty(Contato.Telefone))
                {
                    n_cnt.cnt_cpf = Contato.Cpf;
                    n_cnt.cnt_par_id = "T";
                    var auxarrTelefone = Contato.Telefone.Split(' ');
                    string auxddd = auxarrTelefone[0].ToString().Replace("(", "").Replace(")", "");
                    n_cnt.cnt_ddd = auxddd;
                    auxarrTelefone = Contato.Telefone.Split(' ');
                    string auxfone = auxarrTelefone[1].ToString().Replace("-", "");
                    n_cnt.cnt_fone = auxfone;
                }
                if (!string.IsNullOrEmpty(Contato.Celular))
                {
                    var auxarrCelular = Contato.Celular.Split(' ');
                    n_cnt.cnt_par_id = "T";
                    string auxcelular = auxarrCelular[1].ToString().Replace("-", "");
                    n_cnt.cnt_celular = auxcelular;
                    string auxdddCelular = auxarrCelular[0].ToString().Replace("(", "").Replace(")", "");
                    n_cnt.cnt_ddd_celular = auxdddCelular;
                    n_cnt.cnt_seg_id = Contato.IdTitular;
                }

                ctx.Contato.Add(n_cnt);
            }

            ctx.SaveChanges();
        }

        public static Segurado ConsultarSeguradoPorId(int Id)
        {
            var ctx = new SindicatoMedicoEntities();

            var Segurado = ctx.Segurado.Where(x => x.seg_id == Id).FirstOrDefault();
            return Segurado;
        }

        public static Segurado ConsultarSeguradoPorCpf(string cpf)
        {
            var ctx = new SindicatoMedicoEntities();

            var Segurado = ctx.Segurado.Where(x => x.seg_cpf.Equals(cpf)).FirstOrDefault();
            return Segurado;
        }

        public static IEnumerable<Dependente> ConsultarDependentesPorCpfTitular(string cpf)
        {
            var ctx = new SindicatoMedicoEntities();

            var Deps = ctx.Dependente.Where(x => x.dep_cpf_titular.Equals(cpf)).ToList();
            return Deps;
        }


        public static Contato ConsultarContatoPorIdDependente(int IdDependente)
        {
            var ctx = new SindicatoMedicoEntities();

            var Cnt = ctx.Contato.Where(x => x.cnt_dep_id == IdDependente).FirstOrDefault();
            return Cnt;
        }

        public static Contato ConsultarContatoPorCpf(string cpf)
        {
            var ctx = new SindicatoMedicoEntities();

            var Cnt = ctx.Contato.Where(x => x.cnt_cpf == cpf).FirstOrDefault();
            return Cnt;
        }

        public static Endereco ConsultarEnderecoPorIdDependente(int IdDependente)
        {
            var ctx = new SindicatoMedicoEntities();

            var End = ctx.Endereco.Where(x => x.end_dep_id == IdDependente).FirstOrDefault();
            return End;
        }

        public static PlanoSegurado ConsultarPlanoOdontoPorIdDependente(int IdDependente)
        {
            var ctx = new SindicatoMedicoEntities();

            var plano = ctx.PlanoSegurado.Where(x => x.pls_seg_id == IdDependente && x.Plano.pla_tpp_id == 2).FirstOrDefault();
            return plano;
        }

        public static Endereco ConsultarEnderecoBaseCEP(string cep)
        {
            var ctx = new CEPEntities();
            var Endereco = new Endereco();
            var LogrBaseCEP = ctx.LOG_LOGRADOURO.FirstOrDefault(x => x.CEP.Equals(cep));

            string auxTPLogr = LogrBaseCEP.TLO_TX;
            int nrBairro = LogrBaseCEP.BAI_NU_INI;
            string auxLogr = LogrBaseCEP.LOG_NO;

            Endereco.end_endereco = auxTPLogr + " " + auxLogr;
            Endereco.end_estado = LogrBaseCEP.UFE_SG;
            Endereco.end_bairro = ctx.LOG_BAIRRO.FirstOrDefault(x => x.BAI_NU == nrBairro).BAI_NO;
            Endereco.end_cidade = LogrBaseCEP.LOG_LOCALIDADE.LOC_NO;

            return Endereco;
        }

        public static Endereco ConsultarEnderecoPorCpf(string cpf)
        {
            var ctx = new SindicatoMedicoEntities();

            var End = ctx.Endereco.Where(x => x.end_cpf == cpf).FirstOrDefault();
            return End;
        }

        public static int CarregaIdade(DateTime DataNascimento)
        {
            var age = DateTime.Now.Year - DataNascimento.Year;
            return age;
        }

        public static Dependente ConsultarDependentePorId(int Id)
        {
            var ctx = new SindicatoMedicoEntities();

            var Dependente = ctx.Dependente.Where(x => x.dep_id == Id).FirstOrDefault();
            return Dependente;
        }

        public static SeguradoVM Serialize(Segurado Segurado)
        {
            var ctx = new SindicatoMedicoEntities();
            var Seg = new SeguradoVM();

            if (Segurado != null)
            {
                Seg.IdSegurado = Segurado.seg_id;
                Seg.SetNome(Segurado.seg_nome);
                Seg.Email = Segurado.seg_email;
                Seg.IdFormaPagamento = Segurado.seg_for_id;
                Seg.SetPisPasep(Segurado.seg_pispasep);
                Seg.SetCns(Segurado.seg_cns);
                Seg.NomeMae = Segurado.seg_nome_mae;
                Seg.SetDn(Segurado.seg_dn);
                if (Segurado.EstadoCivil != null)
                    Seg.IdEstadoCivil = Segurado.EstadoCivil.civ_id;
                switch (Segurado.seg_civ_id)
                {
                    case 1:
                        Seg.SetEstadoCivil("Solteiro(a)");
                        break;
                    case 2:
                        Seg.SetEstadoCivil("Casado(a)");
                        break;
                    case 3:
                        Seg.SetEstadoCivil("Viúvo(a)");
                        break;
                    case 4:
                        Seg.SetEstadoCivil("Desquitado(a)");
                        break;
                    case 5:
                        Seg.SetEstadoCivil("Divorciado(a)");
                        break;
                    case 6:
                        Seg.SetEstadoCivil("Outros");
                        break;
                    default:
                        Seg.SetEstadoCivil("");
                        break;
                }


                int auxIdEsp = Segurado.seg_esp_id != null ? (int)Segurado.seg_esp_id : 1;

                Seg.IdEspecialidade = auxIdEsp;
                Seg.Especialidade = ctx.Especialidade.FirstOrDefault(x => x.esp_id == auxIdEsp).esp_descricao;

                if (Segurado.seg_sexo == "M") Seg.SetSexo("Masculino");
                if (Segurado.seg_sexo == "F") Seg.SetSexo("Feminino");

                Seg.DataFiliacao = Segurado.seg_dt_filiacao != null ? DateTime.Parse(Segurado.seg_dt_filiacao.ToString()).ToShortDateString() : "";
                Seg.InicioVigencia = Segurado.seg_inicio_vigencia != null ? DateTime.Parse(Segurado.seg_inicio_vigencia.ToString()).ToShortDateString() : "";
                Seg.DataNasc = Segurado.seg_data_nascimento != null ? DateTime.Parse(Segurado.seg_data_nascimento.ToString()).ToShortDateString() : "";
                Seg.NrProposta = Segurado.seg_num_proposta != null ? Segurado.seg_num_proposta.ToString() : "";
                Seg.NrFiliacao = Segurado.seg_num_filiacao != null ? Segurado.seg_num_filiacao.ToString() : "";
                Seg.NrCarteirinha = Segurado.seg_numero_carteira != null ? Segurado.seg_numero_carteira.ToString() : "";

                // Carrega planos
                var PlanosSeg = ctx.PlanoSegurado.Where(x => x.pls_seg_id == Segurado.seg_id).ToList();
                if (PlanosSeg != null)
                    foreach (var item in PlanosSeg)
                    {
                        var plnVM = new PlanoVM()
                        {
                            IdPlano = item.pls_pla_id,
                            Descricao = item.Plano.pla_descricao,
                            TipoPlano = item.Plano.pla_tpp_id
                        };
                        Seg.Planos.Add(plnVM);
                    }

                Seg.IdMelhorDiaPagamento = Segurado.seg_mdp_id == null ? 0 : Segurado.seg_mdp_id;
                Seg.SetCpf(Segurado.seg_cpf);
                if (Segurado.Parentesco != null)
                    Seg.SetParentesco(Segurado.Parentesco.par_nome);
                if (Segurado.Tipo_parentesco != null)
                    Seg.TPParentesco = Segurado.Tipo_parentesco.tpa_nome;
                if (Segurado.seg_cia_id != null)
                    Seg.IdSeguradora = Segurado.seg_cia_id;
                Seg.SetCrm(Segurado.seg_crm);
                Seg.SetCrmEstado(Segurado.seg_crm_estado);
                Seg.Status = Segurado.seg_ativo == true ? "Ativo" : "Inativo";
                Seg.SetNacionalidade(Segurado.seg_nacionalidade);

                var Dependentes = ctx.Dependente.Where(x => x.dep_cpf_titular.Equals(Segurado.seg_cpf)).ToList();
                if (Dependentes != null)
                    if (Dependentes.Count() > 0)
                    {
                        foreach (var item in Dependentes)
                        {
                            string depAtivo = item.dep_ativo == true ? "Ativo" : "Inativo";
                            string eCivil = item.EstadoCivil == null ? "" : item.EstadoCivil.civ_descricao;

                            string dtInicioVig = item.dep_inicio_vigencia == null ? "" : DateTime.Parse(item.dep_inicio_vigencia.ToString()).ToShortDateString();
                            string dtFimVig = item.dep_fim_vigencia == null ? "" : DateTime.Parse(item.dep_fim_vigencia.ToString()).ToShortDateString();

                            var Dep = new DependenteVM
                            {
                                IdDependente = item.dep_id,
                                Nome = item.dep_nome,
                                Cpf = item.dep_cpf,
                                DtNascimento = item.dep_data_nascimento.ToShortDateString(),
                                Status = depAtivo,
                                Email = item.dep_email,
                                EstadoCivil = eCivil,
                                Sexo = item.dep_sexo,
                                Nacionalidade = item.dep_nacionalidade,
                                Pis = item.dep_pispasep,
                                Cns = item.dep_cns,
                                NrCarteirinha = item.dep_numero_carteira,
                                Dn = item.dep_dn,
                                DtInicioVigencia = dtInicioVig,
                                DtFinalVigencia = dtFimVig,
                                NomeMae = item.dep_nome_mae
                            };
                            // carregar profissão
                            if (item.Profissao != null)
                            {
                                Dep.IdProfissao = item.Profissao.prf_id;
                                Dep.Profissao = item.Profissao.prf_descricao;
                            }

                            // carrega combo profissões
                            var prfs = ctx.Profissao.ToList();
                            foreach (var prf in prfs)
                            {
                                var prfVM = new ProfissaoVM()
                                {
                                    Id = prf.prf_id,
                                    Nome = prf.prf_descricao
                                };
                                Dep.CmbProfissoes.Add(prfVM);
                            }

                            var DepContatos = ctx.Contato.Where(x => x.cnt_dep_id == item.dep_id).ToList();
                            if (DepContatos != null)
                                if (DepContatos.Count() > 0)
                                {
                                    Dep.Contato.cnt_ddd = DepContatos.FirstOrDefault().cnt_ddd == null ? "" : DepContatos.FirstOrDefault().cnt_ddd;
                                    Dep.Contato.cnt_ddd_celular = DepContatos.FirstOrDefault().cnt_ddd_celular == null ? "" : DepContatos.FirstOrDefault().cnt_ddd_celular;
                                    Dep.Contato.cnt_fone = DepContatos.FirstOrDefault().cnt_fone == null ? "" : DepContatos.FirstOrDefault().cnt_fone;
                                    Dep.Contato.cnt_celular = DepContatos.FirstOrDefault().cnt_celular == null ? "" : DepContatos.FirstOrDefault().cnt_celular;
                                }

                            var DepEndereco = ctx.Endereco.Where(x => x.end_dep_id == item.dep_id).ToList();
                            if (DepEndereco != null)
                                if (DepEndereco.Count() > 0)
                                {
                                    Dep.Endereco.end_endereco = DepEndereco.FirstOrDefault().end_endereco == null ? "" : DepEndereco.FirstOrDefault().end_endereco;
                                    Dep.Endereco.end_bairro = DepEndereco.FirstOrDefault().end_bairro == null ? "" : DepEndereco.FirstOrDefault().end_bairro;
                                    Dep.Endereco.end_cep = DepEndereco.FirstOrDefault().end_cep == null ? "" : DepEndereco.FirstOrDefault().end_cep;
                                    Dep.Endereco.end_cidade = DepEndereco.FirstOrDefault().end_cidade == null ? "" : DepEndereco.FirstOrDefault().end_cidade;
                                    Dep.Endereco.end_estado = DepEndereco.FirstOrDefault().end_estado == null ? "" : DepEndereco.FirstOrDefault().end_estado;
                                }

                            var PlanosDep = ctx.PlanoSegurado.Where(x => x.pls_seg_id == item.dep_id && x.Plano.pla_tpp_id == 2).ToList();
                            if (PlanosDep != null)
                            {
                                if (PlanosDep.Count() > 0)
                                {
                                    Dep.IdPlanoOdonto = PlanosDep.FirstOrDefault().pls_pla_id;
                                    Dep.PlanoOdonto = PlanosDep.FirstOrDefault().Plano.pla_descricao;
                                }
                            }

                            var PlanosOdontoDep = ctx.Plano.Where(x => x.pla_ativo == true && x.pla_tpp_id == 2).ToList();

                            foreach (var planoO in PlanosOdontoDep)
                            {
                                var planoOVM = new PlanoVM()
                                {
                                    IdPlano = planoO.pla_id,
                                    Descricao = planoO.pla_descricao,
                                    TipoPlano = planoO.pla_tpp_id
                                };

                                Dep.Planos.Add(planoOVM);
                            }

                            Seg.Dependentes.Add(Dep);
                        }

                    }

                var Enderecos = ctx.Endereco.Where(x => x.end_seg_id == Segurado.seg_id).ToList();
                if (Enderecos != null)
                    if (Enderecos.Count() > 0)
                    {
                        var End = new Endereco
                        {
                            end_endereco = Enderecos[0].end_endereco == null ? "" : Enderecos[0].end_endereco,
                            end_bairro = Enderecos[0].end_bairro == null ? "" : Enderecos[0].end_bairro,
                            end_cidade = Enderecos[0].end_cidade == null ? "" : Enderecos[0].end_cidade,
                            end_complemento = Enderecos[0].end_complemento == null ? "" : Enderecos[0].end_complemento,
                            end_estado = Enderecos[0].end_estado == null ? "" : Enderecos[0].end_estado,
                            end_cep = Enderecos[0].end_cep == null ? "" : Enderecos[0].end_cep
                        };
                        Seg.Enderecos.Add(End);
                    }

                var Contatos = ctx.Contato.Where(x => x.cnt_seg_id == Segurado.seg_id).ToList();
                if (Contatos != null)
                {
                    if (Contatos.Count() > 0)
                    {
                        var Contato = new Contato
                        {
                            cnt_par_id = Contatos[0].cnt_par_id,
                            cnt_cpf = Contatos[0].cnt_cpf,
                            cnt_ddd_celular = Contatos[0].cnt_ddd_celular == null ? "" : Contatos[0].cnt_ddd_celular,
                            cnt_ddd = Contatos[0].cnt_ddd == null ? "" : Contatos[0].cnt_ddd,
                            cnt_fone = Contatos[0].cnt_fone == null ? "" : Contatos[0].cnt_fone,
                            cnt_celular = Contatos[0].cnt_celular == null ? "" : Contatos[0].cnt_celular
                        };

                        Seg.Contatos.Add(Contato);
                    }

                }

                if (Segurado.DadosCobranca.Count() > 0)
                {
                    foreach (var item in Segurado.DadosCobranca)
                    {
                        var dCobranca = new DadosCobranca()
                        {
                            dco_id = item.dco_id,
                            dco_agencia = item.dco_agencia,
                            dco_conta = item.dco_conta,
                            dco_ban_id = item.dco_ban_id
                        };

                        Seg.DadosCobranca.Add(dCobranca);
                    }
                }
            }

            return Seg;
        }

        public static ICollection<SeguradoVM> Serialize(ICollection<Segurado> Segurados)
        {
            List<SeguradoVM> lista = new List<SeguradoVM>();

            int maxCount = Segurados.Count();

            foreach (var item in Segurados)
            {
                var Seg = new SeguradoVM();

                Seg.IdSegurado = item.seg_id;
                Seg.SetNome(item.seg_nome);
                Seg.SetCpf(item.seg_cpf);
                Seg.SetParentesco(item.Parentesco.par_nome);
                Seg.TPParentesco = item.Tipo_parentesco.tpa_nome;
                Seg.SetCrm(item.seg_crm);
                Seg.MaxCount = maxCount;
                Seg.Status = item.seg_ativo == true ? "Ativo" : "Inativo";

                lista.Add(Seg);
            }

            return lista;
        }


        private static decimal CarregaValorPlanoSaudeFaixaEtaria(int IdPlano, int Idade)
        {
            var ctx = new SindicatoMedicoEntities();
            var PlanoFaixaEtaria = ctx.PlanoFaixaEtaria.Where(x => x.pla_id == IdPlano && (x.pfe_faixa_inicial <= Idade && x.pfe_faixa_final >= Idade)).FirstOrDefault();
            if (PlanoFaixaEtaria != null)
                return PlanoFaixaEtaria.pfe_premio_total;            
            return 0;
        }
    }
}
