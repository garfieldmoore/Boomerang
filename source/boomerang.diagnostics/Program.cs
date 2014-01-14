using System;
using System.Collections.Generic;
using System.Linq;

namespace boomerang.diagnostics
{
    using Rainbow.Testing.Boomerang.Host;

    class Program
    {
        private static List<Request> requests;
        static void Main(string[] args)
        {
            requests = new List<Request>();
            Console.WriteLine("Starting boomerang server");
            Boomerang.Server(5100).Get("/api/menu").Returns("blah", 200).Returns("blah blah", 201);

            while (true)
            {
                // TODO this should be done using events....
                var req = Boomerang.Server().GetAllReceivedRequests().ToList();

                if (HasNewRequests(req))
                {
                    DisplayNewRequests(req);
                }

                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey().Key == ConsoleKey.X)
                    {
                        break;
                    }
                }
            }
        }

        private static void DisplayNewRequests(List<Request> req)
        {
            foreach (var request in req)
            {
                if (IsNew(request))
                {
                    Console.WriteLine(request);
                }
            }

            requests.AddRange(req);
        }

        private static bool IsNew(Request request)
        {
            return requests.Contains(request) == false;
        }

        private static bool HasNewRequests(List<Request> req)
        {
            return null != req && req.Count > 0 && req.Count != requests.Count;
        }
    }
}
