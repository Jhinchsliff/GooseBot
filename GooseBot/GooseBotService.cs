using Discord;
using Discord.Commands;
using Discord.WebSocket;
using GooseBot.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GooseBot
{
    public class GooseBotService
    {
        private DiscordSocketClient _client;
        private CommandService _commands;

        public async void Start()
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
        }
        public async void Stop()
        {
            await _client.StopAsync();
            await _client.LogoutAsync();
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

        private Task ConsoleLogger(LogMessage message)
        {
            Console.WriteLine(message.Message);
            return Task.CompletedTask;
        }
    }
}
