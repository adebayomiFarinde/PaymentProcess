using PaymentSystem.Core.Entities.Enums;

namespace PaymentSystem.Core.Entities
{
    public class TransactionHistory : Entity
    {
        public decimal Amount { get; set; }
        public Guid BusinessId {get; set;}
        public Customer? Customer { get; set;}
        public Guid CustomerId { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
