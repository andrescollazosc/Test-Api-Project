using AbstraTest.Regions.Core.Countries.Modesl;

namespace AbstraTest.Regions.Core.Cities.Models;

public class City
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public Guid CountryId { get; set; }
    public Country Country { get; set; } = null!;
}