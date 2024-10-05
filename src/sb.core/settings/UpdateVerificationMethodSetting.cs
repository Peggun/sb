using sb.core.interfaces;
using sb.shared.enums;

namespace sb.core.settings
{
    public static class UpdateVerificationMethodSetting
    {
        public static void UpdateVerificationMethod(IConfigService configService, string value)
        {
            VerificationMethods type;

            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("Please provide a value for this setting.");
                return;
            }

            var config = configService.LoadConfig();

            try
            {
                type = (VerificationMethods)Enum.Parse(typeof(VerificationMethods), value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid value {value} for Auto Compression Type.");
                return;
            }

            config.VerificationMethod = Enum.GetName(type);
            configService.SaveConfig(config);

            Console.WriteLine($"Verification Method updated successfully to {value}");
        }
    }
}
