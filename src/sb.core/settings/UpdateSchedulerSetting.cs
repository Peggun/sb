using sb.core.interfaces;
using sb.shared.enums;

namespace sb.core.settings
{
    public static class UpdateSchedulerSetting
    {
        public static void UpdateScheduler(IConfigService configService, string value)
        {
            Schedulers type;

            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("Please provide a value for this setting.");
                return;
            }

            var config = configService.LoadConfig();

            try
            {
                type = (Schedulers)Enum.Parse(typeof(Schedulers), value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid value {value} for Scheduler.");
                return;
            }

            config.Scheduler = Enum.GetName(type);
            configService.SaveConfig(config);

            Console.WriteLine($"Scheduler updated successfully to {value}");
        }
    }
}
