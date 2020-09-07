using System;
using System.Collections.Generic;
using System.Text;

namespace GooseBot.Extensions
{
    public static class StringExtensions
    {
        private const char BotCommandPrefix = '!';

        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        public static bool IsBotCommand(this string input)
        {
            if (input.IsNullOrWhiteSpace())
                return false;

            return input[0] == BotCommandPrefix;
        }
    }
}
