using Discord;
using System.Diagnostics;
using System.Globalization;

namespace NuljiBot.Helpers
{
    public static class EmbedBuilderHelper
    {
        public static EmbedBuilder EmbedBuilderUptime(string description, string title = "Uptime")
        {
            var processStartTime = Process.GetCurrentProcess().StartTime;
            var builder = new EmbedBuilder()
                .WithTitle(title)
                .WithDescription($"{description}")
                .WithFooter(footer =>
                {
                    footer
                        .WithText($"Développé par {JsonHelper.GetAuthorName()} " +
                        $"| Actif depuis le " +
                        $"{processStartTime.ToString("f", CultureInfo.CreateSpecificCulture(JsonHelper.GetCulture()))}")
                        .WithIconUrl(JsonHelper.GetAuthorIconUrl());
                        
                });

            return builder;
        }

        public static EmbedBuilder EmbedBuilderInformation(IUser user)
        {
            var builder = new EmbedBuilder()
                .WithCurrentTimestamp()
                .WithFooter(footer =>
                {
                    footer
                        .WithText($"Commande lancée par {user.Username}")
                        .WithIconUrl(user.GetAvatarUrl());
                });

            return builder;
        }

    }
}
