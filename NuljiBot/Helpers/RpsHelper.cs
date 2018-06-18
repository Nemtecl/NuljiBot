using Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace NuljiBot.Helpers
{
    public static class RpsHelper
    {
        public static string[] PossibleValues = new[]
        {
            "Pierre",
            "Feuille",
            "Ciseaux"
        };

        public static Dictionary<string, Func<IUser, string, string>> ChoiceChecker = new Dictionary<string, Func<IUser, string, string>>
        {
            { Rock, RockResult },
            { Paper, PaperResult },
            { Scissors, ScissorsResult }
        };

        public static string Rock { get => PossibleValues[0].ToLower(); }

        public static string Paper { get => PossibleValues[1].ToLower(); }

        public static string Scissors { get => PossibleValues[2].ToLower(); }


        public static string RockResult(IUser user, string botChoice)
        {

            string result = $"{user.Mention} a choisi ***{PossibleValues[0]}***, je choisi ***{botChoice}***";
            if (botChoice.ToLower().Equals(PossibleValues[1].ToLower()))
                result += $"\nJ'ai gagné ! :hand_splayed:";
            else
                result += $"\nTu as gagné ! :punch:";
            return result;
        }

        private static string PaperResult(IUser user, string botChoice)
        {
            string result = $"{user.Mention} a choisi ***{PossibleValues[1]}***, je choisi ***{botChoice}***";
            if (botChoice.ToLower().Equals(PossibleValues[2].ToLower()))
                result += $"\nJ'ai gagné ! :v:";
            else
                result += $"\nTu as gagné ! :hand_splayed:";
            return result;
        }

        private static string ScissorsResult(IUser user, string botChoice)
        {
            string result = $"{user.Mention} a choisi ***{PossibleValues[2]}***, je choisi ***{botChoice}***";
            if (botChoice.ToLower().Equals(PossibleValues[0].ToLower()))
                result += $"\nJ'ai gagné ! :punch:";
            else
                result += $"\nTu as gagné ! :v:";
            return result;
        }
    }
}
