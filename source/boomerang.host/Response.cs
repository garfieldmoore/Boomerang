namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;

    /// <summary>
    /// The response defined for a service request
    /// </summary>
    public class Response
    {
        public Response()
        {
            ContentType = "text/html; charset=UTF-8";
            CacheControl = "private, max-age=0";
            Headers=new Dictionary<string, string>();
        }

        /// <summary>
        /// Number status code returned by the service (i.e. 200, 201, 301, 400 etc)
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// The body of the response
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Response content type. Defaults to text/html
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Cache control header of response. defaults to private, max-age=0
        /// </summary>
        public string CacheControl { get; set; }

        /// <summary>
        /// Headers expected in response
        /// </summary>
        public IDictionary<string, string> Headers { get; set; }
    }
}