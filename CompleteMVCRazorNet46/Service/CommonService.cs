using QuestionarioCOrg.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace QuestionarioCOrg.Service
{    
    public class CommonService
    {
        // refs: http://www.macoratti.net/11/09/c_val1.htm
        public static bool CpfValido(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
        // https://social.msdn.microsoft.com/Forums/pt-BR/1e07a7b3-924b-43b3-8e6b-f6be49862e17/validar-email-via-cdigo-c?forum=aspnetpt
        public static bool ValidarEmail(string Email)
        {
            if (string.IsNullOrEmpty(Email))
                return true;
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (rg.IsMatch(Email))
                return true;
            else
                return false;
        }
        public static string PrimeiraLetraMaiuscula(string texto)
        {
            texto = texto.ToLower();
            var ArrTexto = texto.Split(' ');
            string resultado = string.Empty;
            foreach(var txt in ArrTexto)
            {
                if (txt.Length > 2)
                {
                    if (!txt.Equals("dos") && !txt.Equals("das"))
                        resultado += " " + char.ToUpper(txt[0]) + txt.Substring(1);
                }
                else
                    resultado += " " + txt;
            }

            return resultado;
        }
        // refs: http://www.macoratti.net/11/09/c_val1.htm
        public static bool CnpjValido(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
        public static SelectList GerarComboEstado(string Estado)
        {
            // Carrega as empresas para filtro
            var OptEstadoList = new List<SelectListItem>();

            OptEstadoList.Add(new SelectListItem() { Text = "Acre", Value = "AC" });
            OptEstadoList.Add(new SelectListItem() { Text = "Alagoas", Value = "AL" });
            OptEstadoList.Add(new SelectListItem() { Text = "Amapá", Value = "AP" });
            OptEstadoList.Add(new SelectListItem() { Text = "Amazonas", Value = "AM" });
            OptEstadoList.Add(new SelectListItem() { Text = "Bahia", Value = "BA" });
            OptEstadoList.Add(new SelectListItem() { Text = "Ceará", Value = "CE" });
            OptEstadoList.Add(new SelectListItem() { Text = "Distrito Federal", Value = "DF" });
            OptEstadoList.Add(new SelectListItem() { Text = "Goiás", Value = "GO" });
            OptEstadoList.Add(new SelectListItem() { Text = "Maranhão", Value = "MA" });
            OptEstadoList.Add(new SelectListItem() { Text = "Mato Grosso", Value = "MT" });
            OptEstadoList.Add(new SelectListItem() { Text = "Mato Grosso do Sul", Value = "MS" });
            OptEstadoList.Add(new SelectListItem() { Text = "Minas Gerais", Value = "MG" });
            OptEstadoList.Add(new SelectListItem() { Text = "Pará", Value = "PA" });
            OptEstadoList.Add(new SelectListItem() { Text = "Paraíba", Value = "PB" });
            OptEstadoList.Add(new SelectListItem() { Text = "Paraná", Value = "PR" });
            OptEstadoList.Add(new SelectListItem() { Text = "Pernambuco", Value = "PE" });
            OptEstadoList.Add(new SelectListItem() { Text = "Paiuí", Value = "PI" });
            OptEstadoList.Add(new SelectListItem() { Text = "Rio de Janeiro", Value = "RJ" });
            OptEstadoList.Add(new SelectListItem() { Text = "Rio Grande do Norte", Value = "RN" });
            OptEstadoList.Add(new SelectListItem() { Text = "Rio Grande do Sul", Value = "RS" });
            OptEstadoList.Add(new SelectListItem() { Text = "Rondônia", Value = "RO" });
            OptEstadoList.Add(new SelectListItem() { Text = "Roraima", Value = "RR" });
            OptEstadoList.Add(new SelectListItem() { Text = "Santa Catarina", Value = "SC" });
            OptEstadoList.Add(new SelectListItem() { Text = "São Paulo", Value = "SP" });
            OptEstadoList.Add(new SelectListItem() { Text = "Sergipe", Value = "SE" });
            OptEstadoList.Add(new SelectListItem() { Text = "Tocantins", Value = "TO" });

            if (!string.IsNullOrEmpty(Estado))
                return new SelectList(OptEstadoList, "Value", "Text", Estado);
            else
                return new SelectList(OptEstadoList, "Value", "Text");
        }
        public static SelectList GerarComboRamo(string Ramo)
        {
            // Carrega as empresas para filtro
            var OptRamoList = new List<SelectListItem>();

            OptRamoList.Add(new SelectListItem() { Text = "Alimentação", Value = "Alimentação" });
            OptRamoList.Add(new SelectListItem() { Text = "Educação", Value = "Educação" });
            OptRamoList.Add(new SelectListItem() { Text = "Financeiro", Value = "Financeiro" });
            OptRamoList.Add(new SelectListItem() { Text = "Marketing", Value = "Marketing" });
            OptRamoList.Add(new SelectListItem() { Text = "Seguros", Value = "Seguros" });
            OptRamoList.Add(new SelectListItem() { Text = "Saude", Value = "Saude" });
            OptRamoList.Add(new SelectListItem() { Text = "Tecnologia", Value = "Tecnologia" });
            OptRamoList.Add(new SelectListItem() { Text = "Turismo", Value = "Turismo" });
            OptRamoList.Add(new SelectListItem() { Text = "Outros", Value = "Outros" });

            if (!string.IsNullOrEmpty(Ramo))
                return new SelectList(OptRamoList, "Value", "Text", Ramo);
            else
                return new SelectList(OptRamoList, "Value", "Text");
        }
        public static IEnumerable<Questionario> CarregarQuestionariosPorEmpresa(int IdEmpresa = 0)
        {
            if (IdEmpresa == 0)
                return new List<Questionario>();

            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();
            var EQs = db.EmpresaQuestionario.Where(x => x.eqt_emp_id == IdEmpresa
                        && x.eqt_ativo);
            EQs = EQs.Where(x => x.Questionario.qst_ativo);

            // Filtrando apenas os objs Questionario
            var LsQ = new List<Questionario>();
            foreach (var item in EQs)
                LsQ.Add(item.Questionario);

            return LsQ;
        }
        public static IEnumerable<Questionario> CarregarQuestionarios()
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            return db.Questionario.Where(x => x.qst_ativo && x.qst_id != 0).OrderBy(x => x.qst_nome);
        }
        public static IEnumerable<Questionario> CarregarQuestionariosPublicados()
        {
            QuestionarioOrgDBEntities db = new QuestionarioOrgDBEntities();

            return db.Questionario.Where(x => x.qst_ativo && x.qst_publicado).OrderBy(x => x.qst_nome);
        }

        public static string FormatarCasasDecimais(decimal Numero, int CasasDecimais = 2)
        {
            var arrNumeros = Numero.ToString().Split(',');
            string NrFormatado = string.Empty;
            if (arrNumeros.Length > 1)
            {
                arrNumeros[1] = arrNumeros[1].Substring(0, 2);
                NrFormatado = string.Format("{0},{1}", arrNumeros[0], arrNumeros[1]);
            }
                
            else
                NrFormatado = Numero.ToString();

            return NrFormatado;
        }
    }
}