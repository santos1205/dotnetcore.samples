using QuestionarioCOrg.DataAccess;
using QuestionarioCOrg.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionarioCOrg.Service
{
    public class ResultadoService
    {


        public static ResultadosVM ConsolidarResultadoBasico(ResultadosVM VM, bool state = true)
        {            
            if (!state)
            {
                VM = new ResultadosVM();
                VM.Respostas = new List<RespostaUsuario>();
                return VM;
            }
            
            
            return VM;
        }

        public static ResultadosVM ConsolidarResultadoCOrg(ResultadosVM VM, bool Consolidado = false)
        {
            var ObjCOrgs = new List<ResultadosCOrgVM>();
            var db = new QuestionarioOrgDBEntities();
            IEnumerable<Media> ObjMds = db.Media;

            if (!Consolidado)
            {
                #region dummyData
                //ObjCOrgs.Add(new ResultadosCOrgVM
                //{
                //    Nome = "Mario Santos",
                //    Departamento = "DTI",
                //    Cargo = "Desenvolvedor",
                //    Cultura = "hierárquico",
                //    PontuacaoAtual = "20.05",
                //    PontuacaoDesejado = "32.33",
                //    Data = "01/12/2019"
                //});
                //ObjCOrgs.Add(new ResultadosCOrgVM
                //{
                //    Nome = "Daniel San",
                //    Departamento = "DTI",
                //    Cargo = "Desenvolvedor",
                //    Cultura = "hierárquico",
                //    PontuacaoAtual = "11.05",
                //    PontuacaoDesejado = "22.33",
                //    Data = "01/12/2019"
                //});
                #endregion
                
                if (VM.IdUsuarioRespondente != 0)                
                    ObjMds = db.Media.Where(x => x.Usuario.usu_id == VM.IdUsuarioRespondente);
                else
                    ObjMds = db.Media.Where(x => x.Usuario.Empresa.emp_id == VM.IdEmpresa 
                                                && x.Usuario.Departamento.dpt_id == VM.IdDepartamento);

                string Cultura = string.Empty;
                foreach(var md in ObjMds)
                {
                    switch(md.mda_item)
                    {
                        case "A": Cultura = "Familiar"; break;
                        case "B": Cultura = "Inovadora"; break;
                        case "C": Cultura = "Mercado"; break;
                        case "D": Cultura = "Hierárquica"; break;
                        default: break;
                    }

                    ObjCOrgs.Add(new ResultadosCOrgVM
                    {
                        Nome = md.Usuario.usu_nome,
                        Departamento = md.Usuario.Departamento.dpt_nome,
                        Cargo = md.Usuario.usu_cargo,
                        Cultura = Cultura,
                        PontuacaoAtual = md.mda_valor_atual.ToString(),
                        PontuacaoDesejado = md.mda_valor_desejado.ToString(),
                        Data = md.mda_dt_cadastro != null ? DateTime.Parse(md.mda_dt_cadastro.ToString()).ToShortDateString() : "" 
                    });
                }

                VM.RespostasCOrg = ObjCOrgs;
            }
            else
            {   
                ObjMds = db.Media.Where(x => x.Usuario.Empresa.emp_id == VM.IdEmpresa);

                string Cultura = string.Empty;
                foreach (var md in ObjMds)
                {
                    switch (md.mda_item)
                    {
                        case "A": Cultura = "Familiar"; break;
                        case "B": Cultura = "Inovadora"; break;
                        case "C": Cultura = "Mercado"; break;
                        case "D": Cultura = "Hierárquica"; break;
                        default: break;
                    }
                    ObjCOrgs.Add(new ResultadosCOrgVM
                    {
                        Nome = md.Usuario.usu_nome,
                        Departamento = md.Usuario.Departamento.dpt_nome,
                        Cargo = md.Usuario.usu_cargo,
                        Cultura = Cultura,
                        PontuacaoAtual = md.mda_valor_atual.ToString(),
                        PontuacaoDesejado = md.mda_valor_desejado.ToString(),
                        Data = md.mda_dt_cadastro != null ? DateTime.Parse(md.mda_dt_cadastro.ToString()).ToShortDateString() : ""
                    });
                }
                #region DummyData
                //ObjCOrgs.Add(new ResultadosCOrgVM
                //{
                //    Departamento = "DTI",
                //    PontuacaoAtual = "20.05",
                //    PontuacaoDesejado = "32.33",
                //    Data = "01/12/2019"
                //});
                //ObjCOrgs.Add(new ResultadosCOrgVM
                //{
                //    Departamento = "DTI",
                //    PontuacaoAtual = "11.05",
                //    PontuacaoDesejado = "22.33",
                //    Data = "01/12/2019"
                //});
                #endregion

                VM.RespostasCOrg = ConsolidarCORG(ObjCOrgs);
            }

            return VM;
        }

        #region Private methods
        private static IEnumerable<RespostaUsuario> ConsolidarPorQuestionario(ResultadosVM VM)
        {
            IEnumerable<RespostaUsuario> ListaConsolidada = new List<RespostaUsuario>();

            int IdQuestionarioAConsolidar = (int)VM.IdQuestionario;

            VM.Respostas = VM.Respostas.Where(x => x.Questionario.qst_id == IdQuestionarioAConsolidar);
            if (VM.Respostas.Count() > 0)
                ListaConsolidada = Consolidar(VM);

            return ListaConsolidada;
        }

        private static IEnumerable<ResultadosCOrgVM> ConsolidarCORG(List<ResultadosCOrgVM> VMs)
        {            
            VMs = VMs.OrderBy(x => x.Departamento).ToList();
            if (VMs.Count() == 0)
                return new List<ResultadosCOrgVM>();
            string deptoAnterior = VMs.FirstOrDefault().Departamento;            
            var VMsConsolidadas = new List<ResultadosCOrgVM>();
            int ContUsuariosFamiliar = 0;
            int ContUsuariosInovadora = 0;
            int ContUsuariosMercado = 0;
            int ContUsuariosHierarquico = 0;

            decimal VlrAtualSomaFamiliar = 0;
            decimal VlrAtualSomaInovadora = 0;
            decimal VlrAtualSomaMercado = 0;
            decimal VlrAtualSomaHierarquico = 0;

            decimal VlrDesejadoSomaFamiliar = 0;
            decimal VlrDesejadoSomaInovadora = 0;
            decimal VlrDesejadoSomaMercado = 0;
            decimal VlrDesejadoSomaHierarquico = 0;

            var LastElem = VMs.LastOrDefault();
            var blnAdicionarConsolidacao = false;
            for(int i = 0; i < VMs.Count(); i++)
            {
                bool IsLast = false;
                // Flag de ultimo elemento
                if (VMs[i] == LastElem)
                    IsLast = true;
                // Se não for ultimo registro e o proximo registro for outro depto, add consolidação                    
                if (deptoAnterior != VMs[i].Departamento)
                    blnAdicionarConsolidacao = true;
                
                if (blnAdicionarConsolidacao || IsLast)
                {
                    //FAMILIAR - Calcular media por depto valor atual e valor desejado                                        
                    VMsConsolidadas.Add(new ResultadosCOrgVM
                    {
                        Departamento = deptoAnterior,
                        Data = VMs[i].Data,
                        Cultura = "Familiar",
                        PontuacaoAtual = CommonService.FormatarCasasDecimais(VlrAtualSomaFamiliar / ContUsuariosFamiliar),
                        PontuacaoDesejado = CommonService.FormatarCasasDecimais(VlrDesejadoSomaFamiliar / ContUsuariosFamiliar)
                    });

                    // zerando valores para próxima consolidação
                    ContUsuariosFamiliar = 0;
                    VlrAtualSomaFamiliar = 0;
                    VlrDesejadoSomaFamiliar = 0;

                    //INOVADORA - Calcular media por depto valor atual e valor desejado
                    VMsConsolidadas.Add(new ResultadosCOrgVM
                    {
                        Departamento = deptoAnterior,
                        Data = VMs[i].Data,
                        Cultura = "Inovadora",
                        PontuacaoAtual = CommonService.FormatarCasasDecimais(VlrAtualSomaInovadora / ContUsuariosInovadora),
                        PontuacaoDesejado = CommonService.FormatarCasasDecimais(VlrDesejadoSomaInovadora / ContUsuariosInovadora)
                    });
                    // zerando valores para próxima consolidação
                    ContUsuariosInovadora = 0;
                    VlrAtualSomaInovadora = 0;
                    VlrDesejadoSomaInovadora = 0;

                    //MERCADO - Calcular media por depto valor atual e valor desejado                    
                    VMsConsolidadas.Add(new ResultadosCOrgVM
                    {
                        Departamento = deptoAnterior,
                        Data = VMs[i].Data,
                        Cultura = "Mercado",
                        PontuacaoAtual = CommonService.FormatarCasasDecimais(VlrAtualSomaMercado / ContUsuariosMercado),
                        PontuacaoDesejado = CommonService.FormatarCasasDecimais(VlrDesejadoSomaMercado / ContUsuariosMercado)
                    });
                    // zerando valores para próxima consolidação
                    VlrAtualSomaMercado = 0;
                    VlrDesejadoSomaMercado = 0;
                    ContUsuariosMercado = 0;

                    //HIERÁRQUICO - Calcular media por depto valor atual e valor desejado
                    if (IsLast)
                    {
                        if (VMs[i].Cultura.Equals("Hierárquica"))
                        {
                            VlrAtualSomaHierarquico += VMs[i].PontuacaoAtual != null ? decimal.Parse(VMs[i].PontuacaoAtual) : 0;
                            VlrDesejadoSomaHierarquico += VMs[i].PontuacaoDesejado != null ? decimal.Parse(VMs[i].PontuacaoDesejado) : 0;
                            ContUsuariosHierarquico++;
                        }
                    }
                    
                    VMsConsolidadas.Add(new ResultadosCOrgVM
                    {
                        Departamento = deptoAnterior,
                        Data = VMs[i].Data,
                        Cultura = "Hierárquica",
                        PontuacaoAtual = CommonService.FormatarCasasDecimais(VlrAtualSomaHierarquico / ContUsuariosHierarquico),
                        PontuacaoDesejado = CommonService.FormatarCasasDecimais(VlrDesejadoSomaHierarquico / ContUsuariosHierarquico)
                    });
                    // zerando valores para próxima consolidação
                    ContUsuariosHierarquico = 0;
                    VlrAtualSomaHierarquico = 0;
                    VlrDesejadoSomaHierarquico = 0;

                    if (VMs[i] != LastElem)
                        deptoAnterior = VMs[i].Departamento;
                    blnAdicionarConsolidacao = false;
                }

                // Calcular qtde de registros p/ depto e cultura
                // Familiar
                if (VMs[i].Cultura.Equals("Familiar"))
                {
                    VlrAtualSomaFamiliar += VMs[i].PontuacaoAtual != null ? decimal.Parse(VMs[i].PontuacaoAtual) : 0;
                    VlrDesejadoSomaFamiliar += VMs[i].PontuacaoDesejado != null ? decimal.Parse(VMs[i].PontuacaoDesejado) : 0;
                    ContUsuariosFamiliar++;
                }
                // Inovadora
                if (VMs[i].Cultura.Equals("Inovadora"))
                {
                    VlrAtualSomaInovadora += VMs[i].PontuacaoAtual != null ? decimal.Parse(VMs[i].PontuacaoAtual) : 0;
                    VlrDesejadoSomaInovadora += VMs[i].PontuacaoDesejado != null ? decimal.Parse(VMs[i].PontuacaoDesejado) : 0;
                    ContUsuariosInovadora++;
                }
                // Mercado
                if (VMs[i].Cultura.Equals("Mercado"))
                {
                    VlrAtualSomaMercado += VMs[i].PontuacaoAtual != null ? decimal.Parse(VMs[i].PontuacaoAtual) : 0;
                    VlrDesejadoSomaMercado += VMs[i].PontuacaoDesejado != null ? decimal.Parse(VMs[i].PontuacaoDesejado) : 0;
                    ContUsuariosMercado++;
                }
                // Hierárquica
                if (VMs[i].Cultura.Equals("Hierárquica"))
                {
                    VlrAtualSomaHierarquico += VMs[i].PontuacaoAtual != null ? decimal.Parse(VMs[i].PontuacaoAtual) : 0;
                    VlrDesejadoSomaHierarquico += VMs[i].PontuacaoDesejado != null ? decimal.Parse(VMs[i].PontuacaoDesejado) : 0;
                    ContUsuariosHierarquico++;
                }
            }            
            
            return VMsConsolidadas;
        }

        private static IEnumerable<RespostaUsuario> ConsolidarPorEmpresa(ResultadosVM VM)
        {            
            IEnumerable<RespostaUsuario> ListaConsolidada = new List<RespostaUsuario>();
            
            int IdEmpresaAConsolidar = (int)VM.IdEmpresa;

            VM.Respostas = VM.Respostas.Where(x => x.Empresa.emp_id == IdEmpresaAConsolidar);

            if (VM.Respostas.Count() > 0)
                ListaConsolidada = Consolidar(VM);

            return ListaConsolidada;
        }
        private static IEnumerable<RespostaUsuario> ConsolidarPorDepartamento(ResultadosVM VM)
        {
            IEnumerable<RespostaUsuario> ListaConsolidada = new List<RespostaUsuario>();

            int IdDeptoAConsolidar = VM.IdDepartamento == null ? 0 : (int)VM.IdDepartamento;

            VM.Respostas = VM.Respostas.Where(x => x.Departamento.dpt_id == IdDeptoAConsolidar);

            if (VM.Respostas.Count() > 0)
                ListaConsolidada = Consolidar(VM);            

            return ListaConsolidada;

        }
        private static IEnumerable<RespostaUsuario> Consolidar(ResultadosVM VM)
        {

            int PontuacaoTotal = 0;
            int IdUsuarioAConsolidar = 0;

            if (VM.Respostas.Count() > 0)
                IdUsuarioAConsolidar = VM.Respostas.FirstOrDefault().rpu_usu_id;

            var ListaConsolidada = new List<RespostaUsuario>();

            int last = VM.Respostas.Count();
            int auxCount = 1;
            RespostaUsuario RespostaAnterior = new RespostaUsuario();

            foreach (var rs in VM.Respostas)
            {
                // Se troca usuário ou chegar no último registro, atribui dados consolidados.
                if ((rs.rpu_usu_id != IdUsuarioAConsolidar) || (last == auxCount))
                {
                    var rs_consolidada = new RespostaUsuario()
                    {
                        rpu_usu_id = RespostaAnterior.rpu_usu_id,
                        Usuario = RespostaAnterior.Usuario,
                        Departamento = RespostaAnterior.Departamento,
                        Empresa = RespostaAnterior.Empresa,
                        rpu_valor_resposta = string.Format("{0}#{1}", PontuacaoTotal, DefinirResultadoMindset(PontuacaoTotal)),
                        rpu_datacadastro = RespostaAnterior.rpu_datacadastro
                    };                    

                    ListaConsolidada.Add(rs_consolidada);
                    IdUsuarioAConsolidar = rs.rpu_usu_id;
                    PontuacaoTotal = 0;
                }

                PontuacaoTotal += CalcularPontosMindset(rs.Pergunta.prg_descricao, rs.Resposta.rsp_descricao);
                RespostaAnterior = rs;
                auxCount++;
            }


            var ListaFiltroData = new List<RespostaUsuario>();
            // Filtra por data, caso haja o parâmetro
            foreach(var item in ListaConsolidada)
            {
                if (VM.AnoPreenchimento != null)
                    if (item.rpu_datacadastro.Year != VM.AnoPreenchimento)
                        continue;
                // Filtra por data, caso haja o parâmetro
                if (VM.MesPreenchimento != null)
                    if (item.rpu_datacadastro.Month != VM.MesPreenchimento)
                        continue;

                ListaFiltroData.Add(item);
            }
            
            ListaConsolidada = ListaFiltroData;
                

            return ListaConsolidada;
        }
        private static string DefinirResultadoMindset(int pontos)
        {
            if (pontos <= 20)
                return "Forte mentalidade fixa";
            else if (pontos > 20 && pontos <= 35)
                return "Mentalidade fixa com algumas idéias de crescimento";
            else if (pontos > 35 && pontos <= 45)
                return "Mentalidade de crescimento com algumas idéias fixas";
            else if (pontos > 45 && pontos <= 60)
                return "Forte mentalidade de crescimento";
            else
                return "";
        }
        private static int CalcularPontosMindset(string Pergunta, string Resposta)
        {
            if (Pergunta.Equals("1") || Pergunta.Equals("4") || Pergunta.Equals("7") || Pergunta.Equals("8") || Pergunta.Equals("11")
                 || Pergunta.Equals("11") || Pergunta.Equals("12") || Pergunta.Equals("14") || Pergunta.Equals("16") || Pergunta.Equals("17")
                  || Pergunta.Equals("20"))
            {
                if (Resposta.Equals("ConcordoPlenamente"))
                    return 0;
                if (Resposta.Equals("Concordo"))
                    return 1;
                if (Resposta.Equals("Discordo"))
                    return 2;
                if (Resposta.Equals("DiscordoPlenamente"))
                    return 3;
            }

            if (Pergunta.Equals("2") || Pergunta.Equals("3") || Pergunta.Equals("5") || Pergunta.Equals("6") || Pergunta.Equals("9")
                 || Pergunta.Equals("10") || Pergunta.Equals("13") || Pergunta.Equals("15") || Pergunta.Equals("18") || Pergunta.Equals("19"))
            {
                if (Resposta.Equals("ConcordoPlenamente"))
                    return 3;
                if (Resposta.Equals("Concordo"))
                    return 2;
                if (Resposta.Equals("Discordo"))
                    return 1;
                if (Resposta.Equals("DiscordoPlenamente"))
                    return 0;
            }

            return 0;
        }
        #endregion        
    }
}