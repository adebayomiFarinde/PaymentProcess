namespace PaymentSystem.Core.Entities
{
    public class Customer : Entity
    {
        public string? NationalIDNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DOB { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<TransactionHistory>? TransactionHistories { get; set; }

    }
}
