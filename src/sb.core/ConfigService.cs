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

            if (_fileSystem.FileExists(Constants.configPath))
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
                    UpdateAutoCompressionTypeSetting.UpdateAutoCompressionType(configService, value);
                    break;
                case "schedule":
                    
                    break;
                case "backup-time":
                    break;
                case "scheduler":
                    UpdateSchedulerSetting.UpdateScheduler(configService, value);
                    break;
                case "use-incremental":
                    
                    break;
                case "incremental-compare-method":
                    UpdateIncrementalCompareMethodSetting.UpdateIncrementalCompareMethod(configService, value);
                    break;
                case "restore-overwrite-behavior":
                    UpdateRestoreOverwriteBehaviorSetting.UpdateRestoreOverwriteBehavior(configService, value); 
                    break;
                case "restore-directory":
                    UpdateRestoreDirectorySetting.UpdateRestoreDirectory(configService, value);
                    break;
                case "enable-auto-verification":
                    UpdateEnableAutoVerificationSetting.UpdateEnableAutoVerification(configService, value);
                    break;
                case "verification-method":
                    break;
                case "log":
                    UpdateLogSetting.UpdateLog(configService, value);
                    break;
                case "log-file-path":
                    UpdateLogFilePathSetting.UpdateLogFilePath(configService, value);
                    break;
                case "retention-length":
                    UpdateRetentionLengthSetting.UpdateRetentionPolicy(configService, value);
                    break;
                case "auto-delete-backups":
                    UpdateAutoDeleteBackupsSetting.UpdateAutoDeleteBackups(configService, value);
                    break;
                case "enable-encryption":
                    UpdateEnableEncryptionSetting.UpdateEnableEncryption(configService, value);
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
            if (!_fileSystem.DirExists(Constants.sbConfigFolder))
            {
                _fileSystem.CreateDirectory(Constants.sbConfigFolder);
            }
        }

        private void EnsureConfigFileExists()
        {
            if (!_fileSystem.FileExists(Constants.configPath))
            {
                _fileSystem.CreateFile(Constants.configPath);
            }
        }
    }
}