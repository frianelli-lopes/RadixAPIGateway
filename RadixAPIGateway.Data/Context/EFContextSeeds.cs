using RadixAPIGateway.Data.Context.Seeds;

namespace RadixAPIGateway.Data.Context
{
    public static class EFContextSeeds
    {
        public static void Seed(this EFContext context)
        {
            SeedStore.InsertData(context);
            SeedSaleTransaction.InsertData(context);
        }
    }
}
