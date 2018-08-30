using RadixAPIGateway.Domain.Interfaces.Providers;
using RadixAPIGateway.Domain.Models.Request;

namespace RadixAPIGateway.Provider.AntiFraud
{
    public class ClearSaleAntiFraud : IAntiFraudProvider
    {
        public bool IsSecure(SaleRequest sale)
        {
            return true;
        }
    }
}
