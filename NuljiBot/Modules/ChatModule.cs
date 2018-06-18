using Discord;
using Discord.Commands;
using NuljiBot.Services;
using System.Threading.Tasks;

namespace NuljiBot.Modules
{
    [Name("Chat")]
    [Summary("Module de chat")]
    public sealed class ChatModule : NuljiModule
    {
        private readonly ChatService _service;

        public ChatModule(ChatService service)
        {
            _service = service;
            _service.SetParentModule(this);
        }

        [Command("Flipcoin")]
        [Remarks("!flipcoin")]
        [Summary("Pile ou face")]
        public async Task FlipCoin()
        {
            _service.FlipCoinAsync(Context.Channel, Context.User);
            await Task.Delay(0);
        }

        [Command("Roll")]
        [Remarks("!roll [n]")]
        [Summary("Lancer de 1 ou plusieurs dés")]
        public async Task Roll([Remainder] int n = 0)
        {
            _service.RollAsync(Context.Channel, Context.User, n);
            await Task.Delay(0);
        }

        [Command("Rps")]
        [Remarks("!rps [choice]")]
        [Summary("Pierre Feuille Ciseaux")]
        public async Task Rps([Remainder] string choice = null)
        {
            _service.RpsAsync(Context.Channel, Context.User, choice);
            await Task.Delay(0);
        }

        [Command("Clear")]
        [Remarks("!clear [n]")]
        [Summary("Supprime [n] messages du channel courant")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task ClearMessages([Remainder] int n = 0)
        {
            await _service.ClearMessagesAsync(Context.Guild, Context.Channel, Context.User, n);
        }

    }
}
