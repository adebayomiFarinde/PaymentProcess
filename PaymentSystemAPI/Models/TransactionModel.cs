using System.ComponentModel.DataAnnotations;

namespace PaymentSystemAPI.Models
{
    public class TransactionModel
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Range(0.1, 99999999.99)]
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public Guid BusinessId { get; set; }
    }
}
