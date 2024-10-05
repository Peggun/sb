/*
 * 
 *                                                            This and other files in Lz77 Folder are inspired and modified based
 *                                                            on this repository
 *                                                            
 *                                                            https://github.com/zlociu/LZ77.NET
 * 
 */

using sb.core.compressors.lz77.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace sb.core.compressors.lz77.algorithms
{
    public static class BinaryWriterExtension
    {
        public static void Write(this BinaryWriter writer, Lz77CoderOutputModel model)
        {
            writer.Write(model.Position);
            writer.Write(model.Length);
            writer.Write(model.Character);
        }
    }
}
