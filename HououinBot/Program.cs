using System;
using HououinBot.Models;

namespace HououinBot {
    class Program {
        private static BotServer _bot;

        static void Main() {
            Debug.LogSuccess("Press 'Enter' to start");
            while (true) {
                if (Console.KeyAvailable) {
                    switch (Console.ReadKey(true).Key) {
                        case ConsoleKey.Enter:
                            if (_bot == null) {
                                Console.Clear();
                                Debug.LogInfo("Begin work...");
                                _bot = new BotServer();
                            }
                            break;
                        case ConsoleKey.Escape:
                            _bot.UpdateUsersData();
                            Debug.LogInfo("End work...");
                            Environment.Exit(0);
                            break;
                    }
                }
            }
        }
    }
}
