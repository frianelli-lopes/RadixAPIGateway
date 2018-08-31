using RadixAPIGateway.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadixAPIGateway.Data.Context.Seeds
{
    public class SeedSaleTransaction
    {
        public static void InsertData(EFContext context)
        {
            if (!context.SaleTransaction.Any())
            {
                IList<SaleTransaction> listSaleTransaction = new List<SaleTransaction>() {
                    new SaleTransaction() {Date=new DateTime(2018,07,01), Total=155, StoreId=1},
                    new SaleTransaction() {Date=new DateTime(2018,08,17), Total=160.35F, StoreId=1},
                    new SaleTransaction() {Date=new DateTime(2018,08,01), Total=538, StoreId=2},
                    new SaleTransaction() {Date=new DateTime(2018,08,02), Total=300.35F, StoreId=2},
                    new SaleTransaction() {Date=new DateTime(2018,08,03), Total=345, StoreId=2}
                };

                context.SaleTransaction.AddRange(listSaleTransaction);
                context.SaveChanges();
            }
        }
    }
}
