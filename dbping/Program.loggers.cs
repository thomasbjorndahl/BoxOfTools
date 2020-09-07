using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VareNo.common
{
    public static class Loggers
    {
        #region loggers
        public static void WriteError(string message)
        {
            var cl = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error occured: {message}");
            Console.ForegroundColor = cl;
        }

        public static void WriteMessage(string message)
        {
            Console.WriteLine($"{message}");

        }
        public static void Write(string message)
        {
            Console.Write($"{message}");

        }
        #endregion
    }
}
