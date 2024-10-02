using sb.core.interfaces;
using sb.shared.enums;

namespace sb.core.settings
{
    public static class UpdateAutoCompressionTypeSetting
    {
        public static void UpdateAutoCompressionType(IConfigService configService, string value)
        {
            CompressionTypes type;

            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("Please provide a value for this setting.");
                return;
            }

            var config = configService.LoadConfig();

            try
            {
                type = (CompressionTypes)Enum.Parse(typeof(CompressionTypes), value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid value {value} for Auto Compression Type.");
                return;
            }

            config.AutoCompressionType = Enum.GetName(type);
            configService.SaveConfig(config);

            Console.WriteLine($"Auto Compression Type updated successfully to {type}.");
        }
    }
}
