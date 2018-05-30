using MyWebServer.Application.Controllers;
using MyWebServer.Server.Contracts;
using MyWebServer.Server.Handlers;
using MyWebServer.Server.Routing.Contracts;

namespace MyWebServer.Application
{
    public class MainApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .AddRoute("/", new GetHandler(request => new HomeController().Index()));

            appRouteConfig.AddRoute(
                "/register",
                new PostHandler(
                    httpContext => new UserController()
                    .RegisterPost(httpContext.ForumData["name"])));
            appRouteConfig.AddRoute(
                "/register",
                new GetHandler(httpContext => new UserController().RegisterGet()));
            appRouteConfig.AddRoute(
                "/user/{(?<name>[a-z]+)}",
                new GetHandler(httpContext => new UserController()
                .Details(httpContext.UrlParameters["name"])));
        }
    }
}