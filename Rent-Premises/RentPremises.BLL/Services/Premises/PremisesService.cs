using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rent_Premises.DAL.Repository.Interfaces;
using RentPremises.BLL.Services.Premises.Interfaces;
using RentPremises.Common.Models.DTOs.Premises;
using RentPremises.Common.Models.DTOs.TypeOfPremises;
using E = Rent_Premises.DAL.Entities;
namespace RentPremises.BLL.Services.Premises;

public class PremisesService : IPremises
{
    private readonly IRepository<E.Premises> _repository;
    private readonly IRepository<E.TypeOfPremises> _type;
    private readonly IMapper _mapper;
    public PremisesService(IRepository<E.Premises> repository, IMapper mapper, IRepository<E.TypeOfPremises> type)
    {
        _repository = repository;
        _mapper = mapper;
        _type = type;
    }

    public async Task<bool> AddAsync(CreatePremisesDto premisesDto)
    {
        try
        {
            var entity = _mapper.Map<E.Premises>(premisesDto);
            var type = await _type.SingleOrDefaultAsync(x => x.Id == premisesDto.TypeOfPremisesId);
            entity.IsAvailable = true;
            entity.Type = type;
            await _repository.InsertAsync(entity);
            return true;
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message);
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _repository.SingleOrDefaultAsync(x => x.Id == id);
        if (entity == null)
        {
            throw new ArgumentException("Premises not found");
        }
        
        await _repository.DeleteAsync(entity);
        return true;
    }

    public async Task<bool> UpdateAsync(PremisesDto premisesDto)
    {
        var entity = await _repository.SingleOrDefaultAsync(x => x.Id == premisesDto.Id);
        if (entity == null)
        {
            throw new ArgumentException("Premises not found");
        }

        _mapper.Map(premisesDto, entity);
        await _repository.UpdateAsync(entity);
        return true;
    }

    public async Task<IEnumerable<PremisesDto>> GetAllAsync()
    {
        var entities = await _repository.ToListAsync();
        return _mapper.Map<IEnumerable<PremisesDto>>(entities);
    }

    public async Task<PremisesDto> GetByIdAsync(Guid id)
    {
        var entity = await _repository.SingleOrDefaultAsync(x => x.Id == id);
        if (entity == null)
        {
            throw new ArgumentException("Premises not found");
        }
        
        return _mapper.Map<PremisesDto>(entity);
    }

    public async Task<List<PremisesDto>> GetAwaitableByIdAsync()
    {
        var entities = _repository.Where(r => r.IsAvailable );
        
        return _mapper.Map<List<PremisesDto>>(entities);
    }
    
}