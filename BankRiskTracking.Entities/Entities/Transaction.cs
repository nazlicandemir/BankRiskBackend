namespace BankRiskTracking.Entities.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime TransactionDate {  get; set; }
        public decimal Amount { get; set; }

        public string TrancationType {  get; set; }
        
        public string? Description { get; set; }

        public Customer Customer { get; set; }

        public string Title { get; set; }
    }
}
