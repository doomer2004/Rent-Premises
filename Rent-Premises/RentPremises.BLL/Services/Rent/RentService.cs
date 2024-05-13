using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rent_Premises.DAL.Repository.Interfaces;
using E = Rent_Premises.DAL.Entities;
using RentPremises.BLL.Services.Rent.Interfaces;
using RentPremises.Common.Models.DTOs.Rent;

namespace RentPremises.BLL.Services.Rent;

public class RentService : IRent
{
    private readonly IMapper _mapper;
    private readonly IRepository<E.Premises> _repository;
    
    public RentService(IRepository<E.Premises> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<bool> RentPremisesAsync(RentPremisesDto rentPremisesDto)
    {
        var entity = await _repository.FirstOrDefaultAsync(x => x.Id == rentPremisesDto.PremisesId);
        if (entity == null)
        {
            throw new ArgumentException("Premises not found");
        }
        
        entity.Id = rentPremisesDto.PremisesId;
        entity.IsAvailable = false;
        await _repository.UpdateAsync(entity);
        return true;
    }

    public async Task<List<RentPremisesDto>> GetAllAsync()
    {
        return await _repository
            .Where(x => x.IsAvailable)
            .Select(x => _mapper.Map<RentPremisesDto>(x))
            .ToListAsync();
    }

    public async Task<RentPremisesDto> GetByIdAsync(Guid id)
    {
        var entity = await _repository.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
        {
            throw new ArgumentException("Premises not found");
        }
        
        return _mapper.Map<RentPremisesDto>(entity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _repository.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
        {
            throw new ArgumentException("Premises not found");
        }

        if (entity.IsAvailable == true)
        {
            throw new ArgumentException("Premises is already available");
        }
        
        entity.IsAvailable = true;
        entity.Renter = null;
        await _repository.UpdateAsync(entity);
        return true;
    }

    public async Task<bool> UpdateAsync(RentPremisesDto rentPremisesDto)
    {
        var entity = await _repository.FirstOrDefaultAsync(x => x.Id == rentPremisesDto.PremisesId);
        if (entity == null)
        {
            throw new ArgumentException("Premises not found");
        }
        
        entity.IsAvailable = false;
        entity.RenterId = rentPremisesDto.RenterId;
        await _repository.UpdateAsync(entity);
        return true;
    }

    public async Task<List<RentPremisesDto>> GetPremisesByLessorIdAsync(Guid id)
    {
        return await _repository
            .Where(x => x.LessorId == id)
            .Select(x => _mapper.Map<RentPremisesDto>(x))
            .ToListAsync();
    }
}