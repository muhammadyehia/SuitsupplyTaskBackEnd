using System.Web;
using SuitsupplyTask.Core.Entities;

namespace SuitsupplyTask.Application.Utils
{
    public interface IHttpRequestFileUtils
    {
        Photo GetProductPhoto(HttpRequest context=null);
    }
}