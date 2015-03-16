using System;

namespace boomerang.diagnostics
{
    using Rainbow.Testing.Boomerang.Host;

    class Program
    {
        private static IBoomerang _webHost;

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.DomainUnload+=CurrentDomain_DomainUnload;
            var listeningOnPort = 5100;
            var address = "http://localhost";
            Console.WriteLine("Starting boomerang server at :'{0}:{1}'", address, listeningOnPort);
            DisplayInstructions();

            _webHost = Boomerang.Create(x => x.AtAddress(string.Format("{0}:{1}", address, listeningOnPort)));
            _webHost.Start();
            _webHost.OnReceivedRequest += OnReceivedRequest_Display;

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var consoleKeyInfo = Console.ReadKey();

                    if (consoleKeyInfo.Key == ConsoleKey.X)
                    {
                        break;
                    }

                    if (consoleKeyInfo.Key == ConsoleKey.D1)
                    {
                        AddRequest();
                    }
                }
            }

            _webHost.Stop();
        }

        private static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            _webHost.Stop();
        }

        private static void OnReceivedRequest_Display(object sender, ProxyRequestEventArgs e)
        {
            if (null != e)
                Console.WriteLine(string.Format("{0}: received for address: {1}", e.Method, e.RelativePath));
        }

        private static void AddRequest()
        {
            Console.Clear();

            Console.WriteLine("Enter the HTTP verb for the request");
            var method = Console.ReadLine();
            Console.WriteLine("enter the relative address for the response");
            var address = Console.ReadLine();
            Console.WriteLine("Enter the required response content");
            var response = Console.ReadLine();
            Console.WriteLine("Enter the response status code");
            var statusCodeText = Console.ReadLine() + "";
            var statusCode = int.Parse(statusCodeText);

            _webHost.Request(address, method).Returns(response, statusCode);

            Console.Clear();
            DisplayInstructions();
        }

        private static void DisplayInstructions()
        {
            Console.WriteLine("Choose from the below options or start sending requests to boomerang;\r\n");
            Console.WriteLine("1. To enter an expected request response");
        }
    }
}
