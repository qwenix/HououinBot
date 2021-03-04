using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;
using HououinBot.Models;

namespace HououinBot {
    public static class Debug {
        private const ConsoleColor DefaultColor = ConsoleColor.White;
        private const ConsoleColor ErrorColor = ConsoleColor.Red;
        private const ConsoleColor SuccessColor = ConsoleColor.Green;
        private const ConsoleColor InfoColor = ConsoleColor.Cyan;

        public static void Log(string msg, ConsoleColor color) {
            ConsoleColor previous = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = previous;
        }

        public static void WriteMessageLog(Message message) {
            LogSuccess("Message received");
            Log("Type : " + message.Type);
            Log("First Name : " + message.Chat.FirstName);
            if (message.Chat.LastName != null)
                Log("Last Name : " + message.Chat.LastName);
            Log("Username : " + message.Chat.Username);
            Log("Chat ID : " + message.Chat.Id.ToString());
        }

        public static void Log(string msg) {
            Log(msg, DefaultColor);
        }


        public static void LogError(string msg) {
            Log(msg, ErrorColor);
        }

        public static void LogSuccess(string msg) {
            Log(msg, SuccessColor);
        }

        public static void LogInfo(string msg) {
            Log(msg, InfoColor);
        }
    }
}
