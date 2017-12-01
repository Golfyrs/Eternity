using System;
using Eternity.Server;

namespace Server.Console
{
    public class Core
    {
        public static void Main(string[] args)
        {
            var server = new EternityServer("25.70.57.150", 5555);
            server.Start();
            
            ConsoleKeyInfo keyInfo;
            do
                keyInfo = System.Console.ReadKey(); while (keyInfo.Key != ConsoleKey.Escape);
        }
    }
}
