using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRiskTracking.Entities.Entities
{
    public enum RiskLevel
    {
        Low,
        Medium, 
        High

    }

    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string IdentityNumber {  get; set; }
        public decimal TotalDebt {  get; set; }
        public int CreditScore {  get; set; }

        public RiskLevel riskLevel { get; set; }

        public string? Notes {  get; set; }

        public DateTime LastEvaluated {  get; set; }

    }
}
