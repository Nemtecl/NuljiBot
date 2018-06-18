using Discord;
using Discord.Commands;
using NuljiBot.Services;
using System.Threading.Tasks;

namespace NuljiBot.Modules
{
    [Name("Admin")]
    [Summary("Contient les commandes d'administration")]
    public sealed class AdminModule : NuljiModule
    {
        private readonly AdminService _service;

        public AdminModule(AdminService service)
        {
            _service = service;
            _service.SetParentModule(this);
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
