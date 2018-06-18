using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace NuljiBot.Services
{
    /// <summary>
    /// Objet de prise en charge des commandes de chat
    /// </summary>
    public sealed class ChatService : NuljiService
    {
        /// <summary>
        /// Méthode de prise en charge de la commande flipcoin
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="user"></param>
        public void FlipCoinAsync(IMessageChannel channel, IUser user)
        {
            Random rnd = new Random();
            int result = rnd.Next(1, 3);
            string message = result == 1 ? "Pile" : "Face";

            Reply($"{user.Mention} {message}");
        }

        /// <summary>
        /// Méthode de prise en charge de la commande roll
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="user"></param>
        /// <param name="n"></param>
        public void RollAsync(IMessageChannel channel, IUser user, int n)
        {
            // Vérification des paramètres
            if (n == 0)
            {
                var eb = new EmbedBuilder();
                eb.WithTitle("Roll command");
                eb.WithDescription("Lancer de [n] dés");
                eb.AddField("Usage", "!roll [n]");
                Reply("", eb);
                return;
            }

            Random rnd = new Random();
            string result = $"{rnd.Next(1, 7)}";

            for (int i = 1; i < n; i++)
            {
                result += $", {rnd.Next(1, 7)}";
            }
            Reply($"{user.Mention} {result}");
        }

        /// <summary>
        /// Méthode de prise en charge de la commande rps
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="user"></param>
        /// <param name="userChoice"></param>
        public void RpsAsync(IMessageChannel channel, IUser user, string userChoice)
        {

            var eb = new EmbedBuilder();
            eb.WithTitle("Rps command");
            eb.WithDescription("Pierre Feuille Ciseaux");
            eb.AddField("Usage", "!rps [choice]");
            eb.AddField("Parameters", "Pierre :punch:\nFeuille :hand_splayed:\nCiseaux :v:");

            // Vérification des paramètres
            if (userChoice == null)
            {
                Reply("", eb);
                return;
            }

            string[] rps = new string[] { "Pierre", "Feuille", "Ciseaux" };
            bool exists = false;

            foreach (var choice in rps)
            {
                if (choice.ToLower().Equals(userChoice.ToLower()))
                {
                    exists = true;
                }
            }

            if (!exists)
            {
                Reply("", eb);
                return;
            }

            // Comportement
            Random rnd = new Random();
            string botChoice = rps[rnd.Next(0, 3)];

            if (botChoice.ToLower().Equals(userChoice.ToLower()))
            {
                Reply($"{user.Mention} a choisi ***{botChoice}***, je choisi ***{botChoice}***\nEgalité !");
                return;
            }

            string sChoice = "";
            if (userChoice.ToLower().Equals(rps[0].ToLower()))
            {
                sChoice += $"{user.Mention} a choisi ***{rps[0]}***, je choisi ***{botChoice}***";
                if (botChoice.ToLower().Equals(rps[1].ToLower()))
                    sChoice += $"\nJ'ai gagné ! :hand_splayed:";
                else
                    sChoice += $"\nTu as gagné ! :punch:";
            }
            else if (userChoice.ToLower().Equals(rps[1].ToLower()))
            {
                sChoice += $"{user.Mention} a choisi ***{rps[1]}***, je choisi ***{botChoice}***";
                if (botChoice.ToLower().Equals(rps[2].ToLower()))
                    sChoice += $"\nJ'ai gagné ! :v:";
                else
                    sChoice += $"\nTu as gagné ! :hand_splayed:";
            }
            else
            {
                sChoice += $"{user.Mention} a choisi ***{rps[2]}***, je choisi ***{botChoice}***";
                if (botChoice.ToLower().Equals(rps[0].ToLower()))
                    sChoice += $"\nJ'ai gagné ! :punch:";
                else
                    sChoice += $"\nTu as gagné ! :v:";
            }
            Reply(sChoice);
        }

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
            eb.WithTitle("Clear command");
            eb.WithDescription("Supprime [n] messages du channel courrant");
            eb.AddField("Usage", "!clear [n]");
            eb.AddField("Require", "Permission de gestion des messages");

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
            var messages = await channel.GetMessagesAsync(n).FlattenAsync();
            await (channel as SocketTextChannel).DeleteMessagesAsync(messages);

            Reply($"{user.Mention} Suppression des messages ...");
        }
    }
}
