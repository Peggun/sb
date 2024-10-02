using sb.core.interfaces;

namespace sb.core.settings
{
    public static class UpdateBackupTimeSetting
    {
        public static void UpdateBackupTime(IConfigService configService, string value)
        {

        }

        // Helper Methods // Custom Time Parser //
        // Time format e.g. 02:00AM
        // This is a custom time parser to help with the parsing of time
        // in the format above.
        // That format is the probable time format used in the releases.
        // This will support 12 and 24 hour time just using one check.

        // Turns out DateTime.Parse exists... currently has everything that
        // i need... if anything that i need to use that DateTime doesnt
        // have, i will code a fresh one.
    }
}
