using BankRiskTracking.Entities.DTOs;
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
        IResponse<CustomerQueryDto> GetById(int id);
        IResponse<IEnumerable<CustomerQueryDto>> ListAll();
        IResponse<Customer>Create(CustomerCreateDto customer);
        Task<IResponse<Customer>> Update(Customer customer);
        IResponse<Customer> Delete(int id);

        Task<IResponse<CustomerQueryDto>> GetByName(string name);

    }
}
