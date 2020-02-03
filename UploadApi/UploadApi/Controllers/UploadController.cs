using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace UploadApi.Controllers
{
    public class UploadController : ApiController
    {
        //REMEMBER TO CONFIG WEB.CONFIGs MAX LENGTH TRANSFER FILE
        // IN httpRunTime tag, add: maxRequestLength="50000000"
        [HttpPost]
        [Route("api/UploadFile")]
        public async Task<string> UploadFile()
        {
            var rootFiles = HttpContext.Current.Server.MapPath("~/Files_uploaded");

            var provider = new MultipartFileStreamProvider(rootFiles);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                
                foreach(var file in provider.FileData)
                {
                    var name = file.Headers.ContentDisposition.FileName.Trim('"');
                    var localFileName = file.LocalFileName;
                    var filePath = Path.Combine(rootFiles, name);

                    var fileSize = file.Headers.ContentDisposition.Size;

                    File.Move(localFileName, filePath);

                }
            }
            catch(Exception ex)
            {
                return "Error: " + ex.Message; 
            }
         
            return "File Uploaded";
        }
    }
}
