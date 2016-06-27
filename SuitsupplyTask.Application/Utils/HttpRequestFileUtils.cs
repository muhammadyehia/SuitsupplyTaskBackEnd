using System.Web;
using SuitsupplyTask.Core.Entities;

namespace SuitsupplyTask.Application.Utils
{
    public  class HttpRequestFileUtils : IHttpRequestFileUtils
    {
        public   Photo GetProductPhoto(HttpRequest context=null)
        {
            if (context == null) return null;
            var hfc = context.Files;
            if (hfc.Count == 0) return null;
            var hpf = hfc[0];
            var productPhoto = new Photo
            {
                PhotoName = hpf.FileName,
                ContentType = hpf.ContentType
            };
            var myBinary = new byte[hpf.InputStream.Length];
            hpf.InputStream.Read(myBinary, 0, (int)hpf.InputStream.Length);
            productPhoto.Content = myBinary;
            return productPhoto;
        }
    }
}