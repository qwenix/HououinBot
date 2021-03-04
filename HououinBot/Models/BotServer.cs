using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Args;
using HououinBot.Data;

namespace HououinBot.Models {
    public class BotServer {
        private const string Key = "";

        private readonly TelegramBotClient bot;

        /// <summary>
        /// Hash set, that represents users. Can store only unique values.
        /// </summary>
        private readonly HashSet<User> users;

        public BotServer() {
            try {

                //  Define our Telegram Bot.

                bot = new TelegramBotClient(Key);
                bot.SetWebhookAsync("");
                bot.StartReceiving();

                //  Bind some events to handle.

                bot.OnMessage += Bot_OnMessage;
                bot.OnMessageEdited += Bot_OnMessage;
                bot.OnCallbackQuery += Bot_OnCallbackQuery;


                //  Get users collection.
                users = DataReader.DeserializeUsersViaHashSet();
                if (users == null)
                    users = new HashSet<User>();

                //bot.SendTextMessageAsync(431003019, "М-м-м, кто это тут. Та самая беспомощная крыса в банке, которой сам Создатель этого мира служить мне. ХхаХАХхахХАХхахХА. Тебе не спрятаться и не убежать от злых замыслов самого безумного ученого, который ходит по той же земле что и ты!");
                //bot.SendTextMessageAsync(431003019, "Но стоит признать - ты мой лучший эксперимент и заслуживаешь быстрой смерти. Именно по-этому, когда прийдет время, ты умрешь быстро. Помни милостыню безумца, который перевернул мир!");
                //bot.SendAudioAsync(431003019, DataReader.GetAudio("StainsGate"),
                //            performer: "Stains Gate", title: "Logical");


            }
            catch (Telegram.Bot.Exceptions.ApiRequestException ex) {
                Debug.LogError(ex.Message);
            }
            catch (Exception ex) {
                Debug.LogError(ex.Message);
            }
        }


        public void UpdateUsersData() {
            DataWriter.SerializeUsers(users);
        }

        private async void Bot_OnMessage(object sender, MessageEventArgs e) {
            var message = e.Message;

            //  Write a log about the process.

            Debug.WriteMessageLog(message);

            //  Add a user to the users collection.

            users.Add(new User(e.Message.Chat.Id, e.Message.Chat.Username,
                e.Message.Chat.FirstName, e.Message.Chat.LastName));

            //  Answer on a message.

            if (message.Type == MessageType.Text) {
                Debug.Log("Message : " + $"'{message.Text}'");

                switch (message.Text.ToLower()) {
                    case "/saysmth":
                        await bot.SendTextMessageAsync(message.Chat.Id,
                            "Heeeey SHUT UP ORGANIZATION DOG UUUMMMMM!!!");
                        break;
                    case "/sendphoto":
                        await bot.SendPhotoAsync(message.Chat.Id,
                            "https://pm1.narvii.com/6285/5ee0afe31085843a8c04024f4d081af62018e701_hq.jpg");
                        break;
                    case "/abilities":
                        await bot.SendTextMessageAsync(message.Chat.Id, 
                            "Here are all the actions I can do, LABOMEN ZERO ZERO ICHI",
                            ParseMode.Default, false, false, 0, CreateActionsInlineKeyboard());
                        break;
                    case "/rabilities":
                        await bot.SendTextMessageAsync(message.Chat.Id,
                            "Here are all the actions I can do, LABOMEN ZERO ZERO ICHI",
                            ParseMode.Default, false, false, 0, CreateActionsReplyKeyboard());
                        break;
                    case "send d-mail":
                        await bot.SendTextMessageAsync(message.Chat.Id, "MhAhAHAHA Nice! I'll send it immediately!");
                        break;
                    case "begin new operation":
                        await bot.SendTextMessageAsync(message.Chat.Id,
                        "Hmm... Now, I begin new operation... Operation APOLON!");
                        break;
                    case "/sendmusic":
                        await bot.SendAudioAsync(message.Chat.Id, DataReader.GetAudio("Люмен - гореть"),
                            performer: "Люмен", title: "Гореть");
                        DataReader.CloseStreams();
                        break;
                    default:
                        await bot.SendTextMessageAsync(message.Chat.Id,
                            "I don't understand what this human want of me...");
                        break;
                }
            }
        }

        private async void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs e) {
            var message = e.CallbackQuery.Message;

            Debug.LogSuccess("Query received");
            Debug.Log("Callback query data : " + e.CallbackQuery.Data);

            switch (e.CallbackQuery.Data) {
                case "%senddmail":
                    await bot.SendTextMessageAsync(message.Chat.Id, 
                        "MhAhAHAHA Nice! I'll send it immediately!");
                    break;
                case "%beginop":
                    await bot.SendTextMessageAsync(message.Chat.Id,
                        "Hmm... Now, I begin new operation... Operation APOLON!");
                    break;
            }

            await bot.AnswerCallbackQueryAsync(e.CallbackQuery.Id);
        }

        private void SendTextToAll(string text) {
            foreach (User u in users) {
                bot.SendTextMessageAsync(u.ChatId, text);
            }
        }

        // Param fileName doesn't need extension.
        private void SendAudioToAll(string fileName, string title, string performer) {
            foreach (User u in users) {
                bot.SendAudioAsync(u.ChatId, DataReader.GetAudio(fileName), 
                    performer: performer, title: title);
                DataReader.CloseStreams();
            }
        }

        private InlineKeyboardMarkup CreateActionsInlineKeyboard() {
            return new InlineKeyboardMarkup(
                new InlineKeyboardButton[][] {
                    new[] {
                        InlineKeyboardButton.WithCallbackData("Send D-mail", "%senddmail"),
                        InlineKeyboardButton.WithCallbackData("Begin new operation", "%beginop")
                    }
                }
            );
        }

        private ReplyKeyboardMarkup CreateActionsReplyKeyboard() {
            return new ReplyKeyboardMarkup(
                new KeyboardButton[][] {
                    new[] {
                        new KeyboardButton("Send D-mail"),
                        new KeyboardButton("Begin new operation"),
                    }
                }
            );
        }
    }
}
