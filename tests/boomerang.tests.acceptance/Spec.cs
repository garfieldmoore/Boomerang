namespace boomerang.tests.acceptance
{
    using System.Collections.Generic;
    using System.Linq;

    using Rainbow.Testing.Boomerang.Host;

    using RestSharp;

    internal class Spec
    {
        #region Static Fields

        public static readonly string HostAddress = "http://localhost:5100/";

        public static IDictionary<string, string> ResponseHeaders;

        private static IBoomerang defaultServer;

        #endregion

        #region Public Properties

        public static IList<Request> ReceivedRequests
        {
            get
            {
                return defaultServer.GetAllReceivedRequests().ToList();
            }
        }

        public static string ResponseText { get; set; }

        public static string StatusCode { get; set; }

        #endregion

        #region Public Methods and Operators

        public static void WhenDeleteSentTo(string webAddress)
        {
            var request = new RestRequest(webAddress, Method.DELETE);
            var client = new RestClient();
            IRestResponse response = client.Execute(request);

            StatusCode = response.StatusCode.ToString();
            ResponseText = response.Content;
        }

        public static void WhenGetRequestSent(string webHostAddress)
        {
            ResponseHeaders = new Dictionary<string, string>();
            var request = new RestRequest(webHostAddress, Method.GET);
            var client = new RestClient();

            IRestResponse response = client.Execute(request);

            StatusCode = response.StatusCode.ToString();
            ResponseText = response.Content;

            foreach (Parameter parameter in response.Headers)
            {
                ResponseHeaders.Add(parameter.Name, parameter.Value.ToString());
            }
        }

        public static void WhenPostsSentTo(string webHostAddress, string data)
        {
            var request = new RestRequest(webHostAddress, Method.POST);
            request.AddBody(data);
            var client = new RestClient();
            IRestResponse response = client.Execute(request);

            StatusCode = response.StatusCode.ToString();
            ResponseText = response.Content;
        }

        public static void WhenPutSentTo(string webHostAddress, string data)
        {
            var request = new RestRequest(webHostAddress, Method.PUT);
            request.AddBody(data);
            var client = new RestClient();
            IRestResponse response = client.Execute(request);

            StatusCode = response.StatusCode.ToString();
            ResponseText = response.Content;
        }

        #endregion

        #region Methods

        internal static IBoomerang GivenAServerOnSpecificPort()
        {
            // Starts on specific port so conflicts will be easier to detect in tests
            defaultServer = Boomerang.Server(5100);
            return defaultServer;
        }

        #endregion
    }
}