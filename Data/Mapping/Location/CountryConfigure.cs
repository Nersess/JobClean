using Core.Domains.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mapping.Location
{
    public class CountryConfigure : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(s => s.Id);

            builder.ToTable("Country");

            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
        }
    }
}
