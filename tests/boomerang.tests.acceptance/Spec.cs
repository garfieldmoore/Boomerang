namespace boomerang.tests.acceptance
{
    using System;
    using System.IO;
    using System.Net;

    using Rainbow.Testing.Boomerang.Host;

    internal class Spec
    {
        internal static IBoomerang GivenADefaultServer()
        {
            return Boomerang.Server(5200);
        }

        public static void WhenWebGetRequestSent(string webHostAddress)
        {
            try
            {
                var request = WebRequest.Create(webHostAddress);
                request.Method = "Get";

                var response = request.GetResponse();
                var httpWebResponse = ((HttpWebResponse)response);

                StatusDescription = httpWebResponse.StatusDescription;
                StatusCode = httpWebResponse.StatusCode.ToString();

                var responeText = ReadFromStream(response.GetResponseStream());
                response.Close();

                ResponseText = responeText;
            }
            catch (WebException e)
            {
                var httpWebResponse = e.Response as HttpWebResponse;
                if (httpWebResponse != null)
                {
                    StatusCode = httpWebResponse.StatusCode.ToString();
                    StatusDescription = httpWebResponse.StatusDescription;
                }

                if (e.Response != null)
                {
                    var responeText = ReadFromStream(e.Response.GetResponseStream());
                    ResponseText = responeText;
                }
            }
        }

        private static string ReadFromStream(Stream response)
        {
            var responseText = string.Empty;
            using (StreamReader reader = new StreamReader(response))
            {
                responseText = reader.ReadToEnd();
            }

            return responseText;
        }

        public static string StatusCode { get; set; }

        public static string ResponseText { get; set; }

        public static string StatusDescription { get; set; }
    }
}