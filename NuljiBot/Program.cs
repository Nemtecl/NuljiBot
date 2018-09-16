using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using NuljiBot.Helpers;
using NuljiBot.Services;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace NuljiBot
{
    public class Program
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = InstallServices();

            await InstallCommands();

            await _client.LoginAsync(TokenType.Bot, JsonHelper.GetToken());
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private IServiceProvider InstallServices()
        {
            ServiceCollection services = new ServiceCollection();

            services
                .AddSingleton<GameService>()
                .AddSingleton<AdminService>()
                .AddSingleton<InformationService>()
                .AddSingleton<DofusService>();

            return services.BuildServiceProvider();
        }

        private async Task InstallCommands()
        {
            _client.MessageReceived += MessageReceived;

            _client.Ready += Ready;
            _client.UserJoined += UserJoined;
            _client.UserLeft += UserLeft;
            _client.Log += Log;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task MessageReceived(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            if (!(message.HasCharPrefix(JsonHelper.GetPrefix(), ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos)))
            {
                return;
            }

            var context = new CommandContext(_client, message);

            var result = await _commands.ExecuteAsync(context, argPos, _services);
            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);
        }

        private async Task Ready()
        {
            await _client.SetStatusAsync(UserStatus.DoNotDisturb);
            await _client.SetGameAsync(JsonHelper.GetCurrentGame(), null, ActivityType.Watching);
        }

        private async Task UserJoined(SocketGuildUser user)
        {
            var channel = user.Guild.DefaultChannel;
            var msgList = JsonHelper.GetJoinedMessage();
            await channel.SendMessageAsync(msgList[0] + user.Mention + msgList[1]);
        }

        private async Task UserLeft(SocketGuildUser user)
        {
            var channel = user.Guild.DefaultChannel;
            await channel.SendMessageAsync(user.Mention + JsonHelper.GetLeftMessage());
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
