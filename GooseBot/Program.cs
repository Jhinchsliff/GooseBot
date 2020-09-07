using Discord;
using Discord.Net.Rest;
using DiscordBotsList.Api;
using DiscordBotsList.Api.Objects;
using System;
using System.Threading.Tasks;

namespace GooseBot
{
    class Program
    {
        static void Main(string[] args) 
            => new Program().MainAsync().GetAwaiter().GetResult();
        
        public async Task MainAsync()
        {
            DoBotStuff();
        }
        //public static async void DoInDiscordApi()
        //{
        //    IDiscordClient client;
        //    client = Discord.conn
        //}

        public static async void DoBotStuff()
        {
            DiscordBotListApi discordBotList = new DiscordBotListApi();
            try
            {
                var botTask = discordBotList.GetBotAsync(752596003378561046);
                var bot = await botTask;
            }
            catch (Exception ex)
            {

                throw;
            }
            Console.WriteLine("TEST");


        }
    }
}
