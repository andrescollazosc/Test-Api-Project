using AbstraTest.Regions.Core.Cities.Models;
using AbstraTest.Regions.Core.Countries.Modesl;
using AbstraTest.Regions.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AbstraTest.Regions.Data;

public class RegionDbContext : DbContext
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }

    public RegionDbContext(DbContextOptions<RegionDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new CountryMapping().Configure(modelBuilder.Entity<Country>());
        new CityMapping().Configure(modelBuilder.Entity<City>());
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    
        optionsBuilder.ConfigureWarnings(warnings => 
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
}