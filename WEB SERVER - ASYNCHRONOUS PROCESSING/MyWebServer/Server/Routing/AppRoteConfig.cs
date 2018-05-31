using System;
using System.Collections.Generic;
using System.Linq;
using MyWebServer.Server.Enums;
using MyWebServer.Server.Handlers;
using MyWebServer.Server.HTTP.Contracts;
using MyWebServer.Server.Routing.Contracts;

namespace MyWebServer.Server.Routing
{
    public class AppRoteConfig : IAppRouteConfig
    {
        private readonly Dictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> routes;

        public IReadOnlyDictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> Routes => this.routes;

        public AppRoteConfig()
        {
            this.routes =
                new Dictionary<HttpRequestMethod, IDictionary<string, RequestHandler>>();

            var availabelMethods = Enum
                .GetValues(typeof(HttpRequestMethod))
                .Cast<HttpRequestMethod>();

            foreach (var method in availabelMethods)
            {
                this.routes[method] = new Dictionary<string, RequestHandler>();

            }
        }

        public void Get(string route, Func<IHttpRequest, IHttpResponse> handler)
        {
            this.AddRoute(route, new GetHandler(handler));
        }
        public void Post(string route, Func<IHttpRequest, IHttpResponse> handler)
        {
            this.AddRoute(route, new PostHandler(handler));
        }
        public void AddRoute(string route, RequestHandler httpHandler)
        {
            var handlerName = httpHandler.GetType().Name.ToLower();

            if (handlerName.Contains("get"))
            {
                this.routes[HttpRequestMethod.Get].Add(route, httpHandler);
            }
            else if (handlerName.Contains("post"))
            {
                this.routes[HttpRequestMethod.Post].Add(route, httpHandler);
            }
            else
            {
                throw new InvalidOperationException("Invalid handler!");
            }
        }
    }
}