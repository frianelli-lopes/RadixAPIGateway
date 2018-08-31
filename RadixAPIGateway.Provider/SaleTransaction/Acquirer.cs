using RadixAPIGateway.Domain.Models;
using RadixAPIGateway.Domain.Models.Request;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RadixAPIGateway.Provider.SaleTransaction
{
    public abstract class Acquirer
    {
        protected int Id { get; set; }

        private static List<Acquirer> acquires => new List<Acquirer>() {
            Cielo,
            Stone
        };
        
        private static CieloAcquirer Cielo = new CieloAcquirer(1);
        private static StoneAcquirer Stone = new StoneAcquirer(2);

        public abstract Task<HttpResponseMessage> SendRequest(Store store, SaleRequest request);

        public static Acquirer GetAcquirerById(int id)
        {
            return acquires.FirstOrDefault(a => a.Id == id);
        }
    }
}
