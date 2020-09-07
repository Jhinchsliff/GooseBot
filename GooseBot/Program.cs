using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace GooseBot
{
    class Program
    {
        private DiscordSocketClient _client;

        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += ConsoleLogger;

            string botToken = Environment.GetEnvironmentVariable("GooseBotToken");

            await _client.LoginAsync(TokenType.Bot, botToken);
            await _client.StartAsync();

            AppDomain.CurrentDomain.ProcessExit += CloseDiscordConnection;
            await Task.Delay(-1);
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
