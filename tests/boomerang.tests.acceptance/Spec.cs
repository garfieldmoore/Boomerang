namespace boomerang.tests.acceptance
{
    using System.Collections.Generic;
    using System.Linq;

    using Rainbow.Testing.Boomerang.Host;

    using RestSharp;

    internal class Spec
    {
        private static IBoomerang defaultServer;

        public static string StatusCode { get; set; }
        public static string ResponseText { get; set; }

        public static readonly string HostAddress = "http://rainbow.co.uk/";

        public static IDictionary<string, string> ResponseHeaders;

        public static IList<Request> ReceivedRequests
        {
            get
            {
                return defaultServer.GetAllReceivedRequests().ToList();
            }
        }

        internal static IBoomerang GivenADefaultServer()
        {
            defaultServer = Boomerang.Server(5100);
            return defaultServer;
        }

        public static void WhenPostsSentTo(string webHostAddress, string data)
        {
            var request = new RestRequest(webHostAddress, Method.POST);
            request.AddBody(data);
            var client = new RestClient();
            var response = client.Execute(request);

            StatusCode = response.StatusCode.ToString();
            ResponseText = response.Content;
        }

        public static void WhenGetRequestSent(string webHostAddress)
        {
            ResponseHeaders=new Dictionary<string, string>();
            var request = new RestRequest(webHostAddress, Method.GET);
            var client = new RestClient();
            
            var response = client.Execute(request);

            StatusCode = response.StatusCode.ToString();
            ResponseText = response.Content;

            foreach (var parameter in response.Headers)
            {
                ResponseHeaders.Add(parameter.Name, parameter.Value.ToString());
            }
        }

        public static void WhenPutSentTo(string webHostAddress, string data)
        {
            var request = new RestRequest(webHostAddress, Method.PUT);
            request.AddBody(data);
            var client = new RestClient();
            var response = client.Execute(request);

            StatusCode = response.StatusCode.ToString();
            ResponseText = response.Content;
        }

        public static void WhenDeleteSentTo(string webAddress)
        {
            var request = new RestRequest(webAddress, Method.DELETE);
            var client = new RestClient();
            var response = client.Execute(request);

            StatusCode = response.StatusCode.ToString();
            ResponseText = response.Content;
        }        
    }
}