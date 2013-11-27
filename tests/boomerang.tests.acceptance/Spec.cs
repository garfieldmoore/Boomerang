namespace boomerang.tests.acceptance
{
    using Rainbow.Testing.Boomerang.Host;

    using RestSharp;

    internal class Spec
    {
        public static string StatusCode { get; set; }
        public static string ResponseText { get; set; }

        internal static IBoomerang GivenADefaultServer()
        {
            return Boomerang.Server(5200);
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

        public static void WhenWebGetRequestSent(string webHostAddress)
        {
            var request = new RestRequest(webHostAddress, Method.GET);
            var client = new RestClient();
            var response = client.Execute(request);

            StatusCode = response.StatusCode.ToString();
            ResponseText = response.Content;
        }
    }
}