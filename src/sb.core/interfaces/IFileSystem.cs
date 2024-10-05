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
        string[] GetFiles(string path);
        string[] GetFiles(string path, string searchPattern, SearchOption option);
        void CopyFile(string source, string destination, bool overwrite);
    }

    public class FileSystem : IFileSystem
    {
        public bool FileExists(string path) => File.Exists(path);
        public bool DirExists(string path) => Directory.Exists(path);
        public string ReadAllText(string path) => File.ReadAllText(path);
        public void WriteAllText(string path, string contents) => File.WriteAllText(path, contents);
        public void CreateFile(string path) => File.Create(path).Dispose();
        public void CreateDirectory(string path) => Directory.CreateDirectory(path);
        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }
        public string[] GetFiles(string path, string searchPattern, SearchOption option)
        {
            return Directory.GetFiles(path, searchPattern, option);
        }
        public void CopyFile(string source, string destination, bool overwrite) => File.Copy(source, destination, overwrite);
    }
}
