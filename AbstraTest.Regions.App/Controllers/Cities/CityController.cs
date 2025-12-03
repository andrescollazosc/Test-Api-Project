using AbstraTest.Regions.App.Controllers.Cities.Dtos;
using AbstraTest.Regions.Core.Cities.Models;
using AbstraTest.Regions.Core.Cities.Services;
using AbstraTest.Regions.Core.Common.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AbstraTest.Regions.App.Controllers.Cities;

[ApiController]
[Route("/api/regions/[controller]")]
public class CityController(
    ICityService cityService,
    IUnitOfWork unitOfWork,
    IMapper mapper
) : Controller
{
    [HttpGet]
    [Route("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CityDto>>> GetAll()
    {
        var cities = await cityService.GetAll();
        
        return Ok(mapper.Map<IEnumerable<CityDto>>(cities));
    }
    
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CityDto>> GetById(Guid id)
    {
        var city = await cityService.GetById(id);
        
        return Ok(mapper.Map<CityDto>(city));
    }
    
    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> Create([FromBody] CreateCityDto cityDto)
    {
        var city = mapper.Map<City>(cityDto);
        await cityService.Create(city);
        
        await unitOfWork.CommitAsync();
        
        return Created();
    }
    
    [HttpPost]
    [Route("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CityDto>> Update([FromBody] UpdateCityDto cityDto)
    {
        var city = mapper.Map<City>(cityDto);
        await cityService.Update(city);
        
        await unitOfWork.CommitAsync();
        
        return Ok(mapper.Map<CityDto>(city));
    }
    
    [HttpDelete]
    [Route("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await cityService.Delete(id);
        
        await unitOfWork.CommitAsync();
        
        return Ok();
    }
}