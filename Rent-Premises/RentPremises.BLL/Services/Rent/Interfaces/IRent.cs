using RentPremises.Common.Models.DTOs.Rent;
using E = Rent_Premises.DAL.Entities;
namespace RentPremises.BLL.Services.Rent.Interfaces;

public interface IRent
{
    Task<bool> RentPremisesAsync(RentPremisesDto rentPremisesDto);
    
    Task<List<RentPremisesDto>> GetAllAsync();
    
    Task<RentPremisesDto> GetByIdAsync(Guid id);
    
    Task<bool> DeleteAsync(Guid id);
    
    Task<bool> UpdateAsync(RentPremisesDto rentPremisesDto);
    
    Task<List<RentPremisesDto>> GetPremisesByLessorIdAsync(Guid id);
}