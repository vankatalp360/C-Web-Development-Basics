using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.Request_Parser
{
    class StartUp
    {
        private const string OutputMassege = "{0} {1} {2}\r\nContent-Lenght: {3}\r\nContent-Type: text/plain\r\n\r\n{4}";
        static void Main(string[] args)
        {
            var codesMessages = new Dictionary<int, string>()
            {
                {200, "OK"},
                {404, "Not Found"}
            };
            var pathMetods = new Dictionary<string, HashSet<string>>();

            string input = "";

            while ((input = Console.ReadLine()) != "END")
            {
                var tokens = input.Split(new Char[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
                var path = tokens[0];
                var method = tokens[1];
                if (!pathMetods.ContainsKey(path))
                {
                    pathMetods[path] = new HashSet<string>();
                }
                pathMetods[path].Add(method.ToUpper());
            }

            var httpRequest = Console.ReadLine().Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
            var requestMethod = httpRequest[0].ToUpper();
            var requestPath = httpRequest[1].Substring(1);
            var requestHost = httpRequest[2];
            var statusCode = 0;

            if (pathMetods.ContainsKey(requestPath))
            {
                statusCode = pathMetods[requestPath].Contains(requestMethod) ? 200 : 404;
            }
            else
            {
                statusCode = 404;
            }

            var statusText = codesMessages[statusCode];
            var output = string.Format(OutputMassege, requestHost, statusCode, statusText, statusText.Length, statusText);
            Console.WriteLine(output);
        }
    }
}
