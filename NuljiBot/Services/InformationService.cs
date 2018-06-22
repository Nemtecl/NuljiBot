using Discord;
using Humanizer;
using System;
using System.Diagnostics;
using NuljiBot.Helpers;
using System.Globalization;

namespace NuljiBot.Services
{
    /// <summary>
    /// Objet de prise en charge des commandes d'aide
    /// </summary>
    public sealed class InformationService : NuljiService
    {
        // todo : help, github, futur site + commandes info dyno

        /// <summary>
        /// Méthode de prise en charge de la commande help
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="user"></param>
        public void HelpAsync(IMessageChannel channel, IUser user)
        {
            Reply($"{user.Mention} Not implemented yet ! :middle_finger: ");
        }

        public void UptimeAsync(IMessageChannel channel, IUser user)
        {
            var processStartTime = Process.GetCurrentProcess().StartTime;
            TimeSpan uptime = DateTime.Now - processStartTime;
            var builder = new EmbedBuilder()
                .WithTitle("Uptime")
                .WithDescription($"{uptime.Humanize()}")
                .WithFooter(footer =>
                {
                    footer
                        .WithText($"Développé par {JsonHelper.GetAuthorName()} " +
                        $"| Actif depuis {processStartTime.ToString("f", CultureInfo.CreateSpecificCulture("fr-FR"))}")
                        .WithIconUrl(JsonHelper.GetAuthorIconUrl());
                });
            Reply("", builder);
        }
    }
}
