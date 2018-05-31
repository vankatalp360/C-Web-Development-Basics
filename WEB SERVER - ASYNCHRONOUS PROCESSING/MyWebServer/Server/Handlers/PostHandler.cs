using System;
using MyWebServer.Server.HTTP.Contracts;

namespace MyWebServer.Server.Handlers
{
    public class PostHandler : RequestHandler
    {
        public PostHandler(Func<IHttpRequest, IHttpResponse> handlingFunc) 
            :base(handlingFunc)
        {
        }
    }
}