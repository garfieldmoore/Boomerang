namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;

    /// <summary>
    /// The response defined for a service request
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Response()
        {
            ContentType = "text/html; charset=UTF-8";
            CacheControl = "private, max-age=0";
            Headers = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets the status code returned by the service (i.e. 200, 201, 301, 400 etc)
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the body of the response
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the default content type to use if no headers supplied. Defaults to text/html
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the default Cache control header of response to use if no headers supplied. defaults to private, max-age=0
        /// </summary>
        public string CacheControl { get; set; }

        /// <summary>
        /// Gets or sets the headers expected in response
        /// </summary>
        public IDictionary<string, string> Headers { get; set; }
    }
}