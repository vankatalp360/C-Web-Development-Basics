using System.Collections.Generic;
using MyWebServer.Server.Common;
using MyWebServer.Server.Handlers;
using MyWebServer.Server.Routing.Contracts;

namespace MyWebServer.Server.Routing
{
    public class RoutingContext : IRoutingContext
    {
        public RoutingContext(RequestHandler requestHandler, IEnumerable<string> parameters)
        {
            CoreValidator.ThrowIfNull(requestHandler, nameof(requestHandler));
            CoreValidator.ThrowIfNull(parameters, nameof(parameters));

            RequestHandler = requestHandler;
            Parameters = parameters;
        }

        public IEnumerable<string> Parameters { get; private set; }
        public RequestHandler RequestHandler { get; private set; }
    }
}