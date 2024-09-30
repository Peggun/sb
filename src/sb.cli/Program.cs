using sb.core;
using sb.core.interfaces;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace sb.cli;

class Program
{
    static async Task<int> Main(string[] args)
    {
        // Instantiate the file system and config service
        IFileSystem fileSystem = new FileSystem();
        IConfigService configService = new ConfigService(fileSystem);

        // Define the config command
        var configCommand = new Command("config", "Configure sb backup tool")
        {
            new Argument<string>("setting", "The configuration setting to update"),
            new Argument<string>("value", "The value to set for the configuration")
        };

        // Set up the handler for the config command
        configCommand.Handler = CommandHandler.Create<string, string>((setting, value) =>
        {
            if (!string.IsNullOrEmpty(setting) && !string.IsNullOrEmpty(value))
            {
                try
                {
                    // Use the instance of ConfigService (not static anymore)
                    configService.UpdateConfig(configService, setting, value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating config: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Please provide both a setting and a value.");
            }
        });

        // Define the root command
        var rootCommand = new RootCommand
        {
            configCommand
        };

        rootCommand.Description = "sb Backup Tool CLI";

        // Invoke the command handler
        return await rootCommand.InvokeAsync(args);
    }
}