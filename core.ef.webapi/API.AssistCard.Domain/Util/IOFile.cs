using API.Viagem.Domain.Constants;
using Newtonsoft.Json;
using System;
using System.IO;

namespace API.Viagem.Domain.Util
{
    public class IOFile
    {
        public static void SalvarArquivo(object ObjFile, string path)
        {
            string json = JsonConvert.SerializeObject(ObjFile, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public static DirectoryInfo VerificarPasta(string path)
        {
            // Caso n exista a pasta base, cria.
            string basePath = PathsConstant.JSONS_PATH;
            DirectoryInfo di = new DirectoryInfo(basePath);
            if (!di.Exists)
            {
                di.Create();
            }
            // Caso n exista a pasta do cliente, cria.
            basePath = PathsConstant.JSONS_PATH_AssistCard;
            di = new DirectoryInfo(basePath);
            if (!di.Exists)
            {
                di.Create();
            }

            di = new DirectoryInfo(path);
            if (!di.Exists)
            {
                di.Create();
            }

            return di;
        }

        public static void SalvarJsons(object objeto, string path, string nomeArquivo, int? idArquivo = null)
        {
            // Verifica se pasta existe, caso não exista, cria.
            VerificarPasta(path);
            string data = DateTime.Now.ToString("dd/MM/yyyy").Replace("/","");

            if (idArquivo != null)
            {
                nomeArquivo = path + nomeArquivo + "_" + idArquivo + "_" + data + "_" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + ".json";
            }
            else
            {
                nomeArquivo = path + nomeArquivo + "_" + data + "_" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + ".json";
            }

            IOFile.SalvarArquivo(objeto, nomeArquivo);
        }
    }
}
