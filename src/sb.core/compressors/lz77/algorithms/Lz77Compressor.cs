/*
 * 
 *                                                            This and other files in Lz77 Folder are inspired and modified based
 *                                                            on this repository and a little bit of AI Magic cuz I have no idea 
 *                                                            what I'm doing :)
 *                                                            
 *                                                            https://github.com/zlociu/LZ77.NET
 * 
 */

using sb.core.compressors.lz77.interfaces;
using sb.core.compressors.lz77.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sb.core.compressors.lz77.algorithms
{
    public enum Lz77BufferSize
    {
        B32 = 31,
        B64 = 63,
        B128 = 127,
        B256 = 255
    }

    public sealed class Lz77Compressor : ICompressor
    {
        private readonly ushort _dictionarySize;
        private readonly ushort _bufferSize;

        private readonly ushort _bufferBitLen;

        private Lz77CoderOutputModel ConvertShortToCoderModel(ushort number)
        {
            return new Lz77CoderOutputModel(
                Position: (ushort)(number >> (_bufferBitLen) & _dictionarySize),
                Length: (byte)(number & _bufferSize),
                Character: (char)0);
        }

        private ushort ConvertCoderOutputToShort(Lz77CoderOutputModel model)
        {
            ushort number;
            number = model.Length;
            number += (ushort)(model.Position << _bufferBitLen);
            return number;
        }

        private Lz77CoderOutputModel GetLongestSubstring(ReadOnlySpan<char> dictionary, ReadOnlySpan<char> buffer)
        {
            return KMPSearch.KMPGetLongestMatch(dictionary, buffer);
        }

        public Lz77Compressor(Lz77BufferSize bufferSize = Lz77BufferSize.B64)
        {
            _dictionarySize = 32767;
            _bufferSize = (ushort)bufferSize;
            _bufferBitLen = 8;
        }

        public void CompressDirectory(string directoryPath, string? outputFileName)
        {
            // Step 1: Get all files in the directory
            var files = Directory.GetFiles(directoryPath);

            Span<char> dictionary = new char[2 * _dictionarySize];
            Span<char> buffer = new char[4 * _bufferSize];

            var outputFile = File.Create(outputFileName is null ? "compressed_directory.lz77" : (outputFileName + ".lz77"));
            var outputStream = new BinaryWriter(outputFile);

            ushort _dictionaryFillNumber = 0;
            ushort _bufferFillNumber;

            ushort _bufferSegmentOffset = 0;
            ushort _dictionarySegmentOffset = 0;

            bool endData = false;

            foreach (var fileName in files)
            {
                var inputFile = File.OpenRead(fileName);
                var inputStream = new BinaryReader(inputFile);

                // Step 2: Add file name marker
                WriteFileBoundaryMarker(outputStream, fileName);

                // Step 3: Read file content and compress
                var fst = inputStream.ReadChars(4 * _bufferSize);
                fst.CopyTo(buffer);
                _bufferFillNumber = (ushort)fst.Length;

                while (_bufferFillNumber > 0)
                {
                    var coderOut = GetLongestSubstring(
                        dictionary.Slice(_dictionarySegmentOffset, _dictionarySize),
                        buffer.Slice(_bufferSegmentOffset, _bufferSize));
                    if (coderOut.Length < _bufferFillNumber)
                    {
                        // Compress file content as in original logic
                        // (Unchanged logic from original)

                        // Update buffer and dictionary positions
                        if ((_dictionaryFillNumber + coderOut.Length + 1) > _dictionarySize)
                        {
                            if ((_dictionarySegmentOffset + coderOut.Length + 1) >= _dictionarySize)
                            {
                                Span<char> arr = new char[2 * _dictionarySize];
                                dictionary.Slice(_dictionarySegmentOffset, _dictionarySize).CopyTo(arr);
                                dictionary = arr;
                                _dictionarySegmentOffset = 0;
                            }
                            var rest = (ushort)((coderOut.Length + 1) - (_dictionarySize - _dictionaryFillNumber));
                            _dictionarySegmentOffset += rest;
                            _dictionaryFillNumber -= rest;
                        }
                        buffer
                            .Slice(_bufferSegmentOffset, coderOut.Length + 1)
                            .CopyTo(dictionary.Slice(_dictionarySegmentOffset + _dictionaryFillNumber, coderOut.Length + 1));

                        if (_bufferSegmentOffset + (coderOut.Length + 1) >= (3 * _bufferSize))
                        {
                            Span<char> arr = new char[4 * _bufferSize];
                            if (!endData)
                            {
                                var tmp = inputStream.ReadChars(4 * _bufferSize - _bufferFillNumber);
                                tmp.CopyTo(arr.Slice((_bufferFillNumber)));
                                endData = (tmp.Length < (4 * _bufferSize - _bufferFillNumber));
                                _bufferFillNumber += (ushort)(tmp.Length);
                            }
                            buffer.Slice(_bufferSegmentOffset).CopyTo(arr);
                            buffer = arr;
                            _bufferSegmentOffset = 0;
                        }

                        _bufferFillNumber -= (ushort)(coderOut.Length + 1);
                        _bufferSegmentOffset += (ushort)(coderOut.Length + 1);
                        _dictionaryFillNumber += (ushort)(coderOut.Length + 1);

                        outputStream.Write(coderOut);
                    }
                    else
                    {
                        _bufferFillNumber = 0;
                        outputStream.Write(coderOut);
                    }
                }
                inputStream.Close();
                inputFile.Close();
            }

            outputStream.Flush();
            outputStream.Close();
            outputFile.Close();
        }

        public void DecompressDirectory(string compressedFileName, string outputDirectory)
        {
            Span<char> dictionary = new char[2 * _dictionarySize];

            if (compressedFileName.EndsWith(".lz77"))
            {
                var inputFile = File.OpenRead(compressedFileName);
                var inputStream = new BinaryReader(inputFile);

                ushort _dictionaryFillNumber = 0;
                ushort _dictionarySegmentOffset = 0;

                while (inputStream.BaseStream.Position + 4 < inputStream.BaseStream.Length)
                {
                    // Step 1: Read file boundary marker
                    ushort fileNameLength = inputStream.ReadUInt16();
                    string fileName = new string(inputStream.ReadChars(fileNameLength));
                    string outputFilePath = Path.Combine(outputDirectory, fileName);

                    // Step 2: Create output file for decompressed content
                    var outputFile = File.Create(outputFilePath);
                    var outputStream = new BinaryWriter(outputFile);

                    // Step 3: Continue decompression logic for the current file
                    while (inputStream.BaseStream.Position + 4 < inputStream.BaseStream.Length)
                    {
                        //1
                        var model = new Lz77CoderOutputModel(
                            inputStream.ReadUInt16(),
                            inputStream.ReadByte(),
                            inputStream.ReadChar());

                        //2 - 6 (decompress each chunk, same logic as original)
                        ReadOnlySpan<char> source = dictionary.Slice(_dictionarySegmentOffset + model.Position, model.Length);
                        if ((_dictionaryFillNumber + model.Length + 1) > _dictionarySize)
                        {
                            if ((_dictionarySegmentOffset + model.Length + 1) >= _dictionarySize)
                            {
                                Span<char> arr = new char[2 * _dictionarySize];
                                dictionary.Slice(_dictionarySegmentOffset, _dictionarySize).CopyTo(arr);
                                dictionary = arr;
                                _dictionarySegmentOffset = 0;
                            }
                            var rest = (ushort)((model.Length + 1) - (_dictionarySize - _dictionaryFillNumber));
                            _dictionarySegmentOffset += rest;
                            _dictionaryFillNumber -= rest;
                        }

                        Span<char> dest = dictionary.Slice(_dictionarySegmentOffset + _dictionaryFillNumber, model.Length + 1);
                        source.CopyTo(dest);
                        dest[model.Length] = model.Character;
                        _dictionaryFillNumber += (ushort)(model.Length + 1);
                        outputStream.Write(dest);
                    }

                    // Finalize the decompression for the current file
                    var last = new Lz77CoderOutputModel(
                        Position: inputStream.ReadUInt16(),
                        Length: inputStream.ReadByte(),
                        Character: inputStream.ReadChar());
                    outputStream.Write(dictionary.Slice(_dictionarySegmentOffset + last.Position, last.Length));

                    // Flush and close the output file for the current file
                    outputStream.Flush();
                    outputStream.Close();
                }

                // Close the compressed input file
                inputStream.Close();
                inputFile.Close();
            }
            else
            {
                throw new FileNotFoundException("wrong input file extension (should end with .lz77)");
            }
        }


        private void WriteFileBoundaryMarker(BinaryWriter outputStream, string fileName)
        {
            // Write a file marker (e.g., file name length and name)
            outputStream.Write((ushort)fileName.Length); // Length of the file name
            outputStream.Write(fileName.ToCharArray());  // File name characters
                                                         // This helps distinguish between different files in the decompressed output
        }
    }

    static class KMPSearch
    {
        private static int[] BuildSearchTable(ReadOnlySpan<char> buffer)
        {
            int[] tab = new int[buffer.Length];

            int i = 2, j = 0;
            tab[0] = -1; tab[1] = 0;

            while (i < buffer.Length)
            {
                if (buffer[i - 1] == buffer[j])
                {
                    ++j;
                    tab[i] = j;
                    ++i;
                }
                else
                {
                    if (j > 0)
                    {
                        j = tab[j];
                    }
                    else
                    {
                        tab[i] = 0;
                        ++i;
                    }
                }
            }
            return tab;
        }

        public static Lz77CoderOutputModel KMPGetLongestMatch(ReadOnlySpan<char> dictionary, ReadOnlySpan<char> buffer)
        {

            if (buffer.Length == 0) throw new IndexOutOfRangeException();

            var tab = BuildSearchTable(buffer);

            int m = 0; 
            int i = 0;  

            int bestPos = 0;
            int bestLength = 0;

            while (m + i < dictionary.Length)
            {
                if (buffer[i] == dictionary[m + i])
                {
                    ++i;

                    if (i == buffer.Length - 1)
                    {
                        return new Lz77CoderOutputModel(Position: (ushort)m, Length: (byte)i, Character: buffer[i]);
                    }

                    if (i > bestLength)
                    {
                        bestLength = i;
                        bestPos = m;
                    }
                }
                else
                {
                    m = m + i - tab[i];
                    if (i > 0)
                    {
                        i = tab[i];
                    }
                }
            }

            return new Lz77CoderOutputModel(
                Position: (ushort)bestPos,
                Length: (byte)bestLength,
                Character: buffer[bestLength]);
        }
    }
}