using sb.core.interfaces;
using sb.shared;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace sb.core.settings
{
    public static class UpdateLogFilePathSetting
    {
        public static void UpdateLogFilePath(IConfigService configService, string value)
        {
            IFileSystem _fileSystem = new FileSystem();

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
                isValidPath = regex.IsMatch(value) && (_fileSystem.FileExists(value) || _fileSystem.DirExists(value));
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Regex regex = new Regex(Constants.LinuxRegexPattern);
                isValidPath = regex.IsMatch(value) && (_fileSystem.FileExists(value) || _fileSystem.DirExists(value));
            }

            if (isValidPath)
            {
                config.LogFilePath = value;
                configService.SaveConfig(config);
                Console.WriteLine($"Log File Path updated successfully to {value}.");
            }
            else
            {
                Console.WriteLine($"{value} is not a valid path. Please make sure the folder exists and is valid.");
            }
        }
    }
}
