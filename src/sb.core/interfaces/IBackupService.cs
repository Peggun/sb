using sb.shared.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sb.core.interfaces
{
    public interface IBackupService
    {
        void CreateBackup(string sourcePath, string destinationPath, BackupType backupType, string? folderName);
    }
}
