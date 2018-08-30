using System.ComponentModel.DataAnnotations;

namespace RadixAPIGateway.Domain.Models.Request
{
    public class TransactionRequest
    {
        [Required(ErrorMessage = "Campo [valor] é obrigatório")]
        [Range(0.01, float.MaxValue, ErrorMessage = "Valor do campo [valor] está inválido")]
        public float Valor { get; set; }

        [Required(ErrorMessage = "Campo [parcelas] é obrigatório")]
        [Range(1, 12, ErrorMessage = "valor do campo [parcelas] está inválido")]
        public int Parcelas { get; set; }

        [Required(ErrorMessage = "Campo [numeroVenda] é obrigatório")]
        public string NumeroVenda { get; set; }

        [Required(ErrorMessage = "Informações do cartão de crédito são obrigatórias")]
        public CreditCardRequest CartaoCredito { get; set; }
    }
}
