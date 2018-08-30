using System.ComponentModel.DataAnnotations;

namespace RadixAPIGateway.Domain.Models.Request
{
    public class SaleRequest
    {
        [Required(ErrorMessage = "Campo [IdLoja] é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Valor do campo [IdLoja] está inválido")]
        public int IdLoja { get; set; }

        [Required(ErrorMessage = "As informações da transação da venda são obrigatórias")]
        public TransactionRequest Transacao { get; set; }
    }
}
