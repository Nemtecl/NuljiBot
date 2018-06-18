using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace NuljiBot.Modules
{
    /// <summary>
    /// Classe de base qui hérite de ModuleBase
    /// </summary>
    public class NuljiModule : ModuleBase
    {
        public async Task ServiceReplyAsync(string reply)
        {
            await ReplyAsync(reply);
        }

        public async Task ServiceReplyAsync(string title, EmbedBuilder eb)
        {
            await ReplyAsync(title, false, eb.Build());
        }
    }
}
