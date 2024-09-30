using Newtonsoft.Json;
using sb.core.interfaces;
using sb.core.models;
using sb.core.settings;
using sb.shared;

namespace sb.core
{
    public class ConfigService : IConfigService
    {
        private readonly IFileSystem _fileSystem;

        public ConfigService(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public ConfigModel LoadConfig()
        {
            EnsureDirectoryExists();
            EnsureConfigFileExists();

            if (_fileSystem.Exists(Constants.configPath))
            {
                string json = _fileSystem.ReadAllText(Constants.configPath);
                if (string.IsNullOrEmpty(json))
                {
                    var config = new ConfigModel();
                    SaveConfig(config);
                    return config;
                }

                try
                {
                    return JsonConvert.DeserializeObject<ConfigModel>(json);
                }
                catch (Exception)
                {
                    var newConfig = new ConfigModel();
                    SaveConfig(newConfig);
                    return newConfig;
                }
            }

            var freshConfig = new ConfigModel();
            SaveConfig(freshConfig);
            return freshConfig;
        }

        public void UpdateConfig(IConfigService configService, string setting, string value)
        {
            switch (setting.ToLower())
            {
                case "destination-path":
                    UpdateDestinationPathSetting.UpdateDestinationPath(configService, value);
                    break;
                case "auto-compression":
                    UpdateAutoCompressionSetting.UpdateAutoCompression(configService, value);
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
                case "encryption-key-location": // NOT AT ALL SECURE. WILL CHANGE WHEN GET TO IT. 
                    break;
                default:
                    Console.WriteLine($"Unknown setting '{setting}'.");
                    break;
            }
        }

        public void SaveConfig(ConfigModel config)
        {
            EnsureDirectoryExists();
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            _fileSystem.WriteAllText(Constants.configPath, json);
        }

        private void EnsureDirectoryExists()
        {
            string directoryPath = Path.GetDirectoryName(Constants.configPath);
            if (!_fileSystem.Exists(directoryPath))
            {
                _fileSystem.CreateDirectory(directoryPath);
            }
        }

        private void EnsureConfigFileExists()
        {
            if (!_fileSystem.Exists(Constants.configPath))
            {
                _fileSystem.CreateFile(Constants.configPath);
            }
        }
    }
}