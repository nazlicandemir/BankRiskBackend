using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Customer  Customer { get; set; }
    }
}
