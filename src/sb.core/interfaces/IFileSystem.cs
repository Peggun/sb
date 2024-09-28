using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sb.core.interfaces
{
    public interface IFileSystem
    {
        bool Exists(string path);
        string ReadAllText(string path);
        void WriteAllText(string path, string contents);
        void CreateFile(string path);
        void CreateDirectory(string path);
    }

    public class FileSystem : IFileSystem
    {
        public bool Exists(string path) => File.Exists(path);
        public string ReadAllText(string path) => File.ReadAllText(path);
        public void WriteAllText(string path, string contents) => File.WriteAllText(path, contents);
        public void CreateFile(string path) => File.Create(path).Dispose();
        public void CreateDirectory(string path) => Directory.CreateDirectory(path);
    }
}
