using sb.core.interfaces;
using sb.shared;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace sb.core.settings
{
    public static class UpdateDestinationPathSetting
    {
        public static void UpdateDestinationPath(IConfigService configService, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("Please add a value for this setting.");
                return;
            }

            bool isValidPath = false;
            var config = configService.LoadConfig();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Regex regex = new Regex(Constants.WindowsRegexPattern);
                isValidPath = regex.IsMatch(value) && (File.Exists(value) || Directory.Exists(value));
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Regex regex = new Regex(Constants.LinuxRegexPattern);
                isValidPath = regex.IsMatch(value) && (File.Exists(value) || Directory.Exists(value));
            }

            if (isValidPath)
            {
                config.DestinationPath = value;
                configService.SaveConfig(config);
                Console.WriteLine($"Destination path updated successfully to {value}.");
            }
            else
            {
                Console.WriteLine($"{value} is not a valid path. Please make sure the folder exists and is valid.");
            }
        }
    }
}