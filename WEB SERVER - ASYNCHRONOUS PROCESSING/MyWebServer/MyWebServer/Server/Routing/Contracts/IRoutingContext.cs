using System.Collections.Generic;
using MyWebServer.Server.Handlers;

namespace MyWebServer.Server.Routing.Contracts
{
    public interface IRoutingContext
    {
        IEnumerable<string> Parameters { get; }
        RequestHandler RequestHandler { get; }

    }
}