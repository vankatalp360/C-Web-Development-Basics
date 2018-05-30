using MyWebServer.Server.Contracts;

namespace MyWebServer.Application.Views.Home
{
    public class IndexView : IView
    {
        public string View() => "<h1>Welcome</h1>";
    }
}