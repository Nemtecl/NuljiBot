using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace NuljiBot.Helpers
{
    public static class JsonHelper
    {
        private class DataType
        {
            public string Token { get; set; }
            public string Prefix { get; set; }
            public string CurrentGame { get; set; }
            public JoinedMessage JoinedMessage { get; set; }
            public string LeftMessage { get; set; }
            public EmbedBuilder EmbedBuilder { get; set; }
        }

        private class JoinedMessage
        {
            public string Start { get; set; }
            public string End { get; set; }
        }

        private class EmbedBuilder
        {
            public Author Author { get; set; }
            public string Color { get; set; }
        }

        private class Author
        {
            public string Name { get; set; }
            public string IconUrl { get; set; }
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

        public static string GetAuthorIconUrl()
        {
            DataType data = JsonConvert.DeserializeObject<DataType>(File.ReadAllText("config.json"));
            return data.EmbedBuilder.Author.IconUrl;
        }

        public static string GetAuthorName()
        {
            DataType data = JsonConvert.DeserializeObject<DataType>(File.ReadAllText("config.json"));
            return data.EmbedBuilder.Author.Name;
        }
    }
}
