using System;

namespace API.Seguros.Proseg.Domain.Util
{
    public class FormattingValidation
    {
        public static bool ValidarPlaca(string placa)
        {
            var isValid = true;

            placa = placa.Replace("-", "").Replace(" ", "").Replace("  ", "");

            // Verificar se é vazio.
            if (string.IsNullOrEmpty(placa))
                return false;

            //Verificar se tem 7 caracteres.
            if (placa.Length != 7)
                return false;

            char[] arrayPlaca = placa.ToCharArray();

            //Veirifacar se tem ao mínimo 3 letras e e 3 números.
            var countLetter = 0;
            var countNumber = 0;
            foreach (char letter in placa)
            {
                if (char.IsSymbol(Convert.ToChar(letter)))
                    return false;

                if (char.IsNumber(letter))
                    countNumber++;

                else if (char.IsLetter(letter))
                    countLetter++;
            }
            if (countLetter < 3 || countNumber < 3)
                return false;

            return isValid;
        }
    }
}
