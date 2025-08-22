namespace BankRiskTracking.Entities.Entities
{
    public  class RiskHistory
    {
        public int Id { get; set; } 

        public int CustomerId {  get; set; }

        public string Title { get; set; }
        public RiskLevel RiskLevel { get; set; }
        public  DateTime EvaluatedDate { get; set; }
        public string? Notes { get; set; }

     
    }
}
