using Discord.Commands;
using NuljiBot.Services;
using System.Threading.Tasks;

namespace NuljiBot.Modules
{
    public sealed class InformationModule : NuljiModule
    {
        private readonly InformationService _service;

        public InformationModule(InformationService service)
        {
            _service = service;
            _service.SetParentModule(this);
        }

        [Command("Help")]
        [Remarks("!help")]
        [Summary("Commande d'aide")]
        public async Task Help()
        {
            _service.HelpAsync(Context.Channel, Context.User);
            await Task.Delay(0);
        }

        [Command("Uptime")]
        [Remarks("!uptime")]
        [Summary("Retourne le temps d'exécution du bot")]
        public async Task Uptime()
        {
            _service.UptimeAsync(Context.Channel, Context.User);
            await Task.Delay(0);
        }

        [Command("Membercount")]
        [Remarks("!membercount")]
        [Summary("Retourne le nombre de membre du serveur")]
        public async Task Membercount()
        {
            _service.MembercountAsync(Context.Guild, Context.User);
            await Task.Delay(0);
        }
    }
}
