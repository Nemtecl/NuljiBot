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

        /// <summary>
        /// Méthode de prise en charge de la commande membercount
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="user"></param>
        public async void MembercountAsync(IGuild guild, IUser user)
        {
            var users = await guild.GetUsersAsync();
            (var nbBots, var nbHumans, var nbOnline) = (0, 0, 0);

            foreach (var u in users)
            {
                if (u.IsBot)
                    nbBots += 1;
                else
                    nbHumans += 1;
                nbOnline += (u.Status != UserStatus.Offline ? 1 : 0);
            }

            var builder = EmbedBuilderHelper.EmbedBuilderInformation(user)
                .WithTitle("Membercount command")
                .AddField("Membres", users.Count, true)
                .AddField("En ligne", nbOnline, true)
                .AddField("Humains", nbHumans, true)
                .AddField("Bots", nbBots, true);

            Reply("", builder);
        }

        /// <summary>
        /// Méthode de prise en charge de la commande serverinfo
        /// </summary>
        /// <param name="guild"></param>
        public async void ServerinfoAsync(IGuild guild)
        {
            //TODO: méthode pour factoriser comptage user/bot/online

            var owner = await guild.GetOwnerAsync();

            var builder = new EmbedBuilder()
                .WithTitle("Serverinfo command")
                .AddField("Propriétaire", owner, true)
                .AddField("Région", guild.VoiceRegionId, true)
                .AddField("Roles", guild.Roles.ToString())
                .WithTimestamp(guild.CreatedAt)
                .WithFooter(footer =>
                {
                    footer.WithText($"ID: {guild.Id} | Serveur créé le");
                });

            Reply("", builder);
        }
    }
}
