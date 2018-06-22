using Discord;
using Discord.WebSocket;
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
        public async Task ClearMessagesAsync(IGuild guild, IMessageChannel channel, IUser user, int n)
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
    }
}
