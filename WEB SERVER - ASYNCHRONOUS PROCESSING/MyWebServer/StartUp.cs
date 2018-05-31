using System;
using MyWebServer.Application;
using MyWebServer.Server;
using MyWebServer.Server.Contracts;
using MyWebServer.Server.Routing;

namespace MyWebServer
{
    public class StartUp : IRunnable
    {
        static void Main()
        {
            new StartUp().Run();
        }

        public void Run()
        {
            var mainApplication = new MainApplication();
            var appRouteConfig = new AppRoteConfig();
            mainApplication.Configure(appRouteConfig);

            var webServer = new WebServer(1337, appRouteConfig);

            webServer.Run();
        }
    }
}
