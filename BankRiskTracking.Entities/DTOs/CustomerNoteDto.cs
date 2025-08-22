using BankRiskTracking.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRiskTracking.Entities.DTOs
{
    public class CustomerNoteCreateDto
    {
        

        public string Name { get; set; }
        public int CustomerId { get; set; }
        public string NoteText { get; set; }
        public DateTime CreatedDate { get; set; }

        
    }

    public class CustomerNoteQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public string NoteText { get; set; }
        public DateTime CreatedDate { get; set; }
       
    }

    public class CustomerNoteUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CustomerId { get; set; }
        public string? NoteText { get; set; }
        public DateTime? CreatedDate { get; set; }
       
    }
}
