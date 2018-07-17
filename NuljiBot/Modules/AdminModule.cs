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
            _service.ClearMessagesAsync(Context.Guild, Context.Channel, Context.User, n);
            await Task.Delay(0);
        }

        [Command("Serverinvite")]
        [Remarks("!serverinvite")]
        [Summary("Renvoie un lien d'invitation")]
        [RequireBotPermission(GuildPermission.ManageGuild)]
        public async Task Serverinvite()
        {
            _service.ServerinviteAsync(Context.Guild, Context.User);
            await Task.Delay(0);
        }

        [Command("mute")]
        [Remarks("!mute [user]")]
        [Summary("Met sous sourdine un utilisateur")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        public async Task Mute([Remainder] IGuildUser user)
        {
            _service.MuteAsync(user);
            await Task.Delay(0);
        }

        [Command("unmute")]
        [Remarks("!unmute [user]")]
        [Summary("Retire la sourdine d'un utilisateur")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        public async Task Unmute([Remainder] IGuildUser user)
        {
            _service.UnmuteAsync(user);
            await Task.Delay(0);
        }
    }
}
