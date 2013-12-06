using System;

namespace coffee.demo
{
    using System.Configuration;

    using CoffeTime.Api;

    class Program
    {
        private static string baseAddress = "http://localhost:64744/api/";

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(GetBannerText());
            Console.WriteLine("Coffee time menu");

            var service = new MenuService();

            var menuItems = service.GetMenu(baseAddress + "menu");
            foreach (var menuItem in menuItems)
            {
                Console.WriteLine(string.Format("Item: {0}, Description {1}", menuItem.Name, menuItem.Description));
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

        }

        private static string GetBannerText()
        {
            return  ConfigurationSettings.AppSettings.Get("Banner");
        }
    }
}
