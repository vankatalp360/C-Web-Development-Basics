﻿using MyWebServer.Application.Views.Home;
using MyWebServer.Server;
using MyWebServer.Server.Enums;
using MyWebServer.Server.HTTP.Contracts;
using MyWebServer.Server.HTTP.Response;

namespace MyWebServer.Application.Controllers
{
    public class UserController
    {
        public IHttpResponse RegisterGet()
        {
            return new ViewResponse(HttpStatusCode.Ok, new RegisterView());
        }

        public IHttpResponse RegisterPost(string name)
        {
            return new RedirectResponse($"/user/{name}");
        }

        public IHttpResponse Details(string name)
        {
            Model model = new Model {["name"] = name};
            return new ViewResponse(HttpStatusCode.Ok, new UserDetailsView(model));
        }
    }
}