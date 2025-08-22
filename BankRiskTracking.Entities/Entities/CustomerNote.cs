namespace BankRiskTracking.Entities.Entities
{
    public  class CustomerNote
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int CustomerId { get; set; }
        public string NoteText { get; set; }
        public DateTime CreatedDate { get; set; }

        public Customer Customer { get; set; }
    }
}
