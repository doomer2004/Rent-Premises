using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentPremises.BLL.Services.TypeOfPremises;
using RentPremises.BLL.Services.TypeOfPremises.Interfaces;
using RentPremises.Common.Models.DTOs.TypeOfPremises;

namespace Rent_Premises.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TypeOfPremisesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITypeOfPremises _typeOfPremisesService;
    public TypeOfPremisesController(IMapper mapper, ITypeOfPremises typeOfPremisesService)
    {
        _mapper = mapper;
        _typeOfPremisesService = typeOfPremisesService;
    }
    [HttpGet]
    public async Task<IEnumerable<TypeOfPremisesDto>> GetAllAsync()
    {
        var entities = await _typeOfPremisesService.GetAllAsync();
        return entities;
    }
    
    [HttpGet("{id}")]
    public async Task<TypeOfPremisesDto> GetByIdAsync(Guid id)
    {
        var entity = await _typeOfPremisesService.GetByIdAsync(id);
        return entity;
    }
    
    [HttpPost]
    public async Task<bool> AddAsync(CreateTypeOfPremisesDto typeOfPremises)
    {
        await _typeOfPremisesService.AddAsync(typeOfPremises);
        return true;
    }
    
    [HttpDelete("{id}")]
    public async Task<bool> DeleteAsync(Guid id)
    {
        await _typeOfPremisesService.DeleteAsync(id);
        return true;
    }
    
    [HttpPut("{id}")]
    public async Task<bool> UpdateAsync(int id, TypeOfPremisesDto typeOfPremises)
    {
        await _typeOfPremisesService.UpdateAsync(typeOfPremises);
        return true;
    }
}