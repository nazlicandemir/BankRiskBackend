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
    public interface ITransaction
    {
        IResponse<TransactionQueryDto> Get(int id);
        IResponse<IEnumerable<TransactionQueryDto>> ListAll();
        IResponse<TransactionCreateDto> Create(TransactionCreateDto TransactionCreateDto);
        Task<IResponse<Transaction>> Update(Customer customer);
        IResponse<TransactionQueryDto> Delete(int id);

        IResponse<TransactionQueryDto> GetByName(string name);

    }
}
