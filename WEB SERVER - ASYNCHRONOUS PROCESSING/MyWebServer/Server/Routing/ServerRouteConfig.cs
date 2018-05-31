using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MyWebServer.Server.Enums;
using MyWebServer.Server.Routing.Contracts;

namespace MyWebServer.Server.Routing
{
    public class ServerRouteConfig : IServerRouteConfig
    {
        private readonly Dictionary<HttpRequestMethod, Dictionary<string, IRoutingContext>> routes;

        public ServerRouteConfig(IAppRouteConfig appRouteConfig)
        {
            this.routes = new Dictionary<HttpRequestMethod, Dictionary<string, IRoutingContext>>();

            var availableMethods = Enum
                .GetValues(typeof(HttpRequestMethod))
                .Cast<HttpRequestMethod>();

            foreach (var method in availableMethods)
            {
                this.routes[method] = new Dictionary<string, IRoutingContext>();
            }

            this.InitializeServerConfig(appRouteConfig);
        }
        
        public Dictionary<HttpRequestMethod, Dictionary<string, IRoutingContext>> Routes => routes;


        private void InitializeServerConfig(IAppRouteConfig appRouteConfig)
        {
            foreach (var registerdRoute in appRouteConfig.Routes)
            {
                var requestMethod = registerdRoute.Key;
                var routeHandlers = registerdRoute.Value;

                foreach (var routeWithHandler in routeHandlers)
                {
                    var route = routeWithHandler.Key;
                    var handler = routeWithHandler.Value;
                    var parameters = new List<string>();
                    var parsedRouteRegex = this.ParseRoute(route, parameters);

                    var routingContext = new RoutingContext(handler, parameters);

                    this.routes[requestMethod].Add(parsedRouteRegex, routingContext);
                }
            }
        }

        private string ParseRoute(string route, List<string> parameters)
        {
            var result = new StringBuilder();

            if (route == "/")
            {
                result.Append("^/$");
                return result.ToString();
            }

            result.Append("^/");
            var tokens = route.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);

            this.ParseTokens(tokens, parameters, result);

            return result.ToString();
        }

        private void ParseTokens(string[] tokens, List<string> parameters, StringBuilder result)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                var currentToken = tokens[i];
                var end = i == tokens.Length - 1 ? "$" : "/";
                if (!currentToken.StartsWith("{") && !currentToken.EndsWith("}"))
                {
                    result.Append($"{tokens[i]}{end}");
                    continue;
                }

                var parameterRegex = new Regex("(<\\w+>)");
                var parameterMatch = parameterRegex.Match(currentToken);

                if (!parameterMatch.Success)
                {
                    throw new InvalidOperationException($"Router parameter in '{currentToken}' is not valid.");
                }

                var match = parameterMatch.Value;
                var parameter = match.Substring(1, match.Length - 2);

                parameters.Add(parameter);

                var currentTokenWithoutCurlyBrackets = currentToken.Substring(1, currentToken.Length - 2);
                result.Append($"{currentTokenWithoutCurlyBrackets}{end}");

            }
        }
    }
}