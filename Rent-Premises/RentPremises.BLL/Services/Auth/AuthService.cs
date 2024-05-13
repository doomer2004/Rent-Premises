using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Rent_Premises.DAL.Entities;
using Rent_Premises.DAL.Entities.Base;
using Rent_Premises.DAL.Repository.Interfaces;
using RentPremises.BLL.Extensions;
using RentPremises.BLL.Services.Auth.Base;
using RentPremises.BLL.Services.Auth.Interfaces;
using RentPremises.Common.Constants;
using RentPremises.Common.Models.Configs;
using RentPremises.Common.Models.DTOs.Auth;
using RentPremises.Common.Models.DTOs.User;

namespace RentPremises.BLL.Services.Auth;

public class AuthService : AuthBase<LoginUserDTO>, IAuth
{
    private readonly IMapper _mapper;
    private readonly IRepository<Renter> _renterRepository;
    private readonly IRepository<Lessor> _lessorRepository;

    public AuthService(
        JwtConfig jwtConfig,
        UserManager<User> userManager, IMapper mapper, IRepository<Renter> renterRepository, 
        IRepository<Lessor> lessorRepository)
        : base(jwtConfig, userManager)
    {
        _mapper = mapper;
        _renterRepository = renterRepository;
        _lessorRepository = lessorRepository;
    }

    public async Task<AuthSuccessDto> LoginAsync(LoginUserDTO loginUserDTO)
    {
        var userEntity = await _userManager.FindByEmailAsync(loginUserDTO.Email);
        if (userEntity == null)
        {
            throw new Exception("User not found");
        }
        
        var res = await _userManager.CheckPasswordAsync(userEntity, loginUserDTO.Password);
        if (!res)
        {
            throw new Exception("Invalid credentials");
        }
        
        return await GenerateAuthSuccessDTO(userEntity);
    }
    
    public async Task<AuthSuccessDto> RegisterAsync(RegisterUserDTO user, string role)
    {
        var userEntity = await _userManager.FindByEmailAsync(user.Email);
        if (userEntity != null)
            throw new Exception("User with this email already exists");

        userEntity = _mapper.Map<User>(user);
        var createdUser = await _userManager.CreateAsync(userEntity, user.Password);
        if (!createdUser.Succeeded)
            throw new Exception("Failed to create user");

        var roleAdded = await _userManager.SetUserRoleAsync(userEntity, role);
        if (!roleAdded.Succeeded)
            throw new Exception("Failed to create user");

        if (ApplicationRoles.Lessor == role)
        {
            await _lessorRepository.InsertAsync(new Lessor()
            {
                UserId = userEntity.Id
            });
        }
        
        if(ApplicationRoles.Renter == role)
        {
            await _renterRepository.InsertAsync(new Renter()
            {
                UserId = userEntity.Id
            });
        }
        return await GenerateAuthSuccessDTO(userEntity);
    }


}