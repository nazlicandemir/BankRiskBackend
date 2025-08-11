using BankRiskTracking.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRiskTracking.Entities.DTOs
{
    public class CustomerCreateDto
    {
      
        public string FullName { get; set; }
        
    }
    
    public class CustomerQueryDto
    {
        public string FullName { get; set; }
    }

}
