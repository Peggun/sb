using sb.core.interfaces;
using sb.shared;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace sb.core.settings
{
    public static class UpdateRestoreDirectorySetting
    {
        public static void UpdateRestoreDirectory(IConfigService configService, string value)
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
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Regex regex = new Regex(Constants.LinuxRegexPattern);
                isValidPath = regex.IsMatch(value) && (_fileSystem.FileExists(value) || _fileSystem.DirExists(value));
            }
            
            if (isValidPath)
            {
                config.RestoreDirectory = value;
                configService.SaveConfig(config);
                Console.WriteLine($"Restore Directory updated successfully to {value}.");
            }
            else
            {
                Console.WriteLine($"{value} is not a valid path. Please make sure the folder exists and is valid.");
            }
        }
    }
}
