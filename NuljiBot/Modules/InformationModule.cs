using Discord.Commands;
using NuljiBot.Services;
using System;
using System.Threading.Tasks;

namespace NuljiBot.Modules
{
    public sealed class InformationModule : NuljiModule
    {
        private readonly InformationService _service;
        private readonly CommandService _commands;
        private readonly IServiceProvider _provider;

        public InformationModule(InformationService service, CommandService commands, IServiceProvider provider)
        {
            _service = service;
            _service.SetParentModule(this);
            _commands = commands;
            _provider = provider;
        }

        [Command("Uptime")]
        [Remarks("!uptime")]
        [Summary("Retourne le temps d'exécution du bot")]
        public async Task Uptime()
        {
            _service.UptimeAsync(Context.Channel, Context.User);
            await Task.CompletedTask;
        }

        [Command("Membercount")]
        [Remarks("!membercount")]
        [Summary("Retourne le nombre de membre du serveur")]
        public async Task Membercount()
        {
            _service.MembercountAsync(Context.Guild, Context.User);
            await Task.CompletedTask;
        }

        [Command("Serverinfo")]
        [Remarks("!serverinfo")]
        [Summary("Retourne plusieurs informations sur le serveur")]
        public async Task Serverinfo()
        {
            _service.ServerinfoAsync(Context.Guild);
            await Task.CompletedTask;
        }

        [Command("Whois")]
        [Remarks("!whois [username]")]
        [Summary("Retourne plusieurs information sur un utilisateur")]
        public async Task Whois([Remainder] string username = null)
        {
            _service.WhoisAsync(Context.Guild, Context.User, username);
            await Task.CompletedTask;
        }

        [Command("help")]
        [Remarks("!help")]
        [Summary("Retourne des informations sur l'ensemble des commandes existantes")]
        public async Task Help()
        {
            _service.HelpAsync(_commands, _provider, Context.Guild, Context.User);
            await Task.CompletedTask;
        }
    }
}
