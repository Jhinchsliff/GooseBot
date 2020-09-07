using Discord;
using Discord.Commands;
using Discord.WebSocket;
using GooseBot.Extensions;
using GooseBot.Modules;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace GooseBot
{
    class Program
    {
        private DiscordSocketClient _client;
        private CommandService _commands;

        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += ConsoleLogger;

            string botToken = Environment.GetEnvironmentVariable("GooseBotToken");

            if (botToken.IsNullOrWhiteSpace())
            {
                throw new Exception("Token not found. Ensure a 'GooseBotToken' Environment Variable exists.");
            }

            await _client.LoginAsync(TokenType.Bot, botToken);
            await _client.StartAsync();
            
            _commands = new CommandService();

            _client.MessageReceived += HandleBotCommandAsync;
            AppDomain.CurrentDomain.ProcessExit += CloseDiscordConnection;
            await Task.Delay(-1);
        }

        private async Task HandleBotCommandAsync(SocketMessage messageParam)
        {
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);

            int firstChar = 0;
            SocketUserMessage message = messageParam as SocketUserMessage;

            if (message == null || !message.HasCharPrefix('!', ref firstChar))
                return;

            var context = new SocketCommandContext(_client, message);

            IResult result = await _commands.ExecuteAsync(
                context: context,
                argPos: 0,
                services: null);

            if (!result.IsSuccess)
            {
                Console.WriteLine(result.Error);
                Console.WriteLine(result.ErrorReason);
            }
                
        }

        private async void CloseDiscordConnection(object sender, EventArgs e)
        {
            await _client.StopAsync();
            await _client.LogoutAsync();
        }

        private Task ConsoleLogger(LogMessage message)
        {
            Console.WriteLine(message.Message);
            return Task.CompletedTask;
        }
    }
}
