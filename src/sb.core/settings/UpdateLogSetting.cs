using sb.core.interfaces;

namespace sb.core.settings
{
    public static class UpdateLogSetting
    {
        public static void UpdateLog(IConfigService configService, string value)
        {
            string[] validValues = { "true", "false" };

            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("Please provide a value for this setting.");
                return;
            }

            if (!validValues.Contains(value))
            {
                Console.WriteLine("Invalid value for this setting. Please use 'True' or 'False'");
                return;
            }

            var config = configService.LoadConfig();
            bool log = bool.Parse(value);
            config.Log = log;
            configService.SaveConfig(config);

            Console.WriteLine($"Log updated successfully to {log}.");
        }
    }
}
