using sb.core.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sb.tests
{
    public class LZMACompressionTest
    {
        [Fact]
        public void CompressLZMA_CreatesLZMAArchive()
        {
            // Arrange
            var files = new[] { "file1.txt", "file2.txt" };
            var destinationPath = "test/destination";
            var compressionService = new CompressionService();

            Directory.CreateDirectory(destinationPath);
            foreach (var file in files)
            {
                File.WriteAllText(Path.Combine(destinationPath, file), "Test content");
            }

            var outputFile = Path.Combine(destinationPath, "archive.lzma");

            // Act
            compressionService.CompressLZMA(files, destinationPath, outputFile);

            // Assert
            Assert.True(File.Exists(outputFile));

            // Cleanup
            Directory.Delete(destinationPath, true);
        }

        [Fact]
        public void CompressLZMA_EmptyFilesArray_DoesNothing()
        {
            // Arrange
            var files = new string[] { };  // Empty array
            var destinationPath = "test/destination";
            var compressionService = new CompressionService();

            Directory.CreateDirectory(destinationPath);  // Ensure destination exists

            // Act
            compressionService.CompressLZMA(files, destinationPath, null);

            // Assert
            var lzmaFile = Path.Combine(destinationPath, "archive.lzma");
            Assert.False(File.Exists(lzmaFile));  // No archive created

            // Cleanup
            Directory.Delete(destinationPath, true);
        }

        [Fact]
        public void CompressLZMA_InvalidFilePath_ThrowsException()
        {
            // Arrange
            var files = new[] { "invalid/path/file.txt" };
            var destinationPath = "test/destination";
            var compressionService = new CompressionService();

            Directory.CreateDirectory(destinationPath);

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() =>
                compressionService.CompressLZMA(files, destinationPath, null));
        }

        [Fact]
        public void CompressLZMA_InvalidOutputFileName_ThrowsException()
        {
            // Arrange
            var files = new[] { "file1.txt" };
            var destinationPath = "test/destination";
            var invalidOutputFileName = "";  // Invalid output file name
            var compressionService = new CompressionService();

            Directory.CreateDirectory(destinationPath);

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                compressionService.CompressLZMA(files, destinationPath, invalidOutputFileName));

            // Cleanup
            Directory.Delete(destinationPath, true);
        }

    }
}
