namespace AbstraTest.Regions.App.Controllers.Countries.Dtos;

public record UpdateCountryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}