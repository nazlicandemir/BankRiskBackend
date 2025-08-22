using BankRiskTracking.Entities.DTOs;
using BankRiskTracking.Entities.Response;

namespace BankRiskTracking.Entities.Interfaces
{
    public interface IUserService
    {
        IResponse<UserCreateDto> CreateUser(UserCreateDto user);
        IResponse<string > LoginUser(UserLoginDto user);
    }
}
