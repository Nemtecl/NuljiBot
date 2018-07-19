using Discord;
using Discord.WebSocket;
using NuljiBot.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NuljiBot.Services
{
    public sealed class AdminService : NuljiService
    {
        /// <summary>
        /// Méthode de prise en charge de la commande clear
        /// </summary>
        /// <param name="guild"></param>
        /// <param name="channel"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public async void ClearMessagesAsync(IGuild guild, IMessageChannel channel, IUser user, int n)
        {
            var eb = new EmbedBuilder();
            eb.WithTitle("Clear command")
                .WithDescription("Supprime [n] messages du channel courrant")
                .AddField("Usage", "!clear [n]")
                .AddField("Require", "Permission de gestion des messages");

            // Vérification des paramètres
            if (n == 0)
            {
                Reply("", eb);
                return;
            }

            // Vérifications des permissions
            var guildUser = await guild.GetUserAsync(user.Id);
            if (!guildUser.GetPermissions(channel as ITextChannel).ManageChannel)
            {
                Reply($"{user.Mention} Vous n'avez pas les permissions pour la gestion des messages");
                Reply("", eb);
                return;
            }

            // Suppression
            var messages = await channel.GetMessagesAsync(n + 1).FlattenAsync();
            await (channel as SocketTextChannel).DeleteMessagesAsync(messages);

            Reply($"{user.Mention} Suppression des messages ...");
        }

        /// <summary>
        /// Méthode de prise en charge de la commande serverinvite
        /// </summary>
        /// <param name="guild"></param>
        public async void ServerinviteAsync(IGuild guild, IUser user)
        {
            var invites = (await guild.GetInvitesAsync());

            if (invites.Count == 0)
            {
                Reply($"{user.Mention} Veuillez créer un lien d'invitation permanent pour ce serveur");
            }
            else
            {
                var invite = invites.Where(o => !o.IsTemporary).First();

                var builder = EmbedBuilderHelper.EmbedBuilderInformation(user)
                    .WithTitle("Serverinvite command")
                    .AddField("Lien d'invitation", invite.Url);

                Reply("", builder);
            }
        }

        /// <summary>
        /// Méthode de prise en charge de la commande mute
        /// </summary>
        /// <param name="guild"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task MuteAsync(IGuild guild, IUser user)
        {
            try
            {
                await (user as IGuildUser).ModifyAsync(o => o.Mute = true);
                Reply($"{user.Mention} a perdu sa voix :zipper_mouth:");
            }
            catch
            {
                Reply($"Problème lors de l'utilisation de la commande !mute {user.Mention}");
            }
        }

        /// <summary>
        /// Méthode de prise en charge de la commande unmute
        /// </summary>
        /// <param name="guild"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task UnmuteAsync(IGuild guild, IUser user)
        {
            try
            {
                await (user as IGuildUser).ModifyAsync(x => x.Mute = false);
                Reply($"{user.Mention} a récupéré sa voix :speaking_head:");
            }
            catch
            {
                Reply($"Problème lors de l'utilisation de la commande !unmute {user.Mention}");
            }
        }

        /// <summary>
        /// Méthode de prise en charge de la commande deaf
        /// </summary>
        /// <param name="guild"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task DeafAsync(IGuild guild, IUser user)
        {
            try
            {
                await (user as IGuildUser).ModifyAsync(x => x.Deaf = true);
                Reply($"{user.Mention} a perdu l'audition :mute:");
            }
            catch
            {
                Reply($"Problème lors de l'utilisation de la commande !undeaf {user.Mention}");
            }
        }

        /// <summary>
        /// Méthode de prise en charge de la commande undeaf
        /// </summary>
        /// <param name="guild"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task UndeafAsync(IGuild guild, IUser user)
        {
            try
            {
                await (user as IGuildUser).ModifyAsync(x => x.Deaf = false);
                Reply($"{user.Mention} a récupéré l'audition :loud_sound:");
            }
            catch
            {
                Reply($"Problème lors de l'utilisation de la commande !deaf {user.Mention}");
            }
        }

        /// <summary>
        /// Méthode de prise en charge de la commande setnick
        /// </summary>
        /// <param name="guild"></param>
        /// <param name="user"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public async Task SetnickAsync(IGuild guild, IUser user, string nickname = null)
        {
            try
            {
                await (user as IGuildUser).ModifyAsync(x => x.Nickname = nickname);
            }
            catch
            {
                Reply($"Problème lors de l'utilisation de la commande !setnick {user.Mention} {nickname}");
            }
        }

        /// <summary>
        /// Méthode de prise en charge de la commande kick
        /// </summary>
        /// <param name="guil"></param>
        /// <param name="user"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public async Task KickAsync(IGuild guild, IUser user, string reason = null)
        {
            try
            {
                await (user as IGuildUser).KickAsync(reason);
            }
            catch
            {
                Reply($"Problème lors de l'utilisation de la commande !kick {user.Mention} {reason}");
            }
        }

        /// <summary>
        /// Méthode de prise en charge de la commande ban
        /// </summary>
        /// <param name="guil"></param>
        /// <param name="user"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public async Task BanAsync(IGuild guild, IUser user, string reason = null)
        {
            try
            {
                await guild.AddBanAsync(user, 0, reason);
            }
            catch
            {
                Reply($"Problème lors de l'utilisation de la commande !ban {user.Mention} {reason}");
            }
        }

        /// <summary>
        /// Méthode de prise en charge de la commande unban
        /// </summary>
        /// <param name="guild"></param>
        /// <param name="user"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public async Task UnbanAsync(IGuild guild, string username)
        {
            var banList = await guild.GetBansAsync();
            IUser currentUser = null;
            foreach (var u in banList)
            {
                if (u.User.Username.Contains(username))
                {
                    currentUser = u.User;
                }
            }

            try
            {
                await guild.RemoveBanAsync(currentUser);
            }
            catch
            {
                Reply($"Problème lors de l'utilisation de la commande !ban {username}");
            }
        }
    }
}
