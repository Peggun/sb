using sb.core.interfaces;
using sb.shared.enums;

namespace sb.core.settings
{
    public static class UpdateIncrementalCompareMethodSetting
    {
        public static void UpdateIncrementalCompareMethod(IConfigService configService, string value)
        {
            CompareMethods type;

            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("Please provide a value for this setting.");
                return;
            }

            var config = configService.LoadConfig();

            try
            {
                type = (CompareMethods)Enum.Parse(typeof(CompareMethods), value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid value {value} for Incremental Compare Method");
                return;
            }

            config.CompareMethod = Enum.GetName(type);
            configService.SaveConfig(config);

            Console.WriteLine($"Incremental Compare Method updated successfully to {type}.");
        }
    }
}
