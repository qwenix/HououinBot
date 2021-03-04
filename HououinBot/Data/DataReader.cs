using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using HououinBot.Models;
using System.Text;
using System.Reflection;
using Telegram.Bot.Types.InputFiles;

namespace HououinBot.Data {

    internal struct Paths {
        public static readonly string UsersPath = $@"..\..\..\Files\users.json";
        public static readonly string MusicPath = $@"..\..\..\Files\Music\";
    }

    public static class DataReader {
        // All unclosed streams. 
        private static List<Stream> _activeStreams = new List<Stream>();

        public static HashSet<User> DeserializeUsersViaHashSet() {
            return DeserializeFromJson<HashSet<User>>(Paths.UsersPath);
        }

        public static IEnumerable<User> DeserializeUsers() {
            return DeserializeFromJson<IEnumerable<User>>(Paths.UsersPath);
        }

        //  Returns audiofile. Supports only mp3 files.
        public static InputOnlineFile GetAudio(string title) {
            return GetFile(Paths.MusicPath + title + ".mp3");
        }

        public static InputOnlineFile GetFile(string path) {
            if (File.Exists(path)) {    
                FileStream fs = new FileStream(path, FileMode.Open);
                _activeStreams.Add(fs);
                return new InputOnlineFile(fs);
            }
            return null;
        }

        public static void CloseStreams() {
            if (_activeStreams.Count > 0) {
                foreach (Stream s in _activeStreams) {
                    s.Close();
                }
                _activeStreams = new List<Stream>();
            }
        }

        private static T DeserializeFromJson<T>(string path) {
            using (StreamReader sr = new StreamReader(path)) {
                string json = sr.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }
    }
}
