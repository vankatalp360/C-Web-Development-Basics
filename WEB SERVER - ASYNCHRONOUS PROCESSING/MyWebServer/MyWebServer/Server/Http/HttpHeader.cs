﻿using MyWebServer.Server.Common;

namespace MyWebServer.Server.HTTP
{
    public class HttpHeader
    {
        public HttpHeader(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(value));

            this.Key = key;
            this.Value = value;
        }

        public string Key { get; private set; }
        public string Value { get; private set; }


        public override string ToString()
        {
            return $"{this.Key}: {this.Value}";
        }
    }
}