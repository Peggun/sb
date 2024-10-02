using sb.core.interfaces;

namespace sb.core.settings
{
    public static class UpdateRetentionLengthSetting
    {
        public static void UpdateRetentionPolicy(IConfigService configService, string value)
        {
            int result;

            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("Please add a value for this setting.");
                return;
            }

            try
            {
                result = int.Parse(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Please put a valid number.");
                return;
            }

            var config = configService.LoadConfig();
            config.RetentionPolicy = result;
            configService.SaveConfig(config);

            Console.WriteLine($"Retention Policy successfully updated to {value}");
        }
    }
}
