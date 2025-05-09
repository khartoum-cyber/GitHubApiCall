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
    }
}
