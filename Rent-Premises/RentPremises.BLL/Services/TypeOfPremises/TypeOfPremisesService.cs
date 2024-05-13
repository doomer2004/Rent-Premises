using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rent_Premises.DAL.Repository.Interfaces;
using RentPremises.BLL.Services.TypeOfPremises.Interfaces;
using RentPremises.Common.Models.DTOs.TypeOfPremises;
using E = Rent_Premises.DAL.Entities;
namespace RentPremises.BLL.Services.TypeOfPremises;

public class TypeOfPremisesService : ITypeOfPremises
{
    private readonly IRepository<E.TypeOfPremises> _repository;
    private readonly IMapper _mapper;

    public TypeOfPremisesService(IRepository<E.TypeOfPremises> typeOfPremisesRepository, IMapper mapper)
    {
        _repository = typeOfPremisesRepository;
        _mapper = mapper;
    }
    
    public async Task<bool> AddAsync(CreateTypeOfPremisesDto typeOfPremisesDto)
    {
        try
        {
            var entity = _mapper.Map<E.TypeOfPremises>(typeOfPremisesDto);
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
            throw new ArgumentException("Type of premises not found");
        }
        
        await _repository.DeleteAsync(entity);
        return true;
    }

    public async Task<bool> UpdateAsync(TypeOfPremisesDto typeOfPremises)
    {
        var entity = await _repository.SingleOrDefaultAsync(x => x.Id == typeOfPremises.Id);
        if (entity == null)
        {
            throw new ArgumentException("Type of premises not found");
        }
        
        _mapper.Map(typeOfPremises, entity);
        await _repository.UpdateAsync(entity);
        return true;
    }

    public async Task<IEnumerable<TypeOfPremisesDto>> GetAllAsync()
    {
        var entities = await _repository.ToListAsync();
        return _mapper.Map<IEnumerable<TypeOfPremisesDto>>(entities);
    }

    public async Task<TypeOfPremisesDto> GetByIdAsync(Guid id)
    {
        var entity = await _repository.SingleOrDefaultAsync(x => x.Id == id);
        if (entity == null)
        {
            throw new ArgumentException("Type of premises not found");
        }
        
        return _mapper.Map<TypeOfPremisesDto>(entity);
    }
}