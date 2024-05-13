using RentPremises.Common.Models.DTOs.TypeOfPremises;

namespace RentPremises.BLL.Services.TypeOfPremises.Interfaces;

public interface ITypeOfPremises
{
    Task<bool> AddAsync(CreateTypeOfPremisesDto typeOfPremises);
    
    Task<bool> DeleteAsync(Guid id);
    
    Task<bool> UpdateAsync(TypeOfPremisesDto typeOfPremises);
    
    Task<IEnumerable<TypeOfPremisesDto>> GetAllAsync();
    
    Task<TypeOfPremisesDto> GetByIdAsync(Guid id);
}