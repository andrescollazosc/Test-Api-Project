using AbstraTest.Regions.Core.Cities.Models;
using AbstraTest.Regions.Core.Cities.Reporistories;
using AbstraTest.Regions.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace AbstraTest.Regions.Data.Repositories;

public class CityRepository(
    RegionDbContext dbContext
)
    : GenericEfRepository<City, RegionDbContext>(dbContext), ICityRepository
{
    private readonly RegionDbContext _dbContext = dbContext;

    public async Task<City?> GetById(Guid id)
    {
        var city = await _dbContext.Cities.SingleOrDefaultAsync(x => x.Id == id);

        return city;
    }
}