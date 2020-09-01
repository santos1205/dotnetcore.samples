using System;
using System.Text.RegularExpressions;

namespace API.Viagem.Domain.Util
{
    public class FormattingValidation
    {
        public static string FormatarCPF(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");
            return cpf;
        }

        public static string FormatarCaracteresEspecias(string strIn)
        {
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }

        public static bool ValidarPlaca(string placa)
        {
            bool isValid = true;

            placa = placa.Replace("-", "").Replace(" ", "").Replace("  ", "");

            // Verificar se é vazio.
            if (string.IsNullOrEmpty(placa))
            {
                return false;
            }

            //Verificar se tem 7 caracteres.
            if (placa.Length != 7)
            {
                return false;
            }

            char[] arrayPlaca = placa.ToCharArray();

            //Veirifacar se tem ao mínimo 3 letras e e 3 números.
            int countLetter = 0;
            int countNumber = 0;
            foreach (char letter in placa)
            {
                if (char.IsSymbol(Convert.ToChar(letter)))
                {
                    return false;
                }

                if (char.IsNumber(letter))
                {
                    countNumber++;
                }
                else if (char.IsLetter(letter))
                {
                    countLetter++;
                }
            }
            if (countLetter < 3 || countNumber < 3)
            {
                return false;
            }

            return isValid;
        }
    }
}
