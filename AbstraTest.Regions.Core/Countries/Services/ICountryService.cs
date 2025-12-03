using AbstraTest.Regions.Core.Countries.Modesl;

namespace AbstraTest.Regions.Core.Countries.Services;

public interface ICountryService
{
    Task<IEnumerable<Country>> GetAll();
    Task<Country?> GetById(Guid id);
    Task<Country?> GetByName(string name);
    Task Create(Country country);
    Task Update(Country country);
    Task Delete(Guid id);
}