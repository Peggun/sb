using sb.core.interfaces;
using sb.core.models;
using sb.shared.enums;

namespace sb.core
{
    public class BackupService : IBackupService
    {
        private readonly IConfigService _configService;
        private readonly IFileSystem _fileSystem;
        private readonly ICompressionService _compressionService;
        //private readonly ILogger _logger; // Will add

        public BackupService(IConfigService service, IFileSystem fs, ICompressionService compressionService)
        {
            _configService = service; 
            _fileSystem = fs;
            _compressionService = compressionService;
        }

        public void CreateBackup(string sourcePath, string destinationPath, BackupType backupType, string? folderName)
        {
            try
            {
                if (string.IsNullOrEmpty(folderName))
                {
                    folderName = DateTime.Today.ToString("yyyy-MM-dd");
                }

                // Step 0: Load current config
                var config = _configService.LoadConfig();

                // Step 1: Validate Paths
                if (!_fileSystem.DirExists(sourcePath))
                {
                    throw new Exception($"{sourcePath} doesn't exist. Please make sure it exists or double check the file path provided.");
                }

                if (!_fileSystem.DirExists(destinationPath))
                {
                    _fileSystem.CreateDirectory(destinationPath);
                }

                // Step 2: Create the backup folder (folder where all files will be copied)
                var backupFolder = Path.Combine(destinationPath, folderName);
                if (!_fileSystem.DirExists(backupFolder))
                {
                    _fileSystem.CreateDirectory(backupFolder);
                }

                // Step 3: Get the files to backup.
                var filesToBackup = _fileSystem.GetFiles(sourcePath, "*", SearchOption.AllDirectories);

                // Step 4: Copy files to the backup folder
                foreach (var file in filesToBackup)
                {
                    var relativePath = Path.GetRelativePath(sourcePath, file);
                    var destinationFile = Path.Combine(backupFolder, relativePath);

                    // Ensure the directory structure exists
                    var destinationDir = Path.GetDirectoryName(destinationFile);
                    if (!string.IsNullOrEmpty(destinationDir) && !_fileSystem.DirExists(destinationDir))
                    {
                        _fileSystem.CreateDirectory(destinationDir);
                    }

                    // Copy files to the backup folder
                    _fileSystem.CopyFile(file, destinationFile, overwrite: true);
                }

                // Step 5: Compression (compressing the backup folder)
                if (config.AutoCompression)
                {
                    if (Enum.TryParse(config.AutoCompressionType, true, out CompressionTypes compressionType))
                    {
                        switch (compressionType)
                        {
                            case CompressionTypes.zip:
                                _compressionService.CompressZip(backupFolder, destinationPath, System.IO.Compression.CompressionLevel.Optimal, true);
                                break;
                            case CompressionTypes.gz:
                                _compressionService.CompressGzip(backupFolder, destinationPath);
                                break;
                            case CompressionTypes.lz:
                                _compressionService.CompressLz(filesToBackup, destinationPath, folderName);
                                break;
                            case CompressionTypes.tar:
                                _compressionService.CompressTar(backupFolder, destinationPath);
                                break;
                            case CompressionTypes.sv:
                                break;
                            case CompressionTypes.rar:
                                break;
                            // Add other compression methods here
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

    }
}
