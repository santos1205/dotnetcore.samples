using BaseAccess.Enums;
using BaseAccess.VModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace BaseAccess
{
    public class LogService
    {
        public static ICollection<Acao> ListarAcoesLog()
        {
            var ctx = new SindicatoMedicoEntities();

            return ctx.Acao.ToList();
        }

        public static ICollection<Usuario> ListarUsuarios(bool? orderByName = false)
        {
            var ctx = new SindicatoMedicoEntities();

            if(orderByName == true)
                return ctx.Usuario.Where(x => x.usr_aprovado == "A").OrderBy(y => y.usr_nome).ToList();
            else
                return ctx.Usuario.Where(x => x.usr_aprovado == "A").ToList();
        }

        public static ICollection<Log> ListarPorParams(int IdUsuario, int IdAcao, DateTime PInicio, DateTime PFim)
        {

            var ctx = new SindicatoMedicoEntities();
            IEnumerable<Log> lista = ctx.Log;

            lista = lista.Where(x => x.log_data >= PInicio.AddDays(-1) && x.log_data <= PFim.AddDays(1));

            if (IdAcao != 0)
                lista = lista.Where(x => x.Acao.aco_id == IdAcao);
            if (IdUsuario != 0)
                lista = lista.Where(x => x.Usuario.usr_id == IdUsuario);
            
            return lista.OrderByDescending(x => x.log_data).ToList();
        }

        public static void CapturaOpcaoLog(Usuario Usuario, string Acao, string cpf)
        {
            var ctx = new SindicatoMedicoEntities();
            var Segurado = ctx.Segurado.FirstOrDefault(x => x.seg_cpf == cpf);
            CapturaOpcaoLog(Usuario, Acao, Segurado.seg_id);
        }

        public static void CapturaOpcaoLogUsuario(Usuario UsuarioLogado, int IdAcao, int IdUsuario)
        {
            var ctx = new SindicatoMedicoEntities();
            CapturaLog(UsuarioLogado, IdAcao, IdUsuario);
        }

        public static void CapturaOpcaoLog(Usuario Usuario, string Acao, int IdSegurado = 0)
        {
            #region classificaAcao
            switch (Acao)
            {
                case "Login":
                    CapturaLog(Usuario, (int)AcaoEnum.Login);
                    break;
                case "RecuperacaoSenha":
                    CapturaLog(Usuario, (int)AcaoEnum.RecuperacaoSenha);
                    break;
                case "RedefinicaoSenha":
                    CapturaLog(Usuario, (int)AcaoEnum.RedefinicaoSenha);
                    break;
                case "AprovacaoUsuario":
                    CapturaLog(Usuario, (int)AcaoEnum.AprovacaoUsuario, IdSegurado);
                    break;
                case "AtivacaoSegurado":
                    CapturaLog(Usuario, (int)AcaoEnum.AtivacaoSegurado, IdSegurado);
                    break;
                case "DesativacaoSegurado":
                    CapturaLog(Usuario, (int)AcaoEnum.DesativacaoSegurado, IdSegurado);
                    break;
                case "AtivacaoDependente":
                    CapturaLog(Usuario, (int)AcaoEnum.AtivacaoDependente, IdSegurado);
                    break;
                case "DesativacaoDependente":
                    CapturaLog(Usuario, (int)AcaoEnum.DesativacaoDependente, IdSegurado);
                    break;
                case "AlteracaoDadosSegurado":
                    CapturaLog(Usuario, (int)AcaoEnum.AlteracaoDadosSegurado, IdSegurado);
                    break;
                case "AlteracaoDadosDependente":
                    CapturaLog(Usuario, (int)AcaoEnum.AlteracaoDadosDependente, IdSegurado);
                    break;
                case "AlteracaoContato":
                    CapturaLog(Usuario, (int)AcaoEnum.AlteracaoContato, IdSegurado);
                    break;
                case "AlteracaoPlano":
                    CapturaLog(Usuario, (int)AcaoEnum.AlteracaoPlano, IdSegurado);
                    break;
                case "AlteracaoFormaPagamento":
                    CapturaLog(Usuario, (int)AcaoEnum.AlteracaoFormaPagamento, IdSegurado);
                    break;
                case "BaixaPagamentoManual":
                    CapturaLog(Usuario, (int)AcaoEnum.BaixaPagamentoManual, IdSegurado);
                    break;
                case "ReprovacaoUsuario":
                    CapturaLog(Usuario, (int)AcaoEnum.ReprovacaoUsuario, IdSegurado);
                    break;
                case "CadastrarSegurado":
                    CapturaLog(Usuario, (int)AcaoEnum.CadastrarSegurado, IdSegurado);
                    break;
                case "CadastrarDependente":
                    CapturaLog(Usuario, (int)AcaoEnum.CadastrarDependente, IdSegurado);
                    break;
                case "BaixaPagamentoAutomatica":
                    CapturaLog(Usuario, (int)AcaoEnum.BaixaPagamentoAutomatica, IdSegurado);
                    break;
                case "AlteracaoPlanoSaudeTitularAPedido":
                    CapturaLog(Usuario, (int)AcaoEnum.AlteracaoPlanoSaudeTitularAPedido, IdSegurado);
                    break;
                case "AlteracaoPlanoSaudeDependenteAPedido":
                    CapturaLog(Usuario, (int)AcaoEnum.AlteracaoPlanoSaudeDependenteAPedido, IdSegurado);
                    break;
                case "AlteracaoPlanoOdontoTitularAPedido":
                    CapturaLog(Usuario, (int)AcaoEnum.AlteracaoPlanoOdontoTitularAPedido, IdSegurado);
                    break;
                case "AlteracaoPlanoOdontoDependenteAPedido":
                    CapturaLog(Usuario, (int)AcaoEnum.AlteracaoPlanoOdontoDependenteAPedido, IdSegurado);
                    break;
                case "AlteracaoPlanoOdontoDependentePorFaixaEtaria":
                    CapturaLog(Usuario, (int)AcaoEnum.AlteracaoPlanoOdontoDependentePorFaixaEtaria, IdSegurado);
                    break;
                case "AlteracaoPlanoSaudeDependentePorFaixaEtaria":
                    CapturaLog(Usuario, (int)AcaoEnum.AlteracaoPlanoSaudeDependentePorFaixaEtaria, IdSegurado);
                    break;
                case "ConsultarBaixaPagamento":
                    CapturaLog(Usuario, (int)AcaoEnum.ConsultarBaixaPagamento, IdSegurado);
                    break;
                case "ConsultarFaturamento":
                    CapturaLog(Usuario, (int)AcaoEnum.ConsultarFaturamento, IdSegurado);
                    break;
                case "ConsultarMudancaFaixa":
                    CapturaLog(Usuario, (int)AcaoEnum.ConsultarMudancaFaixa, IdSegurado);
                    break;
                case "InicioProcessoGeracaoPagamento":
                    CapturaLog(Usuario, (int)AcaoEnum.InicioProcessoGeracaoPagamento, IdSegurado);
                    break;
                case "FimProcessoGeracaoPagamento":
                    CapturaLog(Usuario, (int)AcaoEnum.FimProcessoGeracaoPagamento, IdSegurado);
                    break;
                case "AlteracaoBaixaPagamentoManual":
                    CapturaLog(Usuario, (int)AcaoEnum.AlteracaoBaixaPagamentoManual, IdSegurado);
                    break;
                case "AlteracaoBaixaPagamentoAutomatica":
                    CapturaLog(Usuario, (int)AcaoEnum.AlteracaoBaixaPagamentoAutomatica, IdSegurado);
                    break;
                case "AlteracaoConfiguracaoArquivoBoleto":
                    CapturaLog(Usuario, (int)AcaoEnum.AlteracaoConfiguracaoArquivoBoleto, IdSegurado);
                    break;
                default:
                    break;
            }
            #endregion
        }

        public static ICollection<LogVM> Serialize(ICollection<Log> Logs)
        {
            List<LogVM> lista = new List<LogVM>();

            int maxCount = Logs.Count();

            foreach (var item in Logs)
            {
                var Log = new LogVM();

                Log.Id = item.log_id;
                Log.Nome = item.Usuario.usr_nome;
                Log.Departamento = item.Usuario.NivelAcesso.nvl_descricao;
                Log.ActLog = item.Acao.aco_nome;
                if (!string.IsNullOrEmpty(item.log_cpf))
                {
                    var ctx = new SindicatoMedicoEntities();

                    // Verifica se a ação do log está relacionado com dependentes. Se sim, consulta na tbl Dependente.
                    if (item.Acao.aco_nome.Contains("dependente"))
                    {
                        var segAux = ctx.Dependente.FirstOrDefault(x => x.dep_cpf == item.log_cpf);
                        if (segAux != null)
                            Log.NomeSegurado = segAux.dep_nome;
                    }
                    else
                    {
                        var segAux = ctx.Segurado.FirstOrDefault(x => x.seg_cpf == item.log_cpf);
                        if (segAux != null)
                            Log.NomeSegurado = segAux.seg_nome;
                    }  
                }
                Log.Data = item.log_data.ToString();
                    

                lista.Add(Log);
            }

            return lista;
        }

        private static void CapturaLog(Usuario Usuario, int IdAcao, int IdSeg_usr = 0)
        {
            var ctx = new SindicatoMedicoEntities();
            
            var Acao = ctx.Acao.FirstOrDefault(x => x.aco_id == IdAcao);
            string cpfSeg_Usr = "";            
            if (IdSeg_usr != 0 && IdAcao != (int)AcaoEnum.AprovacaoUsuario 
                    && IdAcao != (int)AcaoEnum.ReprovacaoUsuario
                    && IdAcao != (int)AcaoEnum.AtivacaoDependente
                    && IdAcao != (int)AcaoEnum.DesativacaoDependente
                    && IdAcao != (int)AcaoEnum.AlteracaoDadosDependente
                    && IdAcao != (int)AcaoEnum.CadastrarDependente
                    && IdAcao != (int)AcaoEnum.AlteracaoNivelAcessoUsuario)
                        cpfSeg_Usr = ctx.Segurado.FirstOrDefault(x => x.seg_id == IdSeg_usr).seg_cpf;
            // Caso a ação for ativação/desativação do dependente/salvar dados dependente, o IdSeg_usr é do dependente
            else if (IdSeg_usr != 0 
                    && (IdAcao == (int)AcaoEnum.AtivacaoDependente 
                    || IdAcao == (int)AcaoEnum.DesativacaoDependente)
                    || IdAcao == (int)AcaoEnum.AlteracaoDadosDependente
                    || IdAcao == (int)AcaoEnum.CadastrarDependente)
                        cpfSeg_Usr = ctx.Dependente.FirstOrDefault(x => x.dep_id == IdSeg_usr).dep_cpf;                
            // Caso a ação for aprovação/reprovação usuário, o IdSeg_usr é do usuario e não do segurado
            else if (IdSeg_usr != 0 && IdAcao == (int)AcaoEnum.AprovacaoUsuario 
                    || IdAcao == (int)AcaoEnum.ReprovacaoUsuario
                    || IdAcao == (int)AcaoEnum.AlteracaoNivelAcessoUsuario)
                        cpfSeg_Usr = ctx.Usuario.FirstOrDefault(x => x.usr_id == IdSeg_usr).usr_cpf;

            try
            {
                var n_log = new Log();
                if (Acao != null)

                    #region ClassificaAcao
                    switch (Acao.aco_nome)
                    {
                        case "Login":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_cpf = cpfSeg_Usr,
                                log_ativo = true
                            };
                            break;
                        case "Recuperação de senha":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_cpf = cpfSeg_Usr,
                                log_ativo = true
                            };
                            break;
                        case "Redefinição de senha":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_cpf = cpfSeg_Usr,
                                log_ativo = true
                            };
                            break;
                        case "Aprovação de usuário":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_cpf = cpfSeg_Usr,
                                log_ativo = true
                            };
                            break;
                       
                        case "Ativação de segurado":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_cpf = cpfSeg_Usr,
                                log_par_id = "T",
                                log_ativo = true
                            };
                            break;
                        case "Desativação de segurado":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_cpf = cpfSeg_Usr,
                                log_par_id = "T",
                                log_ativo = true
                            };
                            break;
                        case "Ativação de dependente":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_cpf = cpfSeg_Usr,
                                log_par_id = "D",
                                log_ativo = true
                            };
                            break;
                        case "Desativação de dependente":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_cpf = cpfSeg_Usr,
                                log_par_id = "D",
                                log_ativo = true
                            };
                            break;
                        case "Alteração de dados do segurado":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_cpf = cpfSeg_Usr,
                                log_par_id = "T",
                                log_ativo = true
                            };
                            break;
                        case "Alteração de dados do dependente":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_cpf = cpfSeg_Usr,
                                log_par_id = "D",
                                log_ativo = true
                            };
                            break;
                        case "Alteração de contato":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_cpf = cpfSeg_Usr,
                                log_par_id = "T",
                                log_ativo = true
                            };
                            break;
                        case "Alteração de plano":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_cpf = cpfSeg_Usr,
                                log_par_id = "T",
                                log_ativo = true
                            };
                            break;
                        case "Alteração de forma de pagamento":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_cpf = cpfSeg_Usr,
                                log_par_id = "T",
                                log_ativo = true
                            };
                            break;
                        case "Baixa de pagamento manual":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_cpf = cpfSeg_Usr,
                                log_usr_id = Usuario.usr_id,
                                log_ativo = true
                            };
                            break;
                        case "Reprovação de usuário":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_cpf = cpfSeg_Usr,
                                log_ativo = true
                            };
                            break;
                        case "Cadastro de titular":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_cpf = cpfSeg_Usr,
                                log_par_id = "T",
                                log_ativo = true
                            };
                            break;
                        case "Cadastro de dependente":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_cpf = cpfSeg_Usr,
                                log_par_id = "D",
                                log_ativo = true
                            };
                            break;
                        case "Baixa de pagamento automática com sucesso":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_ativo = true
                            };
                            break;
                        case "Plano de saúde do segurado atualizado a pedido":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_ativo = true
                            };
                            break;
                        case "Plano de saúde do dependente atualizado a pedido":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_ativo = true
                            };
                            break;
                        case "Plano odontológico do segurado atualizado a pedido":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_ativo = true
                            };
                            break;
                        case "Plano odontológico do dependente atualizado a pedido":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_ativo = true
                            };
                            break;
                        case "Início processo atualização faixa etária":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_ativo = true
                            };
                            break;
                        case " Fim processo atualização faixa etária":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_ativo = true
                            };
                            break;
                        case "Consultar baixa de pagamento":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_ativo = true
                            };
                            break;
                        case "Consultar faturamento":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_ativo = true
                            };
                            break;
                        case "Consultar mudança faixa etária":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_ativo = true
                            };
                            break;
                        case "Início processo geraçã pagamento":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_ativo = true
                            };
                            break;
                        case "Fim proesso geração pagamento":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_ativo = true
                            };
                            break;
                      case "Alteração de baixa de pagamento":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_cpf = cpfSeg_Usr,
                                log_par_id = "T",
                                log_ativo = true
                            };
                            break;
                        case "Baixa de pagamento automática com erro":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_cpf = cpfSeg_Usr,
                                log_par_id = "T",
                                log_ativo = true
                            };
                            break;
                        case "Alteração de configuração do arquivo do boleto":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,                                
                                log_ativo = true
                            };
                            break;
                        case "Alteração de nível de acesso do usuário":
                            n_log = new Log()
                            {
                                log_aco_id = IdAcao,
                                log_data = DateTime.Now,
                                log_usr_id = Usuario.usr_id,
                                log_ativo = true
                            };
                            break;
                        default:
                            break;
                    }
                #endregion

                ctx.Log.Add(n_log);
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

    }
}
