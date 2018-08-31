﻿using RadixAPIGateway.Domain.Models;
using RadixAPIGateway.Domain.Models.Request;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RadixAPIGateway.Provider.SaleTransaction
{
    public class StoneAcquirer : Acquirer
    {
        public StoneAcquirer(int id)
        {
            base.Id = id;
        }

        public async override Task<HttpResponseMessage> SendRequest(Store store, SaleRequest saleRequest)
        {
            HttpClient client = new HttpClient();

            var jsonInString = ""; //JsonConvert.SerializeObject(transactionToSend);
            var result = await client.PostAsync("https://transaction.stone.com.br", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

            return result;
        }
    }
}
