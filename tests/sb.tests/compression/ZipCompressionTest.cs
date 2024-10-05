using sb.core.interfaces;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace sb.tests
{
    public class ZipCompressionTest
    {
        [Fact]
        public void CompressZip_CreatesZipWithCorrectFiles()
        {
            // Arrange
            var sourcePath = "test/source";
            var destinationPath = "test/destination";
            var compressionService = new CompressionService();

            // Create fake files for the test
            Directory.CreateDirectory(sourcePath);
            File.WriteAllText(Path.Combine(sourcePath, "file1.txt"), "Test content");
            File.WriteAllText(Path.Combine(sourcePath, "file2.txt"), "More test content");

            // Act
            compressionService.CompressZip(sourcePath, destinationPath, CompressionLevel.Fastest, includeBaseDirectory: false);

            // Assert
            var zipFilePath = Path.Combine(destinationPath, "source.zip");
            Assert.True(File.Exists(zipFilePath));

            using (var zip = ZipFile.OpenRead(zipFilePath))
            {
                Assert.Equal(2, zip.Entries.Count); // We expect two files
                Assert.Contains(zip.Entries, e => e.FullName == "file1.txt");
                Assert.Contains(zip.Entries, e => e.FullName == "file2.txt");
            }

            // Cleanup
            Directory.Delete(sourcePath, true);
            Directory.Delete(destinationPath, true);
        }

        [Fact]
        public void CompressZip_EmptySourceDirectory_CreatesEmptyZip()
        {
            // Arrange
            var sourcePath = "test/source";
            var destinationPath = "test/destination";
            var compressionService = new CompressionService();

            Directory.CreateDirectory(sourcePath);  // Empty source directory

            // Act
            compressionService.CompressZip(sourcePath, destinationPath, CompressionLevel.Fastest, includeBaseDirectory: false);

            // Assert
            var zipFilePath = Path.Combine(destinationPath, "source.zip");
            Assert.True(File.Exists(zipFilePath));

            using (var zip = ZipFile.OpenRead(zipFilePath))
            {
                Assert.Empty(zip.Entries);  // No entries since source is empty
            }

            // Cleanup
            Directory.Delete(sourcePath, true);
            Directory.Delete(destinationPath, true);
        }

        [Fact]
        public void CompressZip_SourceDirectoryDoesNotExist_ThrowsException()
        {
            // Arrange
            var sourcePath = "test/nonexistent";
            var destinationPath = "test/destination";
            var compressionService = new CompressionService();

            // Act & Assert
            Assert.Throws<DirectoryNotFoundException>(() =>
                compressionService.CompressZip(sourcePath, destinationPath, CompressionLevel.Fastest, includeBaseDirectory: false));
        }

        [Fact]
        public void CompressZip_DestinationPathDoesNotExist_CreatesZipInNewDirectory()
        {
            // Arrange
            var sourcePath = "test/source";
            var destinationPath = "test/destination";
            var compressionService = new CompressionService();

            Directory.CreateDirectory(sourcePath); // Create source but no destination

            // Act
            compressionService.CompressZip(sourcePath, destinationPath, CompressionLevel.Fastest, includeBaseDirectory: false);

            // Assert
            var zipFilePath = Path.Combine(destinationPath, "source.zip");
            Assert.True(File.Exists(zipFilePath));

            // Cleanup
            Directory.Delete(sourcePath, true);
            Directory.Delete(destinationPath, true);
        }

    }
}
