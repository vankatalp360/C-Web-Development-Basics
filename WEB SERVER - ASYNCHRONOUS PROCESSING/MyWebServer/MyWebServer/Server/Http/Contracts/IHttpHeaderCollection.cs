namespace MyWebServer.Server.HTTP.Contracts
{
    public interface IHttpHeaderCollection
    {
        void Add(HttpHeader header);

        bool ContainKey(string key);

        HttpHeader GetHeader(string key);
    }
}