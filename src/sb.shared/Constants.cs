using System.Runtime.InteropServices;

namespace sb.shared
{
    public static class Constants
    {
        public static readonly string configPath = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "sb", "config", "config.json") : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".sb", "config", "config.json");
        public static readonly string sbFolder = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "sb") : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".sb");
        public static readonly string sbConfigFolder = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "sb", "config") : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".sb", "config");

        public const string WindowsRegexPattern = @"^(?:[a-zA-Z]:|\\)(\\[a-zA-Z0-9_-]+)+\\?$";
        public const string LinuxRegexPattern = @"^(/[^/ ]*)+/?$";

        public static readonly string WindowsTestPath = "C:\\Users";
        public static readonly string LinuxTestPath = "/home/"; // Will need to double check.
    }
}
