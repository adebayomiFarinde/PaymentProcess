namespace PaymentSystem.Core.Entities
{
    public class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
