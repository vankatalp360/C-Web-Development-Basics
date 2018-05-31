using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MyWebServer.Server.Common;
using MyWebServer.Server.Enums;
using MyWebServer.Server.Exceptions;
using MyWebServer.Server.HTTP.Contracts;

namespace MyWebServer.Server.HTTP
{
    public class HttpRequest : IHttpRequest
    {
        private string requestString;
       
        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

            this.requestString = requestString;
            this.ForumData = new Dictionary<string, string>();
            this.Headers = new HttpHeaderCollection();
            this.QueryParameters = new Dictionary<string, string>();
            this.UrlParameters = new Dictionary<string, string>();

            this.ParseRequest(requestString);
        }


        public Dictionary<string, string> ForumData { get; private set; }
        public HttpHeaderCollection Headers { get; private set; }
        public string Path { get; private set; }
        public Dictionary<string, string> QueryParameters { get; private set; }
        public HttpRequestMethod RequestMethod { get; private set; }
        public string Url { get; private set; }
        public Dictionary<string, string> UrlParameters { get; private set; }

        public void AddUrlParameter  (string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            this.UrlParameters[key] = value;
        }

        private void ParseRequest(string requestString)
        {
            var requestLines = requestString.Split(new []{Environment.NewLine}, StringSplitOptions.None);

            if (!requestLines.Any())
            {
                BadRequestException.ThrowFromInvalidRequest();
            }

            var requestLine = requestLines[0].Split(new[] { ' ' }, 
                StringSplitOptions.RemoveEmptyEntries);

            if (requestLine.Length != 3 || requestLine[2].ToLower() != "http/1.1")
            {
                BadRequestException.ThrowFromInvalidRequest();
            }

            this.RequestMethod = this.ParseMethod(requestLine[0]);
            this.Url = requestLine[1];
            this.Path = this.Url
                .Split(new[] {'?', '#'}, StringSplitOptions.RemoveEmptyEntries)[0];

            this.ParseHeaders(requestLines);
            this.ParseParameters();
            var requestLinesCount = requestLines.Length;

            var forumDataLine = requestLines.Where(l => l != "").ToArray().Last();
            this.ParseForumData(forumDataLine);
        }

        

        private HttpRequestMethod ParseMethod(string method)
        {
            HttpRequestMethod parsedMethod;
            var parseTry = Enum.TryParse(method, true, out parsedMethod);
            if (!parseTry)
            {
                BadRequestException.ThrowFromInvalidRequest();
            }
            return parsedMethod;
        }
        private void ParseParameters()
        {
            if (!this.Url.Contains("?"))
            {
                return;
            }

            var query = this.Url
                .Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries)
                .Last();

            this.ParseQuery(query, this.UrlParameters);
        }

        private void ParseQuery(string query, IDictionary<string, string> dict)
        {
            if (!query.Contains('='))
            {
                return;
            }

            var queryPairs = query.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var queryPair in queryPairs)
            {
                var queryKvp = queryPair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                if (queryKvp.Length != 2)
                {
                    return;
                }

                var queryKey = WebUtility.UrlDecode(queryKvp[0]);
                var queryValue = WebUtility.UrlDecode(queryKvp[1]);

                dict.Add(queryKey, queryValue);
            }
        }

        private void ParseHeaders(string[] requestLines)
        {
            var emptyLineAfterHeaderIndex = Array.IndexOf(requestLines, string.Empty);

            for (int i = 1; i < emptyLineAfterHeaderIndex; i++)
            {
                var currentLine = requestLines[i];
                var headerParts = currentLine.Split(new [] {": "},StringSplitOptions.RemoveEmptyEntries);

                if (headerParts.Length != 2)
                {
                    BadRequestException.ThrowFromInvalidRequest();
                }

                var headerKey = headerParts[0];
                var headerValue = headerParts[1].Trim();

                var header = new HttpHeader(headerKey, headerValue);

                this.Headers.Add(header);
            }

            if (!this.Headers.ContainKey("Host"))
            {
                BadRequestException.ThrowFromInvalidRequest();
            }
        }
        private void ParseForumData(string formDataLine)
        {
            if (this.RequestMethod == HttpRequestMethod.Get)
            {
                return;
            }

            this.ParseQuery(formDataLine, this.ForumData);
        }

        public override string ToString()
        {
            return this.requestString;
        }
    }
}