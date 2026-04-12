using Microsoft.Extensions.Configuration;

namespace FleetManager.Infrastructure.Extension
{
    public static class ConfigurationExtension
    {
        public static bool IsTestEnviroment(this IConfiguration configuration)
        {
            return configuration.GetValue<bool>("ImMemoryTest");
        }
    }
}
