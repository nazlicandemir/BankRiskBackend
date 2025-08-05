using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRiskTracking.Entities.Entities
{
    public  class CustomerNote
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int CustomerId { get; set; }
        public string NoteText { get; set; }
        public DateTime createdDate { get; set; }

        public Customer Customer { get; set; }
    }
}
