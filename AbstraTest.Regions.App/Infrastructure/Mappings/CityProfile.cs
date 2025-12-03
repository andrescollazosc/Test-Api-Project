using AbstraTest.Regions.App.Controllers.Cities.Dtos;
using AbstraTest.Regions.Core.Cities.Models;
using AutoMapper;

namespace AbstraTest.Regions.App.Infrastructure.Mappings;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<City, CityDto>();
        CreateMap<CreateCityDto, City>();
        CreateMap<UpdateCityDto, City>();
    }
}