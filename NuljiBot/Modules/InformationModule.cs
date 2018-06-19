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
            _service.Help(Context.Channel, Context.User);
            await Task.Delay(0);
        }
    }
}
