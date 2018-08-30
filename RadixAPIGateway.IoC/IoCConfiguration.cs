using Microsoft.Extensions.DependencyInjection;
using RadixAPIGateway.Data.Repositories;
using RadixAPIGateway.Domain.Interfaces.Repositories;
using RadixAPIGateway.Domain.Interfaces.Services;
using RadixAPIGateway.Domain.Services;

namespace RadixAPIGateway.IoC
{
    public class IoCConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            //Services
            services.AddScoped(typeof(IAcquirerService), typeof(AcquirerService));
            services.AddScoped(typeof(IStoreService), typeof(StoreService));
            services.AddScoped(typeof(ISaleService), typeof(SaleService));

            //Repositories
            //services.AddScoped(typeof(IAcquirerRepository), typeof(AcquirerRepository));
            services.AddScoped(typeof(IStoreRepository), typeof(StoreRepository));
        }
    }
}
