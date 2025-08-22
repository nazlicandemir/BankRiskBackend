using BankRiskTracking.Entities.DTOs;
using BankRiskTracking.Entities.Response;


namespace BankRiskTracking.Entities.Interfaces
{
    public interface ITransaction
    {
        IResponse<TransactionQueryDto> Get(int id);
        IResponse<IEnumerable<TransactionQueryDto>> ListAll();
        IResponse<TransactionCreateDto> Create(TransactionCreateDto TransactionCreateDto);
       
        IResponse<TransactionQueryDto> Delete(int id);

        IResponse<TransactionQueryDto> GetByName(string name);


        IResponse<IEnumerable<TransactionQueryDto>> GetTransactionByCustomerId(int CustomerId);

        IResponse<TransactionUpdateDto> Update(TransactionUpdateDto transactionUpdateDto);

    }
}
