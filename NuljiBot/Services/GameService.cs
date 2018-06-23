using Discord;
using NuljiBot.Helpers;
using System;
using System.Globalization;

namespace NuljiBot.Services
{
    /// <summary>
    /// Objet de prise en charge des commandes de mini-jeu
    /// </summary>
    public sealed class GameService : NuljiService
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

                var builder = EmbedBuilderHelper.EmbedBuilderInformation(user)
                    .WithTitle("Roll command")
                    .WithDescription("Lancer de [n] dés")
                    .AddField("Usage", "!roll [n]");

                Reply("", builder);
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

            var builder = EmbedBuilderHelper.EmbedBuilderInformation(user)
                .WithTitle("Rps command")
                .WithDescription("Pierre Feuille Ciseaux")
                .AddField("Usage", "!rps [choice]")
                .AddField("Parameters", "Pierre :punch:\nFeuille :hand_splayed:\nCiseaux :v:");

            // Vérification des paramètres
            if (userChoice == null)
            {
                Reply("", builder);
                return;
            }

            bool exists = false;

            foreach (var choice in RpsHelper.PossibleValues)
            {
                if (choice.ToLower().Equals(userChoice.ToLower()))
                {
                    exists = true;
                }
            }

            if (!exists)
            {
                Reply("", builder);
                return;
            }

            // Comportement
            Random rnd = new Random();
            string botChoice = RpsHelper.PossibleValues[rnd.Next(0, 3)];

            if (botChoice.ToLower().Equals(userChoice.ToLower()))
            {
                Reply($"{user.Mention} a choisi ***{botChoice}***, je choisi ***{botChoice}***\nEgalité !");
                return;
            }

            string sChoice = "";
            if (userChoice.ToLower().Equals(RpsHelper.Rock))
            {
                sChoice += RpsHelper.ChoiceChecker[RpsHelper.Rock](user, botChoice);
            }
            else if (userChoice.ToLower().Equals(RpsHelper.Paper))
            {
                sChoice += RpsHelper.ChoiceChecker[RpsHelper.Paper](user, botChoice);
            }
            else
            {
                sChoice += RpsHelper.ChoiceChecker[RpsHelper.Scissors](user, botChoice);
            }
            Reply(sChoice);
        }
    }
}
