using sb.shared.enums;

namespace sb.core.models
{
    public class BackupModel
    {
        public string FolderName { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public DateTime BackupTime { get; set; }
        public bool isCompressed { get; set; }
        public string CompressionType { get; set; }
        public BackupType BackupType { get; set; } // Full, Incremental, Differential
    }
}
