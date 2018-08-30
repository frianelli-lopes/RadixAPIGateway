using RadixAPIGateway.Domain.Models.Request.EnumTypes;
using System.ComponentModel.DataAnnotations;

namespace RadixAPIGateway.Domain.Models.Request
{
    public class CreditCardRequest
    {
        [Required(ErrorMessage = "Campo [numeroCartao] é obrigatório")]
        [MaxLength(16, ErrorMessage = "Campo [numeroCartao] deve conter exatos 16 caracteres")]
        [MinLength(16, ErrorMessage = "Campo [numeroCartao] deve conter exatos 16 caracteres")]
        public string NumeroCartao { get; set; }

        [Required(ErrorMessage = "Campo [bandeira] é obrigatório")]
        public CreditCardBrandEnum Bandeira { get; set; }

        [Required(ErrorMessage = "Campo [nomeImpressoCartao] é obrigatório")]
        public string NomeImpressoCartao { get; set; }

        [Required(ErrorMessage = "Campo [mesExpiracao] é obrigatório")]
        [MaxLength(2, ErrorMessage = "valor do campo [mesExpiracao] está inválido")]
        [MinLength(2, ErrorMessage = "valor do campo [mesExpiracao] está inválido")]
        public string MesExpiracao { get; set; }

        [Required(ErrorMessage = "Campo [anoExpiracao] é obrigatório")]
        [MaxLength(4, ErrorMessage = "valor do campo [anoExpiracao] está inválido")]
        [MinLength(4, ErrorMessage = "valor do campo [anoExpiracao] está inválido")]
        public string AnoExpiracao { get; set; }

        [Required(ErrorMessage = "Campo [codigoSeguranca] é obrigatório")]
        public string CodigoSeguranca { get; set; }
    }
}
