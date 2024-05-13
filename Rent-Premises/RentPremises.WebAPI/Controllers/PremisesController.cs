using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentPremises.BLL.Services.Premises.Interfaces;
using RentPremises.Common.Models.DTOs.Premises;
using RentPremises.Common.Models.DTOs.TypeOfPremises;

namespace Rent_Premises.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PremisesController : ControllerBase
{
    private readonly IPremises _premisesService;
    private readonly IMapper _mapper;
    
    public PremisesController(IPremises premisesService, IMapper mapper)
    {
        _premisesService = premisesService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var premises = await _premisesService.GetAllAsync();
        return Ok(premises);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var premises = await _premisesService.GetByIdAsync(id);
        return Ok(premises);
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(CreatePremisesDto premisesDto)
    {
        await _premisesService.AddAsync(premisesDto);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _premisesService.DeleteAsync(id);
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(PremisesDto premisesDto)
    {
        await _premisesService.UpdateAsync(premisesDto);
        return Ok();
    }
    
    
    [HttpGet("GetAwaitablePremiseses")]
    public async Task<IActionResult> GetAwaitableByIdAsync()
    {
        var premises = await _premisesService.GetAwaitableByIdAsync();
        return Ok(premises);
    }
}