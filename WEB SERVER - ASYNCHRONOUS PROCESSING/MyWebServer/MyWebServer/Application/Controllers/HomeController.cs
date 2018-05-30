using MyWebServer.Application.Views.Home;
using MyWebServer.Server.Enums;
using MyWebServer.Server.HTTP;
using MyWebServer.Server.HTTP.Response;

namespace MyWebServer.Application.Controllers
{
    public class HomeController
    {
        public HttpResponse Index()
        {
            return new ViewResponse(HttpStatusCode.Ok, new IndexView());
        }
    }
}