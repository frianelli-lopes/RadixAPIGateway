using Microsoft.Extensions.DependencyInjection;
using RadixAPIGateway.Data.Repositories;
using RadixAPIGateway.Domain.Interfaces.Providers;
using RadixAPIGateway.Domain.Interfaces.Repositories;
using RadixAPIGateway.Domain.Interfaces.Services;
using RadixAPIGateway.Domain.Services;
using RadixAPIGateway.Provider.AntiFraud;
using RadixAPIGateway.Provider.SaleTransaction;

namespace RadixAPIGateway.IoC
{
    public class IoCConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            //Services
            services.AddScoped(typeof(IStoreService), typeof(StoreService));
            services.AddScoped(typeof(ISaleTransactionService), typeof(SaleTransactionService));

            //Repositories
            services.AddScoped(typeof(IStoreRepository), typeof(StoreRepository));

            //Providers
            services.AddScoped(typeof(IAntiFraudProvider), typeof(ClearSaleAntiFraudProvider));
            services.AddScoped(typeof(ISaleTransactionProvider), typeof(SaleTransactionProvider));
        }
    }
}
