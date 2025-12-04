using AbstraTest.Regions.Core.Cities.Models;
using AbstraTest.Regions.Core.Cities.Reporistories;
using AbstraTest.Regions.Core.Cities.Services;
using AbstraTest.Regions.Core.Cities.Services.Impl;
using AbstraTest.Regions.Core.Common.Exceptions;
using FluentValidation;
using Moq;

namespace AbstraTest.Regions.Core.Tests.Services;

[TestFixture]
public class CityServiceTest
{
    private Mock<ICityRepository> _cityRepositoryMock = null!;
    private InlineValidator<City> _validator = null!;
    private ICityService _cityService = null!;

    [SetUp]
    public void SetUp()
    {
        _cityRepositoryMock = new Mock<ICityRepository>();
        _validator = new InlineValidator<City>();
        _validator.RuleFor(c => c.Name)
             .NotEmpty().WithMessage("Name is required");
        
        _cityService = new CityService(
            _cityRepositoryMock.Object, _validator);
    }

    [Test]
    public async Task GetAll_ShouldReturnAllCities()
    {
        // Arrange
        var expected = new List<City>
        {
            new() { Id = Guid.NewGuid(), Name = "Bogotá", CountryId = Guid.NewGuid() },
            new() { Id = Guid.NewGuid(), Name = "Medellín", CountryId = Guid.NewGuid() }
        };

        _cityRepositoryMock.Setup(r => r.GetAllAsync(CancellationToken.None)).ReturnsAsync(expected);

        // Act
        var result = await _cityService.GetAll();

        // Assert
        var enumerable = result.ToList();
        Assert.That(enumerable, Is.Not.Null);
        Assert.That(enumerable.Count(), Is.EqualTo(2));
        Assert.That(enumerable.First().Name, Is.EqualTo("Bogotá"));
    }

    [Test]
    public async Task GetById_ShouldReturnBogotaCity()
    {
        // Arrange
        Guid id = Guid.Parse("f6fa776b-bfa8-4301-8121-52118f4bc8da");
        var expected = new City
        {
            Id = Guid.Parse("f6fa776b-bfa8-4301-8121-52118f4bc8da"), Name = "Bogotá", CountryId = Guid.NewGuid()
        };

        _cityRepositoryMock.Setup(r => r.GetById(id)).ReturnsAsync(expected);

        // Act
        var city = await _cityService.GetById(id);

        // Assert

        Assert.That(city, Is.Not.Null);
        Assert.That(city.Name, Is.EqualTo("Bogotá"));
    }

    [Test]
    public async Task GetById_ShouldReturnNotFoundException()
    {
        // Arrange
        Guid id = Guid.Parse("f6fa776b-bfa8-4301-8121-52118f4bc8da");

        City cityExpected = null!;

        _cityRepositoryMock.Setup(r => r.GetById(id)).ReturnsAsync(cityExpected);

        // Act

        // Assert
        var ex = Assert.ThrowsAsync<NotFoundException>(async () => { await _cityService.GetById(id); });

        Assert.That(ex.Message, Is.EqualTo($"City with id {id} not found"));
    }

    [Test]
    public async Task Create_ShouldValidateAndSaveCity_WhenValid()
    {
        // Arrange
        var city = new City
        {
            Name = "Bogotá",
            CountryId = Guid.NewGuid()
        };

        // Act
        await _cityService.Create(city);

        // Assert
        Assert.That(city.Id, Is.Not.EqualTo(Guid.Empty));
    }

    [Test]
    public async Task Create_ShouldThrowValidationException_WhenNameIsEmpty()
    {
        // Arrange
        var city = new City
        {
            Name = string.Empty,
            CountryId = Guid.NewGuid()
        };

        
        // Assert
        var ex = Assert.ThrowsAsync(typeof(ValidationException), async () =>
        {
            await _cityService.Create(city);
        });

        Assert.That(ex.Message, Does.Contain("Name is required"));
    }
}