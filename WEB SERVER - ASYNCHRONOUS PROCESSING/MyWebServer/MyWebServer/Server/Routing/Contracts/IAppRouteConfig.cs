using System;
using System.Collections.Generic;
using MyWebServer.Server.Enums;
using MyWebServer.Server.Handlers;
using MyWebServer.Server.HTTP.Contracts;

namespace MyWebServer.Server.Routing.Contracts
{
    public interface IAppRouteConfig
    {
        IReadOnlyDictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> Routes { get; }

        void Get(string route, Func<IHttpRequest, IHttpResponse> handler);
        void Post(string route, Func<IHttpRequest, IHttpResponse> handler);

        void AddRoute(string route, RequestHandler httpHandler);
    }
}