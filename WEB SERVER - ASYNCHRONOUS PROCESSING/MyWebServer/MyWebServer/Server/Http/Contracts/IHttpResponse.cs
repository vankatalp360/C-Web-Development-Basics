using MyWebServer.Server.Enums;

namespace MyWebServer.Server.HTTP.Contracts
{
    public interface IHttpResponse
    {
        HttpStatusCode StatusCode { get; }
        HttpHeaderCollection Headers { get; }
    }
}