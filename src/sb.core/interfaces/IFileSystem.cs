namespace sb.core.interfaces
{
    public interface IFileSystem
    {
        bool FileExists(string path);
        bool DirExists(string path);
        string ReadAllText(string path);
        void WriteAllText(string path, string contents);
        void CreateFile(string path);
        void CreateDirectory(string path);
    }

    public class FileSystem : IFileSystem
    {
        public bool FileExists(string path) => File.Exists(path);
        public bool DirExists(string path) => Directory.Exists(path);
        public string ReadAllText(string path) => File.ReadAllText(path);
        public void WriteAllText(string path, string contents) => File.WriteAllText(path, contents);
        public void CreateFile(string path) => File.Create(path).Dispose();
        public void CreateDirectory(string path) => Directory.CreateDirectory(path);
    }
}
