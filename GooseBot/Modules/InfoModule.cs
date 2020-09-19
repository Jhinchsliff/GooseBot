using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GooseBot.Modules
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        private const ulong GooseBotUserId = 752596003378561046;

        [Command("!say")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder] string echo) => ReplyAsync(echo);

        [Command("!goose")]
        [Summary("Honk!")]
        public async Task GooseAsync()
        {
            await Context.Channel.SendMessageAsync("Honk!");
        }

        [Command("!userInfo")]
        [Summary("Returns info about the current user, or a specific user if passed in")]
        [Alias("user", "whois")]
        public async Task UserInfoAsync(SocketUser user = null)
        {
            user ??= Context.Client.CurrentUser;
            await ReplyAsync($"{user.Username}#{user.Discriminator} - {user.Status}");
        }

        [Command("!honk")]
        [Summary("Honk at a good friend")]
        public async Task HonkAsync()
        {
            List<ulong> userIds = new List<ulong>();
            IReadOnlyCollection<SocketGuild> guilds = Context.Client.Guilds;
            
            foreach (SocketGuild guild in guilds)
            {
                foreach (SocketGuildUser user in guild.Users)
                {
                    if (userIds.Contains(user.Id) || user.Id == GooseBotUserId)
                        continue;

                    userIds.Add(user.Id);
                }
            }

            Random randy = new Random();
            int indexToHonkAt = randy.Next(0, (userIds.Count - 1));
            await ReplyAsync($"<@{userIds[indexToHonkAt]}> https://tenor.com/bgig0.gif");
        }
    }
}
