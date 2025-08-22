using BankRiskTracking.Entities.DTOs;
using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Response;

namespace BankRiskTracking.Entities.Interfaces
{
    public interface ICustomerNoteService
    {
        IResponse<CustomerNoteQueryDto> GetById(int id);
        IResponse<IEnumerable<CustomerNoteQueryDto>> ListAll(); 
        IResponse<CustomerNoteQueryDto> Create(CustomerNoteCreateDto customerNote);
        IResponse<CustomerNoteUpdateDto> Update(CustomerNoteUpdateDto dto);
        IResponse<CustomerNoteQueryDto> Delete(int id);
        IResponse<CustomerNoteQueryDto> GetByName(string name);
    }

}
