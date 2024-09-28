using sb.core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sb.shared;
using Newtonsoft.Json;
using System.Text.Json;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.CompilerServices;
using sb.core.interfaces;

namespace sb.core
{
    public static class ConfigService
    {
        private static IFileSystem _fileSystem = new FileSystem();

        public static void SetFileSystem(IFileSystem fileSystem) => _fileSystem = fileSystem;

        public static ConfigModel LoadConfig()
        {
            EnsureDirectoryExists();

            if (_fileSystem.Exists(Constants.configPath))
            {
                string json = _fileSystem.ReadAllText(Constants.configPath);
                return JsonConvert.DeserializeObject<ConfigModel>(json);
            }

            SaveConfig(new ConfigModel());
            return LoadConfig();
        }

        public static void SaveConfig(ConfigModel config)
        {
            EnsureDirectoryExists();

            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            _fileSystem.WriteAllText(Constants.configPath, json);
        }

        public static void UpdateConfig(string setting, string value)
        {
            var config = LoadConfig();

            switch (setting.ToLower())
            {
                case "destination-path":
                    UpdateDestinationPath(value, config);
                    break;
                case "auto-compression":
                    break;
                case "auto-compression-type":
                    break;
                case "schedule":
                    break;
                case "backup-time":
                    break;
                case "scheduler":
                    break;
                case "use-incremental":
                    break;
                case "incremental-compare-method":
                    break;
                case "restore-overwrite-behavior":
                    break;
                case "restore-directory":
                    break;
                case "enable-auto-verification":
                    break;
                case "verification-method":
                    break;
                case "log":
                    break;
                case "log-file-path":
                    break;
                case "retention-length":
                    break;
                case "auto-delete-backups":
                    break;
                case "enable-encryption":
                    break;
                case "encryption-key-location":
                    break;
            }
        }

        public static void UpdateDestinationPath(string value, ConfigModel config)
        {
            if (string.IsNullOrEmpty(value)) return;

            bool isValidPath = false;

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
                SaveConfig(config);
                Console.WriteLine("Destination path updated successfully.");
            }
            else
            {
                Console.WriteLine($"{value} is not a valid path. Please make sure the folder exists.");
            }
        }

        private static void EnsureDirectoryExists()
        {
            string directoryPath = Path.GetDirectoryName(Constants.configPath);
            if (!_fileSystem.Exists(directoryPath))
            {
                _fileSystem.CreateDirectory(directoryPath);
            }
        }

        public static void UpdateAutoCompression(string value)
        {

        }

        public static void UpdateAutoCompressionType(string value)
        {

        }

        public static void UpdateSchedule(string value)
        {

        }

        public static void UpdateBackupTime(string value)
        {

        }
    }
}
