using AbstraTest.Regions.Core.Cities.Models;
using AbstraTest.Regions.Core.Cities.Reporistories;
using AbstraTest.Regions.Core.Common.Exceptions;
using FluentValidation;

namespace AbstraTest.Regions.Core.Cities.Services.Impl;

public class CityService(
    ICityRepository cityRepository,
    IValidator<City> validator) : ICityService
{
    public async Task<IEnumerable<City>> GetAll()
    {
        var cities = await cityRepository.GetAllAsync();
        
        return cities;
    }

    public async Task<City?> GetById(Guid id)
    {
        var city = await cityRepository.GetById(id);

        return city ?? throw new NotFoundException($"City with id {id} not found");
    }

    public async Task Create(City city)
    {
        var validationResult = await validator.ValidateAsync(city);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await  cityRepository.AddAsync(city);
    }

    public async Task Update(City city)
    {
        var validationResult = await validator.ValidateAsync(city);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var cityToUpdate = await GetById(city.Id);
        
        cityToUpdate!.Name = city.Name;
        cityToUpdate.CreatedDate = DateTime.UtcNow;
        
        cityRepository.Update(cityToUpdate);
    }

    public async Task Delete(Guid id)
    {
        var city = await GetById(id);
        
        cityRepository.Remove(city!);
    }
}