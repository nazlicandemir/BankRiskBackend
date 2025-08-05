using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace BankRiskTracking.Entities.Interfaces
{
    public interface ICustomerService
    {
        IResponse<Customer> Get(int id);
        IResponse<IEnumerable<Customer>> ListAll();
        Task<IResponse<Customer>>Create(Customer customer);
        Task<IResponse<Customer>> Update(Customer customer);
        Task<IResponse<Customer>> Delete(int id);

        Task<IResponse<Customer>> GetByName(string name);

    }
}
