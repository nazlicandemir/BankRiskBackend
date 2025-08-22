using BankRiskTracking.Entities.Entities;

namespace BankRiskTracking.Entities.DTOs
{
    public class CustomerCreateDto
    {


        public string FullName { get; set; }

        public string IdentityNumber { get; set; }
        public decimal TotalDebt { get; set; }
        public int CreditScore { get; set; }

        public RiskLevel RiskLevel { get; set; }

        public string? Notes { get; set; }

        public DateTime LastEvaluated { get; set; }

    }
    
    public class CustomerQueryDto
    {
        public string FullName { get; set; }
        public int Id { get; set; }

        public string IdentityNumber { get; set; }
        public decimal TotalDebt { get; set; }
        public int CreditScore { get; set; }

        public RiskLevel RiskLevel { get; set; }

        public string? Notes { get; set; }

        public DateTime LastEvaluated { get; set; }
    }

    public class CustomerUpdateDto
    {
        public string FullName { get; set; }
        public int Id { get; set; }

        public string IdentityNumber { get; set; }
        public decimal TotalDebt { get; set; }
        public int CreditScore { get; set; }

        public RiskLevel RiskLevel { get; set; }

        public string? Notes { get; set; }

        public DateTime LastEvaluated { get; set; }
    }


}
