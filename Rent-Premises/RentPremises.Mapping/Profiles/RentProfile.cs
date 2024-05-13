using AutoMapper;
using RentPremises.Common.Models.DTOs.Rent;
using E = Rent_Premises.DAL.Entities;
namespace RentPremises.Mapping.Profiles;

public class RentProfile : Profile
{
    public RentProfile()
    {
        CreateMap<E.Premises, RentPremisesDto>()
            .ForMember(dest => dest.PremisesId, opt => opt.MapFrom(src => src.Id));
        CreateMap<RentPremisesDto, E.Premises>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PremisesId));
    }
}