using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Cripto
    {
        public static string GerarHash32(string senha)
        {
            // referências: https://imasters.com.br/dotnet/criptografia-na-plataforma-net
            //cria instância da classe MD5CryptoServiceProvider
            MD5CryptoServiceProvider MD5provider = new MD5CryptoServiceProvider();
            //gera o hash do texto
            byte[] valorHash = MD5provider.ComputeHash(Encoding.Default.GetBytes(senha));
            StringBuilder str = new StringBuilder();
            //retorna o hash
            for (int contador = 0; contador < valorHash.Length; contador++)
            {
                str.Append(valorHash[contador].ToString("x2"));
            }
            int qtdeCaract = str.ToString().Length;
            return str.ToString();
        }

        public static bool VerificaHash32(string texto, string valorHashArmazenado)
        {
            //gera o hash para o texto
            string valorHash2 = GerarHash32(texto);
            // Cria um StringComparer e compara o hash gerado com o armazenado
            StringComparer strcomparer = StringComparer.OrdinalIgnoreCase;
            //se o valor dos hash forem iguais então retorna true
            if (strcomparer.Compare(valorHash2, valorHashArmazenado).Equals(0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
