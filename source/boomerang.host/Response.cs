namespace Rainbow.Testing.Boomerang.Host
{
    /// <summary>
    /// The response defined for a service request
    /// </summary>
    public class Response
    {
        public Response()
        {
            ContentType = "text/html; charset=UTF-8";
            CacheControl = "private, max-age=0";
        }

        /// <summary>
        /// Number status code returned by the service (i.e. 200, 201, 301, 400 etc)
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// The body of the response
        /// </summary>
        public string Body { get; set; }

        public string ContentType { get; set; }

        public string CacheControl { get; set; }
    }
}