using RadixAPIGateway.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace RadixAPIGateway.Data.Context.Seeds
{
    public class SeedStore
    {
        public static void InsertData(EFContext context)
        {
            if (!context.Store.Any())
            {
                IList<Store> listStore = new List<Store>() {
                    new Store() {Name="Abba Technology - Brasil", HasAntiFraudAgreement=false, Acquirer=Domain.Models.EnumTypes.AcquirerEnum.Stone},
                    new Store() {Name="Abba Technology - EUA", HasAntiFraudAgreement=true, Acquirer=Domain.Models.EnumTypes.AcquirerEnum.Cielo}
                };

                context.Store.AddRange(listStore);
                context.SaveChanges();
            }
        }
    }
}
