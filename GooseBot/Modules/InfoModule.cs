using Discord.Commands;
using Discord.WebSocket;
using GooseBot.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GooseBot.Modules
{

    public class InfoModule : ModuleBase<SocketCommandContext>
    {
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
            await ReplyAsync($"<@103325209133592576> Honk!");
        }
    }
}
