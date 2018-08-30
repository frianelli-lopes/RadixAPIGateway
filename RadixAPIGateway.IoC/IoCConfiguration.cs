using Microsoft.Extensions.DependencyInjection;
using RadixAPIGateway.Data.Repositories;
using RadixAPIGateway.Domain.Interfaces.Providers;
using RadixAPIGateway.Domain.Interfaces.Repositories;
using RadixAPIGateway.Domain.Interfaces.Services;
using RadixAPIGateway.Domain.Services;
using RadixAPIGateway.Provider.AntiFraud;

namespace RadixAPIGateway.IoC
{
    public class IoCConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            //Services
            services.AddScoped(typeof(IStoreService), typeof(StoreService));
            services.AddScoped(typeof(ISaleService), typeof(SaleService));

            //Repositories
            services.AddScoped(typeof(IStoreRepository), typeof(StoreRepository));

            //Providers
            services.AddScoped(typeof(IAntiFraudProvider), typeof(ClearSaleAntiFraud));
        }
    }
}
