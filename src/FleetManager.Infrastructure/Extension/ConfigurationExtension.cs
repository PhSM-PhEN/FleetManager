using Microsoft.Extensions.Configuration;

namespace FleetManager.Infrastructure.Extension
{
    public static class ConfigurationExtension
    {
        public static bool IsTestEnvironment(this IConfiguration configuration)
        {
            return configuration.GetValue<bool>("ImMemoryTest");
        }
    }
}
