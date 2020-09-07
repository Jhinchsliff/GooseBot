using System;
using Topshelf;

namespace GooseBot
{
    class Program
    {
        static void Main(string[] args)
        {
            TopshelfExitCode exitCode = HostFactory.Run(x =>
            {
                x.Service<GooseBotService>(s =>
                {
                    s.ConstructUsing(gooseBot => new GooseBotService());
                    s.WhenStarted(gooseBot => gooseBot.Start());
                    s.WhenStopped(gooseBot => gooseBot.Stop());
                });

                x.RunAsLocalSystem();
                x.SetServiceName("GooseBot");
                x.SetDisplayName("Goose Bot");
                x.SetDescription("My first Discord bot. Honk!");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
