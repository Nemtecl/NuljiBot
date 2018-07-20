using Discord.Commands;
using NuljiBot.Services;
using System.Threading.Tasks;

namespace NuljiBot.Modules
{
    [Name("Game")]
    [Summary("Contient les commandes de mini-jeu")]
    public sealed class GameModule : NuljiModule
    {
        private readonly GameService _service;

        public GameModule(GameService service)
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
            await Task.CompletedTask;
        }

        [Command("Roll")]
        [Remarks("!roll [n]")]
        [Summary("Lancer de 1 ou plusieurs dés")]
        public async Task Roll([Remainder] int n = 0)
        {
            _service.RollAsync(Context.Channel, Context.User, n);
            await Task.CompletedTask;
        }

        [Command("Rps")]
        [Remarks("!rps [choice]")]
        [Summary("Pierre Feuille Ciseaux")]
        public async Task Rps([Remainder] string choice = null)
        {
            _service.RpsAsync(Context.Channel, Context.User, choice);
            await Task.CompletedTask;
        }
    }
}
