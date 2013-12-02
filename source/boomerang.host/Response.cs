namespace Rainbow.Testing.Boomerang.Host
{
    /// <summary>
    /// The response defined for a service request
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Number status code returned by the service (i.e. 200, 201, 301, 400 etc)
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// The body of the response
        /// </summary>
        public string Body { get; set; }
    }
}