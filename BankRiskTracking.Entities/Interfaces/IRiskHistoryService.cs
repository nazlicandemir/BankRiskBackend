using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRiskTracking.Entities.Interfaces
{
    public interface IRiskHistoryService
    {
        IResponse<RiskHistory> Get(int id);
        IResponse<IEnumerable<RiskHistory>> ListAll();
        IResponse<RiskHistory> Create(Customer customer);
        IResponse<RiskHistory> Update(Customer customer);
        IResponse<RiskHistory> Delete(int id);

        IResponse<RiskHistory> GetByName(string name);
    }
}
