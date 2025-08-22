using BankRiskTracking.Entities.Entities;

namespace BankRiskTracking.Entities.DTOs
{
    public  class RiskHistroyCreateDto
    {
       
        public int CustomerId { get; set; }

        public string Title { get; set; }
        public RiskLevel RiskLevel { get; set; }
        public DateTime EvaluatedDate { get; set; }
        public string? Notes { get; set; }

        
    }
    public class  RiskHistoryQueryDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string Title { get; set; }
        public RiskLevel RiskLevel { get; set; }
        public DateTime EvaluatedDate { get; set; }
        public string? Notes { get; set; }

      

    }
    public class RiskHistoryUpdateDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public string Title { get; set; }
        public RiskLevel RiskLevel { get; set; }
        public DateTime EvaluatedDate { get; set; }
        public string? Notes { get; set; }

      

    }
}
