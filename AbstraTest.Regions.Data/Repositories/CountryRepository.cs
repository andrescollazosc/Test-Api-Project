using AbstraTest.Regions.Core.Countries.Modesl;
using AbstraTest.Regions.Core.Countries.Repositories;
using AbstraTest.Regions.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace AbstraTest.Regions.Data.Repositories;

public class CountryRepository(
    RegionDbContext dbContext
)
    : GenericEfRepository<Country, RegionDbContext>(dbContext), ICountryRepository
{
    private readonly RegionDbContext _dbContext = dbContext;

    public async Task<Country?> GetById(Guid id)
    {
        var country = await _dbContext.Countries.SingleOrDefaultAsync(x => x.Id == id);
        
        return country;
    }

    public async Task<Country?> GetByName(string name)
    {
        var country = await _dbContext.Countries.SingleOrDefaultAsync(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
        
        return country;
    }
}