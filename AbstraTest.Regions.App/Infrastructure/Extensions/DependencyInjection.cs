using AbstraTest.Regions.Core.Cities.Models;
using AbstraTest.Regions.Core.Cities.Reporistories;
using AbstraTest.Regions.Core.Cities.Services;
using AbstraTest.Regions.Core.Cities.Services.Impl;
using AbstraTest.Regions.Core.Cities.Validators;
using AbstraTest.Regions.Core.Common.Data;
using AbstraTest.Regions.Core.Countries.Modesl;
using AbstraTest.Regions.Core.Countries.Repositories;
using AbstraTest.Regions.Core.Countries.Services;
using AbstraTest.Regions.Core.Countries.Services.Impl;
using AbstraTest.Regions.Core.Countries.Validators;
using AbstraTest.Regions.Data;
using AbstraTest.Regions.Data.Repositories;
using FluentValidation;
using Npgsql;
using Microsoft.EntityFrameworkCore;

namespace AbstraTest.Regions.App.Infrastructure.Extensions;

public static class DependencyInjection
{
    private static IServiceCollection Services { get; set; } = null!;
    private static IConfiguration Configuration { get; set; } = null!;

    public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        Services = services;
        Configuration = configuration;

        RegisterDataBases();
        RegisterRepositories();
        RegisterServices();
        RegisterValidators();
    }

    private static void RegisterDataBases()
    {
        var connectionString = Configuration.GetConnectionString("RegionsConnection");
        
        Services.AddSingleton(_ =>
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString)
                .EnableDynamicJson();

            var dataSource = dataSourceBuilder.Build();

            return dataSource;
        });
        
        Services.AddDbContext<RegionDbContext>((provider, options) =>
        {
            options.UseNpgsql(provider.GetRequiredService<NpgsqlDataSource>());
        });
        
        Services.AddScoped<IUnitOfWork, EfUnitOfWork<RegionDbContext>>();
    }

    private static void RegisterRepositories()
    {
        Services.AddScoped<ICityRepository, CityRepository>();
        Services.AddScoped<ICountryRepository, CountryRepository>();
    }
    
    private static void RegisterServices()
    {
        Services.AddAutoMapper(
            typeof(IInvoicingAppAssemblyScanningMarker)
        );
        
        Services.AddScoped<ICityService, CityService>();
        Services.AddScoped<ICountryService, CountryService>();
    }

    private static void RegisterValidators()
    {
        Services.AddSingleton<IValidator<City>, CityValidator>();
        Services.AddSingleton<IValidator<Country>, CountryValidator>();
    }

}