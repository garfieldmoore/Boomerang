﻿namespace Rainbow.Testing.Boomerang.Host
{
    public class Response
    {
        public int StatusCode { get; set; }

        public string Body { get; set; }

        public static Response CreateNew()
        {
            return new Response();
        }
    }
}