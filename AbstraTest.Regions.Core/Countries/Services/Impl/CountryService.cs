using AbstraTest.Regions.Core.Common.Exceptions;
using AbstraTest.Regions.Core.Countries.Modesl;
using AbstraTest.Regions.Core.Countries.Repositories;
using FluentValidation;

namespace AbstraTest.Regions.Core.Countries.Services.Impl;

public class CountryService(
    ICountryRepository countryRepository,
    IValidator<Country> validator) : ICountryService
{
    public async Task<IEnumerable<Country>> GetAll()
    {
        var countries = await countryRepository.GetAllAsync();

        return countries;
    }

    public async Task<Country?> GetById(Guid id)
    {
        var country = await countryRepository.GetById(id);

        return country ?? throw new NotFoundException($"Country with id {id} not found");
    }

    public async Task<Country?> GetByName(string name)
    {
        var country = await countryRepository.GetByName(name);

        return country;
    }

    public async Task Create(Country country)
    {
        var validationResult = await validator.ValidateAsync(country);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await countryRepository.AddAsync(country);
    }

    public async Task Update(Country country)
    {
        var validationResult = await validator.ValidateAsync(country);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var countryToUpdate = await GetById(country.Id);

        countryToUpdate!.Name = country.Name;
        countryToUpdate.CreatedDate = DateTime.UtcNow;

        countryRepository.Update(countryToUpdate);
    }

    public async Task Delete(Guid id)
    {
        var country = await GetById(id);

        countryRepository.Remove(country!);
    }
}