using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RadixAPIGateway.Domain.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasAntiFraudAgreement { get; set; }
        public int? AcquirerId { get; set; }

        public virtual ICollection<SaleTransaction> SalesTransaction { get; set; }
    }
}
