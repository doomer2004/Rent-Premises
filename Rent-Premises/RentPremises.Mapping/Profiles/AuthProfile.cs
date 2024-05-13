using AutoMapper;
using Rent_Premises.DAL.Entities.Base;
using RentPremises.Common.Models.DTOs.Auth;
using RentPremises.Common.Models.DTOs.User;

namespace RentPremises.Mapping.Profiles;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<LoginUserDTO, AuthSuccessDto>();
        CreateMap<RegisterUserDTO, AuthSuccessDto>();
        CreateMap<RegisterUserDTO, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
    }
}