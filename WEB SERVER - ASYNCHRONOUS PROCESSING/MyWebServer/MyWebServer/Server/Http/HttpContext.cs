using MyWebServer.Server.Common;
using MyWebServer.Server.HTTP.Contracts;

namespace MyWebServer.Server.HTTP
{
    public class HttpContext : IHttpContext
    {
        public HttpContext(IHttpRequest request)
        {
            CoreValidator.ThrowIfNull(request, nameof(request));

            this.Request = request;
        }

        public IHttpRequest Request { get; }
    }
}