using BankRiskTracking.Entities.Response;
using BankRiskTracking.Entities.DTOs;

namespace BankRiskTracking.Entities.Interfaces
{
    public interface IRiskHistoryService
    {
        
        IResponse<IEnumerable<RiskHistoryQueryDto>> ListAll();
        IResponse<RiskHistroyCreateDto> Create(RiskHistroyCreateDto riskHistory);
        IResponse<RiskHistoryUpdateDto> Update(RiskHistoryUpdateDto riskHistoryUpdateDto);
        IResponse<RiskHistoryQueryDto> Delete(int id);

        IResponse<RiskHistoryQueryDto> GetByCustomerId(int customerId);

    }
}
