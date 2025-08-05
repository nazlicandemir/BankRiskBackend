using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRiskTracking.Entities.Interfaces
{
    public interface ITransaction
    {
        IResponse<Transaction> Get(int id);
        IResponse<IEnumerable<Transaction>> ListAll();
        IResponse<Transaction> Create(Customer customer);
        Task<IResponse<Transaction>> Update(Customer customer);
        IResponse<Transaction> Delete(int id);

        IResponse<Transaction> GetByName(string name);

    }
}
