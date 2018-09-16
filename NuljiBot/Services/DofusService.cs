using Discord;
using NuljiBot.Helpers;
using NuljiBot.Helpers.SharedClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace NuljiBot.Services
{
    /// <summary>
    /// Objet de prise en charge dees commandes dofus
    /// </summary>
    public sealed class DofusService : NuljiService
    {
        /// <summary>
        /// Méthode de prise en charge de la commande almanax
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="user"></param>
        /// <param name="date"></param>
        public async void AlmanaxAsync(IMessageChannel channel, IUser user, string date = null)
        {

            if (date == null)
            {
                date = DateTime.Today.Day.ToString("D2") + "/" + DateTime.Today.Month.ToString("D2");
            }

            Almanax almanax = JsonHelper.GetAlmanax(date);
            var builder = EmbedBuilderHelper.EmbedBuilderInformation(user)
                .WithThumbnailUrl("https://almanax.ordre2vlad.fr/images/items/" + almanax.ItemImage + ".png")
                .WithTitle("Almanax command")
                .AddField(almanax.Quest.Split(" : ")[0], almanax.Quest.Split(" : ")[1])
                .AddField(almanax.Type, almanax.Effect)
                .AddField("Offrande", almanax.Offering);
            Reply("", builder);
        }
    }
}
