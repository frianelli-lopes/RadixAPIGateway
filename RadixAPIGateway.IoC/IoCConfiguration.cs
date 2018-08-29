using Microsoft.Extensions.DependencyInjection;

namespace RadixAPIGateway.IoC
{
    public class IoCConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            //services.AddScoped(typeof(IAbaService), typeof(AbaService));
        }
    }
}
