using SharpCompress.Archives;
using SharpCompress.Archives.Tar;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenZip;
using System.Diagnostics;
using SharpCompress.Readers;
using SharpCompress.Archives.GZip;
using SharpCompress.Compressors.Xz;
using SharpCompress.Compressors.LZMA;
using sb.core.compressors.lz77.algorithms;
using SevenZip.Sdk.Compression.Lzma;
using System.Reflection;

namespace sb.core.interfaces
{
    public interface ICompressionService
    {
        void CompressZip(string sourcePath, string destinationPath, System.IO.Compression.CompressionLevel level, bool includeBaseDirectory);
        void Compress7Zip(string sourcePath, string destinationPath);
        void CompressGzip(string sourcePath, string destinationFile);
        void CompressLz(string[] files, string destinationPath, string? outputFileName = null, int compressionLevel = 5); // Using LZ77 Compression
        void CompressTar(string sourcePath, string destinationFile);
        void CompressRar(string sourcePath, string destinationFile);
        void CompressCab(string sourcePath, string destinationFile);
        void CompressIso(string sourcePath, string destinationFile);
        void CompressTarGz(string sourcePath, string destinationFile);
        void CompressTarBz2(string sourcePath, string destinationFile);
        void CompressTarXz(string sourcePath, string destinationFile);
        void CompressBz2(string sourcePath, string destinationFile);
        void CompressLZMA(string[] files, string destinationFile, string? outputFileName);
        void CompressLZW(string sourcePath, string destinationFile);
    }

    public class CompressionService
    {
        IFileSystem fileSystem = new FileSystem();

        public void CompressZip(string sourcePath, string destinationPath, System.IO.Compression.CompressionLevel level, bool includeBaseDirectory)
        {
            // Adjust the sourcePath to target the contents of the folder, not the folder itself
            string[] files = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);
            string zipFilePath = Path.Combine(destinationPath, Path.GetFileName(sourcePath) + ".zip");

            using (var zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
            {
                foreach (string file in files)
                {
                    var relativePath = Path.GetRelativePath(sourcePath, file);
                    zipArchive.CreateEntryFromFile(file, relativePath, level);
                }
            }
        }

        public void CompressGzip(string sourcePath, string destinationFile)
        {
            using (FileStream sourceFile = File.OpenRead(sourcePath))
            using (FileStream destination = File.Create(destinationFile))
            using (GZipStream compressionStream = new GZipStream(destination, System.IO.Compression.CompressionMode.Compress))
            {
                sourceFile.CopyTo(compressionStream);
            }
        }

        public void Compress7Zip(string backupFolder, string destinationPath)
        {
            try
            {
                string dllFolder = Environment.Is64BitProcess ? "x64" : "x86";
                string dllPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), dllFolder, "7z.dll");
                if (!File.Exists(dllPath))
                {
                    Debug.WriteLine($"7z.dll not found at: {dllPath}");
                    throw new FileNotFoundException("7z.dll not found", dllPath);
                }
                //Debug.WriteLine($"7z.dll found at: {dllPath}");
                SevenZipCompressor.SetLibraryPath(dllPath);


                var compressor = new SevenZipCompressor
                {
                    CompressionLevel = SevenZip.CompressionLevel.Ultra,
                    CompressionMode = SevenZip.CompressionMode.Create,
                    CompressionMethod = CompressionMethod.Default,
                    FastCompression = true
                };

                var archiveFile = Path.Combine(destinationPath, "backup.7z");

                compressor.CompressDirectory(backupFolder, archiveFile);

                Console.WriteLine("Backup completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void CompressLz(string[] files, string destinationPath, string? outputFileName = null, int compressionLevel = 5)
        {
            Lz77Compressor compressor = new Lz77Compressor();

            string tempDirectory = Path.Combine(destinationPath, "temp_compression");
            Directory.CreateDirectory(tempDirectory);

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(tempDirectory, fileName);
                File.Copy(file, destFile, overwrite: true);
            }

            compressor.CompressDirectory(tempDirectory, outputFileName);

            Directory.Delete(tempDirectory, recursive: true);
        }


        public void CompressLZMA(string[] files, string destinationPath, string? outputFileName)
        {
            outputFileName ??= Path.Combine(destinationPath, "archive.lzma");

            SevenZip.Sdk.Compression.Lzma.Encoder encoder = new SevenZip.Sdk.Compression.Lzma.Encoder();

            using (MemoryStream inputMemoryStream = new MemoryStream())
            {
                foreach (var file in files)
                {
                    byte[] fileBytes = File.ReadAllBytes(file);
                    inputMemoryStream.Write(fileBytes, 0, fileBytes.Length);
                }

                inputMemoryStream.Position = 0;

                using (FileStream outputFileStream = new FileStream(outputFileName, FileMode.Create))
                {
                    encoder.WriteCoderProperties(outputFileStream);

                    outputFileStream.Write(BitConverter.GetBytes(inputMemoryStream.Length), 0, 8);

                    encoder.Code(inputMemoryStream, outputFileStream, inputMemoryStream.Length, -1, null);
                }
            }
        } 
    }
}
