﻿using Discord;
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
            await Task.CompletedTask;
        }

        [Command("Serverinvite")]
        [Remarks("!serverinvite")]
        [Summary("Renvoie un lien d'invitation")]
        [RequireBotPermission(GuildPermission.ManageGuild)]
        public async Task Serverinvite()
        {
            _service.ServerinviteAsync(Context.Guild, Context.User);
            await Task.CompletedTask;
        }

        [Command("mute")]
        [Remarks("!mute [user]")]
        [Summary("Rend un utilisateur muet")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        public async Task Mute([Remainder] IUser user)
        {
            await _service.MuteAsync(Context.Guild, user);
        }

        [Command("unmute")]
        [Remarks("!unmute [user]")]
        [Summary("Rend la parole à un utilisateur")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        public async Task Unmute([Remainder] IUser user)
        {
            await _service.UnmuteAsync(Context.Guild, user);
        }

        [Command("deaf")]
        [Remarks("!deaf [user]")]
        [Summary("Rend un utilisateur sourd")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.DeafenMembers)]
        public async Task Deaf([Remainder] IUser user)
        {
            await _service.DeafAsync(Context.Guild, user);
        }

        [Command("undeaf")]
        [Remarks("!undeaf [user]")]
        [Summary("Rend l'ouïe à un utilisateur")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.DeafenMembers)]
        public async Task Undeaf([Remainder] IUser user)
        {
            await _service.UndeafAsync(Context.Guild, user);
        }

        [Command("setnick")]
        [Remarks("!setnick [user] [nickname]")]
        [Summary("Renomme un utilisateur")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.ChangeNickname)]
        public async Task Setnick(IUser user, [Remainder] string nickname = null)
        {
            await _service.SetnickAsync(Context.Guild, user, nickname);
        }

        [Command("Kick")]
        [Remarks("!kick [user] [reason]")]
        [Summary("Kick un utilisateur du serveur")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Kick(IUser user, [Remainder] string reason = null)
        {
            await _service.KickAsync(Context.Guild, user, reason);
        }

        [Command("Ban")]
        [Remarks("!ban [user] [reason]")]
        [Summary("Ban un utilisateur du serveur")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Ban(IUser user, [Remainder] string reason = null)
        {
            await _service.BanAsync(Context.Guild, user, reason);
        }

        [Command("Unban")]
        [Remarks("!unban [username] [reason]")]
        [Summary("Déban un utilisateur du serveur")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Unban([Remainder] string username)
        {
            await _service.UnbanAsync(Context.Guild, username);
        }
    }
}
