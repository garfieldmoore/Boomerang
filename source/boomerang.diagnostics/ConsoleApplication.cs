namespace boomerang.diagnostics
{
    using System;
    using System.Collections.Generic;

    internal class ConsoleApplication
    {
        private readonly Dictionary<string, MenuOption> options;

        public ConsoleApplication(Dictionary<string, MenuOption> menuOptions)
        {
            options = menuOptions;
        }

        public void Run()
        {
            DisplayTitle();
            DisplayMenu();
            MainLoop();
        }

        private void MainLoop()
        {
            while (true)
            {
                var option = Console.ReadKey(true);
                MenuOption menuOption;
                var available = options.TryGetValue(option.Key.ToString(), out menuOption);
                if (available)
                {
                    menuOption.Method.Execute();
                }
            }
        }

        private void DisplayTitle()
        {
            Console.Title = Title;
            Console.WriteLine(Title);
        }

        public string Title { get; set; }

        private void DisplayMenu()
        {
            Console.WriteLine("Select from the menu:");
            foreach (var option in options.Values)
            {
                Console.WriteLine(option.Title);
            }
        }
    }
}