﻿/*
 * 
 *                                                            This and other files in Lz77 Folder are inspired and modified based
 *                                                            on this repository
 *                                                            
 *                                                            https://github.com/zlociu/LZ77.NET
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sb.core.compressors.lz77.interfaces
{
    public interface ICompressor
    {
        void CompressDirectory(string filename, string? outputFileName);
        void DecompressDirectory(string inputFileName, string outputDirectory);
    }
}
