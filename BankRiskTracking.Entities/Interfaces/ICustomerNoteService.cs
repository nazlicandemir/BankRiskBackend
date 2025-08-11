using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRiskTracking.Entities.Interfaces
{
    public interface ICustomerNoteService
    {
        IResponse<CustomerNote> GetById(int id);
        IResponse<IEnumerable<CustomerNote>> ListAll(); 
        Task<IResponse<CustomerNote>> Create(CustomerNote customerNote);
        Task<IResponse<CustomerNote>> Update(CustomerNote customerNote);
        Task<IResponse<CustomerNote>> Delete(int id);
        IResponse<CustomerNote> GetByName(string name);
    }

}
