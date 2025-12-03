namespace AbstraTest.Regions.App.Controllers.Cities.Dtos;

public record CreateCityDto
{
    public Guid CountryId { get; set; }
    public string Name { get; set; }
}