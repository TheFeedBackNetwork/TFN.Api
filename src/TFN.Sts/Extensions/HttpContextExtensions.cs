using Microsoft.AspNetCore.Http;

namespace TFN.Sts.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetAppUrl(this HttpContext context)
        {
            var http = "http://";
            if (context.Request.IsHttps)
            {
                http = "https://";
            }
            //return $"{http}localhost:5001";
            return $"{http}{context.Request.Host.Value}";
        }
    }
}