using API.Seguros.Proseg.Domain.Constants;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace API.Seguros.Proseg.Domain.Util
{
    public class IOFile
    {
        public static void SalvarArquivo(Object ObjFile, string path)
        {
            string json = JsonConvert.SerializeObject(ObjFile, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public static DirectoryInfo VerificarPasta(string path)
        {
            // Caso n exista a pasta base, cria.
            string basePath = PathsConstant.JSONS_PATH;
            var di = new DirectoryInfo(basePath);
            if (!di.Exists)
                di.Create();
            // Caso n exista a pasta do cliente, cria.
            basePath = PathsConstant.JSONS_PATH_Seguros;
            di = new DirectoryInfo(basePath);
            if (!di.Exists)
                di.Create();

            di = new DirectoryInfo(path);
            if (!di.Exists)
                di.Create();
            return di;
        }

        public static void SalvarJsons(object objeto, string path, string nomeArquivo, string nrCalculo = null, string marca = null, string versao = null, string placa = null)
        {
            // Verifica se pasta existe, caso não exista, cria.
            VerificarPasta(path);

            // nr. randomico, para não repetir arquivo
            Random randomObj = new Random();
            int resultRandomInt = randomObj.Next(1, 100);  // creates a number between 1 and 12

            nomeArquivo = path + nomeArquivo + "_" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + resultRandomInt + ".json";

            SalvarArquivo(objeto, nomeArquivo);
        }
    }
}
