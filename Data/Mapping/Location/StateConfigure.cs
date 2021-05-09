using Core.Domains.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mapping.Location
{
    public class StateConfigure : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.HasKey(s => s.Id);

            builder.ToTable("Sate");

            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);

            builder.HasOne<Country>(s => s.Country)
            .WithMany(s => s.States);            
        }

    }
}
