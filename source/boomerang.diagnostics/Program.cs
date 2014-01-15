using System;

namespace boomerang.diagnostics
{
    using Rainbow.Testing.Boomerang.Host;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting boomerang server");
            DisplayInstructions();

            Boomerang.Server(5100).OnReceivedRequest += OnReceivedRequest_Display;

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

            Boomerang.Server(5100).Request(address, method).Returns(response, statusCode);

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
