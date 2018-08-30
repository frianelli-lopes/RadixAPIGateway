using RadixAPIGateway.Domain.Interfaces.Providers;
using RadixAPIGateway.Domain.Models.EnumTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadixAPIGateway.Provider.SaleTransaction
{
    public class SaleTransactionFactory : ISaleTransactionProvider
    {
        public static ISaleTransactionProvider GetInstance(AcquirerEnum acquirer)
        {
            if (acquirer == AcquirerEnum.Cielo) return new CieloSaleTransaction();

            return new StoneSaleTransaction();
        }
    }
}
