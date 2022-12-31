using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace Administracion_de_Taller.clases
{
    internal class CloudinaryImpl
    {
        public Cloudinary cloudinary;
        public const string CLOUD_NAME = "dicxo5bfw";
        public const string API_KEY = "821933176972937";
        public const string API_SECRET = "Y9rqijzlr-dSum3_j0pOomYT7OA";

        public ImageUploadResult cloudinarySave(String path)
        {
            Account account= new Account(CLOUD_NAME, API_KEY, API_SECRET);
            cloudinary = new Cloudinary(account);
            return subirImagen(path);
        }

        public ImageUploadResult subirImagen(String path)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(path),
            };
            return cloudinary.Upload(uploadParams);
        }

    }
}
