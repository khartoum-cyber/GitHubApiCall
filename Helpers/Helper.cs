namespace GitHubApiCall.Helpers
{
    internal static class Helper
    {
        internal static void PrintInfoMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n" + message);
            Console.ResetColor();
        }

        internal static void PrintCommandMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n" + message + "\n");
            Console.ResetColor();
        }

        internal static void PrintWarningMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n" + message);
            Console.ResetColor();
        }

        internal static void WelcomeMessage()
        {
            Helper.PrintInfoMessage("Hello, Welcome to GitHub User Activity!");
            Helper.PrintInfoMessage("Type \"help\" to know the set of API commands.");
        }

        internal static string? PromptForUsername(int maxRetries = 5)
        {
            var retryCount = 0;

            while (retryCount < maxRetries)
            {
                Console.WriteLine("\nEnter GitHub username:");
                var username = Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(username)) 
                    return username;

                retryCount++;
                Console.WriteLine("Username cannot be empty. Please try again or press Esc to exit.");

                var keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("\nESC key pressed. Exiting...");
                    return null;
                }
            }

            Console.WriteLine("Too many failed attempts. Exiting...");
            return null;
        }
    }
}
