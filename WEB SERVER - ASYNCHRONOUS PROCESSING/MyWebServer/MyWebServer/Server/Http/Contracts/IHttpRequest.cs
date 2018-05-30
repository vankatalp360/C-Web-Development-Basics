using System.Collections.Generic;
using MyWebServer.Server.Enums;

namespace MyWebServer.Server.HTTP.Contracts
{
    public interface IHttpRequest
    {
        Dictionary<string, string> ForumData { get; }

        HttpHeaderCollection Headers { get; }

        string Path { get; }

        Dictionary<string, string> QueryParameters { get; }

        HttpRequestMethod RequestMethod { get; }

        string Url { get; }

        Dictionary<string, string> UrlParameters { get; }

        void AddUrlParameter(string key, string value);
    }
}