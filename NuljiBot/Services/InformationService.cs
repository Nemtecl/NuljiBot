using Discord;
using Humanizer;
using System;
using System.Diagnostics;
using NuljiBot.Helpers;

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
            var uptime = DateTime.Now - Process.GetCurrentProcess().StartTime;
            //string s = uptime.Days + "jours " + uptime.v
            var builder = new EmbedBuilder()
                .WithTitle("Uptime")
                .WithDescription($"{uptime.Humanize()}")
                .WithFooter(footer =>
                {
                    footer
                        .WithText($"{JsonHelper.GetAuthorName()} | Lancé pour la dernière fois le <insérer une date>")
                        .WithIconUrl(JsonHelper.GetAuthorIconUrl());
                });
            Reply("", builder);
        }
    }
}
