using AbstraTest.Regions.Core.Cities.Models;

namespace AbstraTest.Regions.Core.Cities.Services;

public interface ICityService
{
    Task<IEnumerable<City>> GetAll();
    Task<City?> GetById(Guid id);
    Task Create(City city);
    Task Update(City city);
    Task Delete(Guid id);
}