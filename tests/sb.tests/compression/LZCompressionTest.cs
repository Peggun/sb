using Moq;
using sb.core.compressors.lz77.algorithms;
using sb.core.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sb.tests
{
    public class LZCompressionTest
    {
        [Fact]
        public void CompressLz_CallsLz77Compressor()
        {
            // Arrange
            var mockCompressor = new Mock<Lz77Compressor>();
            var files = new[] { "file1.txt", "file2.txt" };
            var destinationPath = "test/destination";
            var compressionService = new CompressionService();

            // Create fake files for testing
            Directory.CreateDirectory(destinationPath);
            foreach (var file in files)
            {
                File.WriteAllText(Path.Combine(destinationPath, file), "Test content");
            }

            // Act
            compressionService.CompressLz(files, destinationPath);

            // Assert
            mockCompressor.Verify(c => c.CompressDirectory(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            // Cleanup
            Directory.Delete(destinationPath, true);
        }

        [Fact]
        public void CompressLz_EmptyFilesArray_DoesNothing()
        {
            // Arrange
            var files = new string[] { };  // Empty array
            var destinationPath = "test/destination";
            var compressionService = new CompressionService();

            Directory.CreateDirectory(destinationPath);  // Ensure destination exists

            // Act
            compressionService.CompressLz(files, destinationPath);

            // Assert
            var compressedFile = Path.Combine(destinationPath, "compressed.lz");
            Assert.False(File.Exists(compressedFile));

            // Cleanup
            Directory.Delete(destinationPath, true);
        }

        [Fact]
        public void CompressLz_InvalidFilePath_ThrowsException()
        {
            // Arrange
            var files = new[] { "invalid/path/file.txt" };
            var destinationPath = "test/destination";
            var compressionService = new CompressionService();

            Directory.CreateDirectory(destinationPath);

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() =>
                compressionService.CompressLz(files, destinationPath));
        }

        [Fact]
        public void CompressLz_EmptyDestinationPath_ThrowsException()
        {
            // Arrange
            var files = new[] { "file1.txt" };
            var destinationPath = "";  // Empty destination
            var compressionService = new CompressionService();

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                compressionService.CompressLz(files, destinationPath));
        }

    }
}
