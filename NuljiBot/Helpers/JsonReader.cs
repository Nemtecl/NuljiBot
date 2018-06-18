using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace NuljiBot.Helpers
{
    public static class JsonHelper
    {
        internal class DataType
        {
            public string Token { get; set; }
            public string Prefix { get; set; }
            public string CurrentGame { get; set; }
            public JoinedMessage JoinedMessage { get; set; }
            public string LeftMessage { get; set; }
        }

        internal class JoinedMessage
        {
            public string Start { get; set; }
            public string End { get; set; }
        }

        public static string GetToken()
        {
            DataType data = JsonConvert.DeserializeObject<DataType>(File.ReadAllText("config.json"));
            return data.Token;
        }

        public static char GetPrefix()
        {
            DataType data = JsonConvert.DeserializeObject<DataType>(File.ReadAllText("config.json"));
            return data.Prefix[0];
        }

        public static string GetCurrentGame()
        {
            DataType data = JsonConvert.DeserializeObject<DataType>(File.ReadAllText("config.json"));
            return data.CurrentGame;
        }

        public static List<string> GetJoinedMessage()
        {
            DataType data = JsonConvert.DeserializeObject<DataType>(File.ReadAllText("config.json"));
            return new List<string> { data.JoinedMessage.Start, data.JoinedMessage.End };
        }

        public static string GetLeftMessage()
        {
            DataType data = JsonConvert.DeserializeObject<DataType>(File.ReadAllText("config.json"));
            return data.LeftMessage;
        }
    }
}
