﻿using sb.core.interfaces;

namespace sb.core.settings
{
    public static class UpdateEnableAutoVerificationSetting
    {
        public static void UpdateEnableAutoVerification(IConfigService configService, string value)
        {
            string[] validValues = { "true", "false" };

            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Please provide a value for this setting.");
                return;
            }

            if (!validValues.Contains(value.ToLower()))
            {
                Console.WriteLine("Invalid value for this setting. Please use 'True' or 'False'");
                return;
            }

            var config = configService.LoadConfig();
            bool enableAutoVerification = bool.Parse(value);
            config.EnableAutoVerification = enableAutoVerification;
            configService.SaveConfig(config);

            Console.WriteLine($"Auto Compression updated successfully to {enableAutoVerification}.");
        }
    }
}