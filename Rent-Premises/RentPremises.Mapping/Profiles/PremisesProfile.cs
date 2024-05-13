using AutoMapper;
using RentPremises.Common.Models.DTOs.Premises;
using RentPremises.Common.Models.DTOs.TypeOfPremises;
using E = Rent_Premises.DAL.Entities;
namespace RentPremises.Mapping.Profiles;

public class PremisesProfile : Profile
{
    public PremisesProfile()
    {
        CreateMap<E.Premises, PremisesDto>();
        CreateMap<PremisesDto, E.Premises>();
        CreateMap<CreatePremisesDto, E.Premises>();
        CreateMap<E.TypeOfPremises, TypeOfPremisesDto>();
        CreateMap<CreateTypeOfPremisesDto, E.TypeOfPremises>();
    }
}