using MyWebServer.Server.HTTP.Contracts;

namespace MyWebServer.Server.Handlers.Contracts
{
    public interface IRequestHandler
    {
        IHttpResponse Handle(IHttpContext context);
    }
}