namespace Rainbow.Testing.Boomerang.Host.Configuration
{
    /// <summary>
    /// Extension methods to help configuration
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Uses the single response per request handler.
        /// <remarks>
        /// By default Boomerang allows registeration of multiple responses for requests and returns a 404 when no more responses are available.
        /// Using this changes the behaviour to always returning one request.  if more than one request is configured, the last one will be used.
        /// </remarks>
        /// </summary>
        public static void UseSingleResponsePerRequestHandler(this IHostConfiguration target)
        {
            target.UseRequestHandlerFactory(()=>new SingleResponseRepository());
        }
    }
}