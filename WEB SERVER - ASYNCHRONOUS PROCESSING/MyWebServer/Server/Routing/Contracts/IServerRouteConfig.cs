using System.Collections.Generic;
using MyWebServer.Server.Enums;

namespace MyWebServer.Server.Routing.Contracts
{
    public interface IServerRouteConfig
    {
        Dictionary<HttpRequestMethod, Dictionary<string, IRoutingContext>> Routes { get; }

    }
}