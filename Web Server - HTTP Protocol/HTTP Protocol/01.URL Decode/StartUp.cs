using System;
using System.Net;

namespace _01.URL_Decode
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputUrl = Console.ReadLine();
            var url = WebUtility.UrlDecode(inputUrl);

            var urlTokens = url.Split(new[] {"://"}, StringSplitOptions.None);

            var protocol = urlTokens[0];
            return;
        }
    }
}
