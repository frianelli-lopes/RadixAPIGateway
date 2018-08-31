using RadixAPIGateway.Domain.Interfaces.Providers;
using RadixAPIGateway.Domain.Models.Request;

namespace RadixAPIGateway.Provider.AntiFraud
{
    public class ClearSaleAntiFraudProvider : IAntiFraudProvider
    {
        public bool IsSecure(SaleRequest sale)
        {
            return true;
        }
    }
}
