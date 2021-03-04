using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HououinBot.Models {
    public class User {
        public long ChatId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User() { }

        public User(long chatId, string userName, string firstName, string lastName) {
            ChatId = chatId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
        }

        public override bool Equals(object obj) {
            if (obj != null && obj is User u) {
                return ChatId == u.ChatId;
            }
            return false;
        }

        public override int GetHashCode() {
            return ChatId.GetHashCode();
        }
    }
}
