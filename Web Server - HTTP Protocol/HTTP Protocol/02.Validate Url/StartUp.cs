using System;
using System.Net;
using System.Text.RegularExpressions;

namespace _02.Validate_Url
{
    class Program
    {
        private const string FaildMessage = "Invalid URL";
        static void Main(string[] args)
        {
            string SuccessMessage = "Protocol: {0}\r\nHost: {1}\r\nPort: {2}\r\nPath: {3}";
            var encodeUrl = Console.ReadLine();
            var decodeUrl = WebUtility.UrlDecode(encodeUrl);

            var urlRegexText =
                @"^(https{0,1}):\/\/([a-zA-Z0-9-.]+)(?::(\d+))?\/*([a-zA-Z0-9\/.]+)*(?:\?)*([a-zA-Z0-9\.\=\+\-&_]*)(#[a-zA-Z0-9]*)*";
            var urlRegex = new Regex(urlRegexText);
            var matchUrl = urlRegex.Match(decodeUrl);
            var input = "";

            if (matchUrl.Success)
            {
                input = ProcessUrl(matchUrl, SuccessMessage);
            }
            else
            {
                Faild();
            }

            Console.WriteLine(input);
        }

        private static void Faild()
        {
            Console.WriteLine(FaildMessage);
            Environment.Exit(0);
        }

        private static string ProcessUrl(Match matchUrl, string successMessage)
        {
            var matches = matchUrl.Groups;
            var protocol = matches[1].ToString();
            var host = matches[2].ToString();
            var port = matches[3].ToString();
            var path = "/" + matches[4].ToString();
            var query = matches[5].ToString();
            var fragment = matches[6].ToString();
            fragment = fragment == "" ? fragment : fragment.Substring(1);

            var expectedPort = protocol == "http" ? "80" : "443";

            if (port != "" && port != expectedPort)
            {
                Faild();
            }
            else
            {
                port = expectedPort;
            }


            if (query != "")
            {
                successMessage += "\r\nQuery: {4}";
            }

            if (fragment != "")
            {
                successMessage += "\r\nFragment: {5}";
            }

            var input = string.Format(successMessage, protocol, host, port, path, query, fragment);
            return input;
        }
    }
}
