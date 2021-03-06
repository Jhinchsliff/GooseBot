﻿using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GooseBot.Modules
{
    public class GooseModule : ModuleBase<SocketCommandContext>
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
            SocketUser sender = Context.Message?.Author;

            List<ulong> userIds = new List<ulong>();
            IReadOnlyCollection<SocketGuild> guilds = Context.Client.Guilds;

            // GooseBot is only a user in GooseTown so Goosetown should be the only Guild (server) returned.
            SocketGuild gooseTown = Context.Client.Guilds.FirstOrDefault();
            await gooseTown.DownloadUsersAsync();

            foreach (SocketGuildUser user in gooseTown.Users)
            {
                if (userIds.Contains(user.Id)
                        || user.Id == GooseBotUserId
                        || user.Id == sender?.Id)
                    continue;

                userIds.Add(user.Id);
            }
            
            Random randy = new Random();
            int indexToHonkAt = randy.Next(0, (userIds.Count - 1));
            await ReplyAsync($"<@{userIds[indexToHonkAt]}> https://tenor.com/bgig0.gif");
        }
    }
}
