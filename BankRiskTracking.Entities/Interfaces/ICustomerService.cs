using BankRiskTracking.Entities.DTOs;
using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Response;


namespace BankRiskTracking.Entities.Interfaces
{
    public interface ICustomerService
    {   
        IResponse<CustomerQueryDto> GetById(int id);
        IResponse<IEnumerable<CustomerQueryDto>> ListAll();
        IResponse<Customer>Create(CustomerCreateDto customer);
        IResponse<CustomerUpdateDto> Update(CustomerUpdateDto customerUpdateDto);
        IResponse<Customer> Delete(int id);

        Task<IResponse<CustomerQueryDto>> GetByName(string name);

    }
}
