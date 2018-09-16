using Discord.Commands;
using NuljiBot.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NuljiBot.Modules
{
    [Name("Dofus")]
    [Summary("Contient les commandes liées à Dofus")]
    public sealed class DofusModule : NuljiModule
    {
        private readonly DofusService _service;

        public DofusModule(DofusService service)
        {
            _service = service;
            _service.SetParentModule(this);
        }

        [Command("Almanax")]
        [Remarks("!almanax [date]")]
        [Summary("Récupère des informations sur la quête almanax du jour indiqué")]
        public async Task Almanax([Remainder] string date = null)
        {
            _service.AlmanaxAsync(Context.Channel, Context.User, date);
            await Task.CompletedTask;
        }
    }
}
