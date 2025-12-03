namespace AbstraTest.Regions.App.Controllers.Cities.Dtos;

public record CityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
}