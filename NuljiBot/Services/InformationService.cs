using Discord;
using System;
using System.Diagnostics;
using NuljiBot.Helpers;
using System.Linq;

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

            var builder = EmbedBuilderHelper.EmbedBuilderInformation(user)
                .WithTitle("Membercount command")
                .AddField("Membres", await InformationHelper.GetNbUsers(guild), true)
                .AddField("En ligne", await InformationHelper.GetNbOnline(guild), true)
                .AddField("Humains", await InformationHelper.GetNbHumans(guild), true)
                .AddField("Bots", await InformationHelper.GetNbBots(guild), true);

            Reply("", builder);
        }

        /// <summary>
        /// Méthode de prise en charge de la commande serverinfo
        /// </summary>
        /// <param name="guild"></param>
        public async void ServerinfoAsync(IGuild guild)
        {
            string sRoles = "";
            foreach (var role in guild.Roles)
            {
                sRoles += role.Name;
                if (!role.Equals(guild.Roles.Last()))
                    sRoles += ", ";
                
            }

            var builder = new EmbedBuilder()
                .WithThumbnailUrl(guild.IconUrl)
                .AddField("Propriétaire", await guild.GetOwnerAsync(), true)
                .AddField("Région", guild.VoiceRegionId, true)
                .AddField("Catégories de salon", (await guild.GetCategoriesAsync()).Count, true)
                .AddField("Salons textuels", (await guild.GetTextChannelsAsync()).Count, true)
                .AddField("Salons vocaux", (await guild.GetVoiceChannelsAsync()).Count, true)
                .AddField("Membres", await InformationHelper.GetNbUsers(guild), true)
                .AddField("En ligne", await InformationHelper.GetNbOnline(guild), true)
                .AddField("Humains", await InformationHelper.GetNbHumans(guild), true)
                .AddField("Bots", await InformationHelper.GetNbBots(guild), true)
                .AddField("Roles", guild.Roles.Count, true)
                .AddField("Liste des roles", sRoles)
                .WithTimestamp(guild.CreatedAt)
                .WithAuthor(author =>
                {
                    author
                    .WithName(guild.Name)
                    .WithIconUrl(guild.IconUrl);
                })
                .WithFooter(footer =>
                {
                    footer
                        .WithText($"ID: {guild.Id} | Serveur créé le");
                });

            Reply("", builder);
        }

        /// <summary>
        /// Méthode de prise en charge de la commande whois
        /// </summary>
        /// <param name="guild"></param>
        /// <param name="user"></param>
        /// <param name="username"></param>
        public async void WhoisAsync(IGuild guild, IUser user, string username)
        {
            var currentUsers = (await guild.GetUsersAsync());
            // TODO: check si aucun user avant de faire le first
            IUser currentUser = (username == null ? user : currentUsers.Where(o => o.Username.Contains(username)).First());

            if (currentUser == null)
            {
                Reply($"{user.Mention} Impossible de trouver l'utilisateur {username} :zipper_mouth:");
            }
            else
            {
                Reply($"{currentUser.Mention} TODO");
            }
        }
    }
}
