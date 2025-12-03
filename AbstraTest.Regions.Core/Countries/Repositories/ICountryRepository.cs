using AbstraTest.Regions.Core.Common;
using AbstraTest.Regions.Core.Countries.Modesl;

namespace AbstraTest.Regions.Core.Countries.Repositories;

public interface ICountryRepository : IGenericRepository<Country>
{
    Task<Country?> GetById(Guid id);
    Task<Country?> GetByName(string name);
}