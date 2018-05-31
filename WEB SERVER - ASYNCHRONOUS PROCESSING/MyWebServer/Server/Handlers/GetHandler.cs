using System;
using MyWebServer.Server.HTTP.Contracts;

namespace MyWebServer.Server.Handlers
{
    public class GetHandler : RequestHandler
    {
        public GetHandler(Func<IHttpRequest, IHttpResponse> handlingFunc) 
            :base(handlingFunc)
        {
        }
    }
}