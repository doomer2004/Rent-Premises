using AutoMapper;
using RentPremises.Common.Models.DTOs.TypeOfPremises;
using E = Rent_Premises.DAL.Entities;
namespace RentPremises.Mapping.Profiles;

public class TypeOfPremisesProfile : Profile
{
    public TypeOfPremisesProfile()
    {
        CreateMap<E.TypeOfPremises, TypeOfPremisesDto>();
        CreateMap<CreateTypeOfPremisesDto, E.TypeOfPremises>();
    }
}