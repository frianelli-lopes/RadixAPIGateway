using RadixAPIGateway.Domain.Models.Request;

namespace RadixAPIGateway.Domain.Interfaces.Providers
{
    public interface IAntiFraudProvider
    {
        bool IsSecure(SaleRequest sale);
    }
}
