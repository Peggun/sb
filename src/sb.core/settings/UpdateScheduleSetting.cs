using sb.core.interfaces;
using sb.shared.enums;

namespace sb.core.settings
{
    public static class UpdateScheduleSetting
    {
        public static void UpdateSchedule(IConfigService configService, string value)
        {
            ScheduleTimes type;

            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("Please provide a value for this setting.");
                return;
            }

            var config = configService.LoadConfig();

            try
            {
                type = (ScheduleTimes)Enum.Parse(typeof(ScheduleTimes), value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid value {value} for Auto Compression Type.");
                return;
            }

            config.Schedule = Enum.GetName(type);
            configService.SaveConfig(config);

            Console.WriteLine($"Schedule successfully updated to {type}.");
        }
    }
}
