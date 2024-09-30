using sb.core.models;

namespace sb.core.interfaces
{
    public interface IConfigService
    {
        ConfigModel LoadConfig();
        void SaveConfig(ConfigModel config);
        void UpdateConfig(IConfigService configService, string setting, string value);
    }
}

