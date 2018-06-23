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
        /// <summary>
        /// Méthode de prise en charge de la commande help
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="user"></param>
        public void HelpAsync(IMessageChannel channel, IUser user)
        {
            Reply($"{user.Mention} Not implemented yet ! :middle_finger: ");
        }

        /// <summary>
        /// Méthode de prise en charge de la commande uptime
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="user"></param>
        public void UptimeAsync(IMessageChannel channel, IUser user)
        {
            var processStartTime = Process.GetCurrentProcess().StartTime;
            TimeSpan uptime = DateTime.Now - processStartTime;

            string description = "";
            if (uptime.Days != 0)
                description += $"{uptime.Days} jour" + (uptime.Days == 1 ? "" : "s") + ", ";
            if (uptime.Hours != 0)
                description += $"{uptime.Hours} heure" + (uptime.Hours == 1 ? "" : "s") + ", ";
            if (uptime.Minutes != 0)
                description += $"{uptime.Minutes} minute" + (uptime.Minutes == 1 ? "" : "s") + ", ";
            description += $"{uptime.Seconds} seconde" + (uptime.Seconds > 1 ? "s" : "");

            var builder = EmbedBuilderHelper.EmbedBuilderUptime(description);

            Reply("", builder);
        }
    }
}
