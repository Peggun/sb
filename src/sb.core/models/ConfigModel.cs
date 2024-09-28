using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using sb.shared;
using sb.shared.enums;

namespace sb.core.models
{
    public class ConfigModel
    {
        public string DestinationPath { get; set; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "sb", "backups") : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".sb", "backups");
        public bool AutoCompression { get; set; } = true;
        public CompressionTypes AutoCompressionType { get; set; } = CompressionTypes.zip;

        public ScheduleTimes Schedule { get; set; } = ScheduleTimes.Weekly;
        public string BackupTime { get; set; } = "02:00AM";
        public Schedulers Scheduler { get; set; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Schedulers.taskScheduler : Schedulers.cron;

        public bool UseIncremental { get; set; } = false;
        public CompareMethod CompareMethod { get; set; } = CompareMethod.Timestamp;

        public bool RestoreOverwriteBehavior { get; set; } = true; // true = overwrite existing files
        public string RestoreDirectory { get; set; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "sb", "restored") : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".sb", "restored");

        public bool EnableAutoVerification { get; set; } = false;
        public VerificationMethod VerificationMethod { get; set; } = VerificationMethod.Checksum;

        public LogLevel LogLevel { get; set; } = LogLevel.Info;
        public bool Log { get; set; } = true;
        public string LogFilePath { get; set; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "sb", "logs") : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".sb", "logs");

        public int RetentionPolicy { get; set; } = 30; // Number of days to retain backups
        public bool AutoDeleteOldBackups { get; set; } = true;

        public bool EnableEncryption { get; set; } = false;
        public string EncryptionKeyLocation { get; set; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "sb", "key") : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".sb", "key");
    }
}
