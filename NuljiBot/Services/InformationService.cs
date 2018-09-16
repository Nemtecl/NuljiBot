using Discord;
using System;
using System.Diagnostics;
using NuljiBot.Helpers;
using System.Linq;
using Discord.WebSocket;
using Humanizer;
using Discord.Commands;

namespace NuljiBot.Services
{
    /// <summary>
    /// Objet de prise en charge des commandes d'aide
    /// </summary>
    public sealed class InformationService : NuljiService
    {

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
                if (guild.Roles.Count != 1 && !role.Equals(guild.Roles.Last()))
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
            // Recherche de l'utilisateur
            // Si aucun username a été indiqué, on choisi l'utilisateur ayant lancé la commande
            var currentUsers = (await guild.GetUsersAsync());
            IUser currentUser = (username == null ? user : currentUsers
                .Where(o => o.Username.ToLower().Contains(username.ToLower())).FirstOrDefault());

            // Si l'username indiqué n'existe pas
            if (currentUser == null)
            {
                Reply($"{user.Mention} Impossible de trouver l'utilisateur {username} :zipper_mouth:");
            }
            else
            {
                SocketGuildUser currentGuildUser = (SocketGuildUser) (await guild.GetUserAsync(currentUser.Id));
                var sRoles = "";
                foreach (var role in currentGuildUser.Roles)
                {
                    sRoles += role.Mention;
                    if (currentGuildUser.Roles.Count != 1 && !role.Equals(currentGuildUser.Roles.Last()))
                        sRoles += ", ";
                }

                var guildPermissionsList = currentGuildUser.GuildPermissions.ToList();
                var sPermissions = "";
                foreach (var permission in guildPermissionsList)
                {
                    sPermissions += permission.Humanize();
                    if (guildPermissionsList.Count != 1 && !permission.Equals(guildPermissionsList.Last()))
                        sPermissions += ", ";
                } 

                var builder = new EmbedBuilder()
                    .WithCurrentTimestamp()
                    .WithThumbnailUrl(currentUser.GetAvatarUrl())
                    .WithDescription(currentUser.Mention)
                    .AddField("Statut", currentUser.Status, true)
                    .AddField("Date d'arrivée", currentGuildUser.JoinedAt, true)
                    .AddField("Activité", currentUser.Activity == null ? "Aucune" : currentUser.Activity.Type + " " +currentUser.Activity.Name, true)
                    .AddField("Date d'inscription", currentUser.CreatedAt, true)
                    .AddField("Roles", sRoles)
                    .AddField("Permissions", sPermissions)
                    .WithAuthor(author =>
                    {
                        author
                            .WithName(currentUser.ToString())
                            .WithIconUrl(currentUser.GetAvatarUrl());
                    })
                    .WithFooter(footer =>
                    {
                        footer
                            .WithText($"ID: {currentUser.Id}");
                    });

                Reply("", builder);
            }
        }

        /// <summary>
        /// Méthode de prise en charge de la commande help
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="provider"></param>
        /// <param name="guild"></param>
        /// <param name="user"></param>
        public void HelpAsync(CommandService commands, IServiceProvider provider, IGuild guild, IUser user)
        {
            var builder = EmbedBuilderHelper.EmbedBuilderUptime(null, "Help");
            foreach (var module in commands.Modules)
            {

            }

            Reply($"", builder);
        }
    }
}
