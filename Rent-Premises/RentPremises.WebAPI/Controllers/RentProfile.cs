using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rent_Premises.DAL.Entities;
using RentPremises.BLL.Services.Rent.Interfaces;
using RentPremises.Common.Constants;
using RentPremises.Common.Models.DTOs.Rent;

namespace Rent_Premises.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class RentProfile : ControllerBase
{
    private readonly IRent _rent;
    private readonly IMapper _mapper;
    
    public RentProfile(IRent rent, IMapper mapper)
    {
        _rent = rent;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<RentPremisesDto>> GetAllAsync()
    {
        var entities = await _rent.GetAllAsync();
        return _mapper.Map<IEnumerable<RentPremisesDto>>(entities);
    }
    
    [HttpGet("{id}")]
    public async Task<RentPremisesDto> GetByIdAsync(Guid id)
    {
        var entity = await _rent.GetByIdAsync(id);
        return _mapper.Map<RentPremisesDto>(entity);
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = ApplicationRoles.Lessor)]
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _rent.DeleteAsync(id);
    }
    
    [HttpPut]
    [Authorize(Roles = ApplicationRoles.Lessor)]
    public async Task<bool> UpdateAsync(RentPremisesDto rentPremisesDto)
    {
        return await _rent.UpdateAsync(rentPremisesDto);
    }
    
    [HttpPost]
    [Authorize(Roles = ApplicationRoles.Renter)]
    public async Task<bool> RentPremisesAsync(RentPremisesDto rentPremisesDto)
    {
        return await _rent.RentPremisesAsync(rentPremisesDto);
    }
    
    [HttpGet("GetPremisesByLessorId/{id}")]
    public async Task<List<RentPremisesDto>> GetPremisesByLessorIdAsync(Guid id)
    {
        return await _rent.GetPremisesByLessorIdAsync(id);
    }
}