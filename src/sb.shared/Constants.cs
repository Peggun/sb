using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace sb.shared
{
    public static class Constants
    {
        public static readonly string configPath = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "sb", "config", "config.json") : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".sb", "config", "config.json");

        public const string WindowsRegexPattern = @"^(?:[a-zA-Z]:|\\)(\\[a-zA-Z0-9_-]+)+\\?$";
        public const string LinuxRegexPattern = @"^(/[^/ ]*)+/?$";
    }
}
