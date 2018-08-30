using System.Runtime.Serialization;

namespace RadixAPIGateway.Domain.Models.Request.EnumTypes
{
    public enum CreditCardBrandEnum
    {
        /// <summary>
        /// Visa
        /// </summary>
        [EnumMember]
        Visa = 1,

        /// <summary>
        /// MasterCard
        /// </summary>
        [EnumMember]
        Mastercard = 2,
    }
}
