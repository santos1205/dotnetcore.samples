
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace Common
{
    // Refs: https://docs.microsoft.com/pt-br/dotnet/csharp/programming-guide/file-system/how-to-write-to-a-text-file
    // Refs: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-get-information-about-files-folders-and-drives        
    public class IOFile
    {
        public static void GravarArquivoBoleto(string nomeArquivo, List<string> Linhas)
        {
            // TODO: OS ARQUIVOS A SEREM EXPORTADOS, SERÃO GRAVADOS TEMPORARIAMENTE EM TEMPFILES.


            //string[] lines = { "First linha", "Second linha", "Third linha" };
            string[] lines = Linhas.ToArray();


            string str_local_path = "../TempFiles";
            str_local_path = HttpContext.Current.Server.MapPath(str_local_path);

            File.WriteAllLines(str_local_path + "\\" + nomeArquivo + ".csv", lines, Encoding.Unicode);

            FileGarbage(30);
        }

        // os arquivos q não tiver o dia vigente serão deletados, para não ocupar memória do servidor
        private static void FileGarbage(int dia)
        {
            string str_local_path = "../TempFiles";
            str_local_path = HttpContext.Current.Server.MapPath(str_local_path);
            string[] files = Directory.GetFiles(str_local_path, "*.csv");

            foreach (string name in files)
            {
                string currentDay = DateTime.Now.Day.ToString("00");
                string currentMonth = DateTime.Now.Month.ToString("00");                
                if (!name.Contains(string.Format("id_{0}{1}", currentDay, currentMonth)))                
                    File.Delete(name);                
            }

        }
        public List<string> ReadFile(Stream Arquivo)
        {
            var linhasArquivo = new List<string>();
            using (StreamReader inputStreamReader = new StreamReader(Arquivo))
            {
                while (!inputStreamReader.EndOfStream)
                {
                    linhasArquivo.Add(inputStreamReader.ReadLine());
                }
            }
            return linhasArquivo;
        }       
    }
}
