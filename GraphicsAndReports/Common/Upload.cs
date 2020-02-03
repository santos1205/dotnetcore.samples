using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class Upload
    {
        public static void SaveFileToDisk(HttpPostedFile file)
        {
            var folder = GetUploadFolder();
            var targetPath = Path.Combine(folder.FullName, file.FileName);
            file.SaveAs(targetPath);
        }

        public static void SaveImageToDisk(HttpPostedFile file, string newName)
        {
            var folder = GetUploadFolder();
            if (file.ContentType == "image/png")
                newName += ".png";
            if (file.ContentType == "image/jpeg")
                newName += ".jpg";

            var targetPath = Path.Combine(folder.FullName, newName);
            file.SaveAs(targetPath);
        }

        public static DirectoryInfo GetUploadFolder()
        {
            string str_local_path = "~/uploaded files/logos";
            str_local_path = HttpContext.Current.Server.MapPath(str_local_path);


            var di = new DirectoryInfo(str_local_path);

            if (!di.Exists)
                di.Create();

            return di;
        }
    }
}
