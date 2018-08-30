﻿using RadixAPIGateway.Domain.Models.EnumTypes;
using System.ComponentModel.DataAnnotations;

namespace RadixAPIGateway.Domain.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasAntiFraudAgreement { get; set; }
        public AcquirerEnum? Acquirer { get; set; }
    }
}
