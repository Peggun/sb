using sb.shared.enums;
using System.Runtime.InteropServices;

namespace sb.core.models
{
    public class ConfigModel
    {
        public string DestinationPath { get; set; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "sb", "backups") : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".sb", "backups");
        public bool AutoCompression { get; set; } = true;
        public string AutoCompressionType { get; set; } = Enum.GetName(CompressionTypes.zip);

        public string Schedule { get; set; } = Enum.GetName(ScheduleTimes.Weekly);
        public string BackupTime { get; set; } = "02:00AM";
        public string Scheduler { get; set; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Enum.GetName(Schedulers.taskScheduler) : Enum.GetName(Schedulers.cron);

        public bool UseIncremental { get; set; } = false;
        public string CompareMethod { get; set; } = Enum.GetName(CompareMethods.Timestamp);

        public bool RestoreOverwriteBehavior { get; set; } = true; // true = overwrite existing files
        public string RestoreDirectory { get; set; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "sb", "restored") : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".sb", "restored");

        public bool EnableAutoVerification { get; set; } = false;
        public string VerificationMethod { get; set; } = Enum.GetName(VerificationMethods.Checksum);

        public string LogLevel { get; set; } = Enum.GetName(LogLevels.Info);
        public bool Log { get; set; } = true;
        public string LogFilePath { get; set; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "sb", "logs") : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".sb", "logs");

        public int RetentionPolicy { get; set; } = 30; // Number of days to retain backups
        public bool AutoDeleteOldBackups { get; set; } = true;

        public bool EnableEncryption { get; set; } = false;
        public string EncryptionKeyLocation { get; set; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "sb", "key") : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".sb", "key");
    }
}
