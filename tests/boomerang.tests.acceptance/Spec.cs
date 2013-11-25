namespace boomerang.tests.acceptance
{
    using System.IO;
    using System.Net;

    using Rainbow.Testing.Boomerang.Host;

    internal class Spec
    {
        public static string StatusCode { get; set; }
        public static string ResponseText { get; set; }
        public static string StatusDescription { get; set; }

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

                using (var response = request.GetResponse())
                {
                    SetResponseValuesFrom(response);
                    response.Close();
                }

            }
            catch (WebException e)
            {
                if (e.Response != null)
                {
                    SetResponseValuesFrom(e.Response);
                }
                else
                {
                    throw;
                }
            }
        }

        private static void SetResponseValuesFrom(WebResponse response)
        {
            var httpWebResponse = ((HttpWebResponse)response);
            StatusDescription = httpWebResponse.StatusDescription;
            StatusCode = httpWebResponse.StatusCode.ToString();
            var responeText = ReadFromStream(response.GetResponseStream());
            response.Close();

            ResponseText = responeText;
        }

        private static string ReadFromStream(Stream response)
        {
            string responseText;
            using (var reader = new StreamReader(response))
            {
                responseText = reader.ReadToEnd();
            }

            return responseText;
        }
    }
}