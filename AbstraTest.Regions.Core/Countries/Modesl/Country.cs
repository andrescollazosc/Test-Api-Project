using AbstraTest.Regions.Core.Cities.Models;

namespace AbstraTest.Regions.Core.Countries.Modesl;

public class Country
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public ICollection<City> Cities { get; set; } = [];
}