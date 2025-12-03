using AbstraTest.Regions.Core.Cities.Models;
using AbstraTest.Regions.Core.Common;

namespace AbstraTest.Regions.Core.Cities.Reporistories;

public interface ICityRepository : IGenericRepository<City>
{
    Task<City?> GetById(Guid id);
}