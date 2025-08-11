using BankRiskTracking.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRiskTracking.Entities.DTOs
{
    public  class TransactionCreateDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }

        public string TrancationType { get; set; }

        public string? Description { get; set; }

        public Customer Customer { get; set; }

        public string Title { get; set; }
    }
    public class TransactionQueryDto
    {
        public int Id { get; set; }
        
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }

        public string TrancationType { get; set; }

        public string? Description { get; set; }

        public Customer Customer { get; set; }

        public string Title { get; set; }
    }
}
