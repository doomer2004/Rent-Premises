using RentPremises.Common.Models.DTOs.Auth;
using RentPremises.Common.Models.DTOs.User;

namespace RentPremises.BLL.Services.Auth.Interfaces;

public interface IAuth
{
    Task<AuthSuccessDto> LoginAsync(LoginUserDTO user);
    Task<AuthSuccessDto> RegisterAsync(RegisterUserDTO user, string role);
}