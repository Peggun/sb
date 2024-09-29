using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.NamingConventionBinder;
using sb.core;

namespace sb.cli;
class Program
{
    static async Task<int> Main(string[] args)
    {
        var configCommand = new Command("config", "Configure sb backup tool")
        {
            new Argument<string>("setting", "The configuration setting to update"),
            new Argument<string>("value", "The value to set for the configuration")
        };

        configCommand.Handler = CommandHandler.Create<string, string>((setting, value) =>
        {
            if (!string.IsNullOrEmpty(setting) && !string.IsNullOrEmpty(value))
            {
                ConfigService.UpdateConfig(setting, value);
            }
            else
            {
                Console.WriteLine("Please provide both a setting and a value.");
            }
        });

        var rootCommand = new RootCommand
        {
            configCommand
        };

        rootCommand.Description = "sb Backup Tool CLI";

        return await rootCommand.InvokeAsync(args);
    }
}
