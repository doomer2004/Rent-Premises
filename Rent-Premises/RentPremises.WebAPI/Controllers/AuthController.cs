using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentPremises.BLL.Services.Auth.Interfaces;
using RentPremises.Common.Constants;
using RentPremises.Common.Models.DTOs.User;

namespace Rent_Premises.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuth _authService;
    private readonly IMapper _mapper;
    
    public AuthController(IAuth authService)
    {
        _authService = authService;
    }
    
    [HttpPost("sign-up-as-renter")]
    public async Task<IActionResult> RegisterAsRenter(RegisterUserDTO userDto)
    {
        return Ok(await _authService.RegisterAsync(userDto, ApplicationRoles.Renter));
    }
    
    [HttpPost("sign-up-as-lessor")]
    public async Task<IActionResult> RegisterAsLessor(RegisterUserDTO userDto)
    {
        return Ok(await _authService.RegisterAsync(userDto, ApplicationRoles.Lessor));
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> Login(LoginUserDTO userDto)
    {
        return Ok(await _authService.LoginAsync(userDto));
    }
}