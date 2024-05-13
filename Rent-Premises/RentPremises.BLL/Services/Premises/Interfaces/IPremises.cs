using RentPremises.Common.Models.DTOs.Premises;
using RentPremises.Common.Models.DTOs.TypeOfPremises;

namespace RentPremises.BLL.Services.Premises.Interfaces;

public interface IPremises
{
    Task<bool> AddAsync(CreatePremisesDto premisesDto);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> UpdateAsync(PremisesDto premisesDto);
    Task<IEnumerable<PremisesDto>> GetAllAsync();
    Task<PremisesDto> GetByIdAsync(Guid id);
    Task<List<PremisesDto>> GetAwaitableByIdAsync();
}