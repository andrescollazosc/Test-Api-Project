using AbstraTest.Regions.App.Controllers.Countries.Dtos;
using AbstraTest.Regions.Core.Countries.Modesl;
using AutoMapper;

namespace AbstraTest.Regions.App.Infrastructure.Mappings;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<Country, CountryDto>();
        CreateMap<CreateCountryDto, Country>();
        CreateMap<UpdateCountryDto, Country>();
    }
}