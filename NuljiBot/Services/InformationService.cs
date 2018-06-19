using Discord;

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
        public void Help(IMessageChannel channel, IUser user)
        {
            Reply($"{user.Mention} Not implemented yet ! :middle_finger: ");
        }
    }
}
