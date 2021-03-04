using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using HououinBot.Models;
using System.Runtime.Serialization;
using System.Text;

namespace HououinBot.Data {
    public static class DataWriter {
        public static void SerializeUsers(IEnumerable<User> users) {
            SerializeToJson(users, Paths.UsersPath);
        }

        private static void SerializeToJson<T>(T obj, string path) {
            using (StreamWriter sw = new StreamWriter(path)) {
                string json = JsonConvert.SerializeObject(obj);
                sw.Write(json);
            }
        } 
    }
}
