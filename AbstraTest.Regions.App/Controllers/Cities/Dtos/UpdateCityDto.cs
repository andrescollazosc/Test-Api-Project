namespace AbstraTest.Regions.App.Controllers.Cities.Dtos;

public record UpdateCityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}