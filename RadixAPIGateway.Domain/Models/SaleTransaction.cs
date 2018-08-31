using System;
using System.ComponentModel.DataAnnotations;

namespace RadixAPIGateway.Domain.Models
{
    public class SaleTransaction
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float Total { get; set; }
        public int StoreId { get; set; }

        public Store Store { get; set; }
    }
}
