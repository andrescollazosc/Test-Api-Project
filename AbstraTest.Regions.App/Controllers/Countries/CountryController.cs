using AbstraTest.Regions.App.Controllers.Countries.Dtos;
using AbstraTest.Regions.Core.Common.Data;
using AbstraTest.Regions.Core.Countries.Modesl;
using AbstraTest.Regions.Core.Countries.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AbstraTest.Regions.App.Controllers.Countries;

[ApiController]
[Route("/api/regions/[controller]")]
public class CountryController(
    ICountryService countryService,
    IUnitOfWork unitOfWork,
    IMapper mapper
) : Controller
{
    [HttpGet]
    [Route("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CountryDto>>> GetAll()
    {
        var countries = await countryService.GetAll();
        
        return Ok(mapper.Map<IEnumerable<CountryDto>>(countries));
    }
    
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CountryDto>> GetById(Guid id)
    {
        var country = await countryService.GetById(id);
        
        return Ok(mapper.Map<CountryDto>(country));
    }
    
    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> Create([FromBody] CreateCountryDto countryDto)
    {
        var country = mapper.Map<Country>(countryDto);
        await countryService.Create(country);
        
        await unitOfWork.CommitAsync();
        
        return Created();
    }
    
    [HttpPost]
    [Route("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CountryDto>> Update([FromBody] UpdateCountryDto cityDto)
    {
        var country = mapper.Map<Country>(cityDto);
        await countryService.Update(country);
        
        await unitOfWork.CommitAsync();
        
        return Ok(mapper.Map<CountryDto>(country));
    }
    
    [HttpDelete]
    [Route("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await countryService.Delete(id);
        
        await unitOfWork.CommitAsync();
        
        return Ok();
    }
}