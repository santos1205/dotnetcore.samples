using API.Seguros.Proseg.Domain.DTOs;
using API.Seguros.Proseg.Domain.Entidades;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Seguros.Proseg.Domain.Util
{
    public class JsonSerializer
    {
        public static string ConverteObjectParaJSon<T>(T obj)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(ms, obj);
                string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return jsonString;
            }
            catch
            {
                throw;
            }
        }

        public static T ConverteJSonParaObject<T>(string jsonString)
        {
            try
            {
                if (jsonString == null)
                {
                    ValoresPadraoApi valorPadrao = new ValoresPadraoApi();
                    var novoJson = ConverteObjectParaJSon(valorPadrao);

                    jsonString = novoJson;
                }

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                T obj = (T)serializer.ReadObject(ms);
                return obj;
            }
            catch
            {
                throw;
            }
        }


    }
}
