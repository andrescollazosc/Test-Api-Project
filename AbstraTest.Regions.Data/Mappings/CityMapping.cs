using AbstraTest.Regions.Core.Cities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbstraTest.Regions.Data.Mappings;

public class CityMapping : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(p => p.CreatedDate)
            .IsRequired();
        
        builder.HasOne(o => o.Country)
            .WithMany(c => c.Cities)
            .HasForeignKey(o => o.CountryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}