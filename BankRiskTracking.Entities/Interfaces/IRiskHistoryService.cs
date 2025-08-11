using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankRiskTracking.Entities.DTOs;

namespace BankRiskTracking.Entities.Interfaces
{
    public interface IRiskHistoryService
    {
        IResponse<RiskHistoryQueryDto> Get(int id);
        IResponse<IEnumerable<RiskHistoryQueryDto>> ListAll();
        IResponse<RiskHistroyCreateDto> Create(RiskHistroyCreateDto riskHistory);
        IResponse<RiskHistory> Update(RiskHistory riskHistory);
        IResponse<RiskHistoryQueryDto> Delete(int id);

        IResponse<RiskHistoryQueryDto> GetByName(string name);
    }
}
