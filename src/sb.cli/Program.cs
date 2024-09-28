using sb.core;

namespace sb.cli
{
    class Program
    {
        public static void Main(string[] args)
        {
            core.ConfigService.SaveConfig(new core.models.ConfigModel());
        }
    }
}