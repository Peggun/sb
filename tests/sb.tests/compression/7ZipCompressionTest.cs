using sb.core.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sb.tests
{
    public class _7ZipCompressionTest
    {
        [Fact]
        public void Compress7Zip_Creates7ZipArchive()
        {
            // Arrange
            var sourceFolder = "test/source";
            var destinationFolder = "test/destination";
            var compressionService = new CompressionService();

            Directory.CreateDirectory(sourceFolder);
            File.WriteAllText(Path.Combine(sourceFolder, "file1.txt"), "Test content");

            // Act
            compressionService.Compress7Zip(sourceFolder, destinationFolder);

            // Assert
            var archivePath = Path.Combine(destinationFolder, "backup.7z");
            Assert.True(File.Exists(archivePath));

            // Optionally, you can also extract and verify contents if you want.

            // Cleanup
            Directory.Delete(sourceFolder, true);
            Directory.Delete(destinationFolder, true);
        }

        [Fact]
        public void Compress7Zip_EmptySourceFolder_CreatesEmpty7z()
        {
            // Arrange
            var sourceFolder = "test/source";
            var destinationFolder = "test/destination";
            var compressionService = new CompressionService();

            Directory.CreateDirectory(sourceFolder);  // Empty source folder

            // Act
            compressionService.Compress7Zip(sourceFolder, destinationFolder);

            // Assert
            var archivePath = Path.Combine(destinationFolder, "backup.7z");
            Assert.True(File.Exists(archivePath));

            // You can also verify the archive contains no files

            // Cleanup
            Directory.Delete(sourceFolder, true);
            Directory.Delete(destinationFolder, true);
        }

        [Fact]
        public void Compress7Zip_SourceFolderDoesNotExist_ThrowsException()
        {
            // Arrange
            var sourceFolder = "test/nonexistent";
            var destinationFolder = "test/destination";
            var compressionService = new CompressionService();

            // Act & Assert
            Assert.Throws<DirectoryNotFoundException>(() =>
                compressionService.Compress7Zip(sourceFolder, destinationFolder));
        }

        [Fact]
        public void Compress7Zip_DestinationFolderDoesNotExist_CreatesFolderAndArchive()
        {
            // Arrange
            var sourceFolder = "test/source";
            var destinationFolder = "test/destination";
            var compressionService = new CompressionService();

            Directory.CreateDirectory(sourceFolder);  // Ensure source exists but destination does not

            // Act
            compressionService.Compress7Zip(sourceFolder, destinationFolder);

            // Assert
            var archivePath = Path.Combine(destinationFolder, "backup.7z");
            Assert.True(File.Exists(archivePath));

            // Cleanup
            Directory.Delete(sourceFolder, true);
            Directory.Delete(destinationFolder, true);
        }

    }
}
