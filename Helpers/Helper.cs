using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
