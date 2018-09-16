using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuljiBot.Helpers.SharedClass;
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
            public string Culture { get; set; }
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

        public static string GetCulture()
        {
            DataType data = JsonConvert.DeserializeObject<DataType>(File.ReadAllText("config.json"));
            return data.Culture;
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

        public static Almanax GetAlmanax(string date)
        {
            JObject json = JObject.Parse(File.ReadAllText("almanax.json"));
            Almanax almanax = new Almanax
            {
                ItemImage = (int)json[date]["itemImage"],
                Quest = (string)json[date]["quest"],
                Type = (string)json[date]["type"],
                Effect = (string)json[date]["effect"],
                Offering = (string)json[date]["offering"],
            };
            return almanax;
        }
    }
}
