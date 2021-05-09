using Core.Domains.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mapping.Location
{
    public class CityConfigure : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("City");

            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);

            builder.HasOne<State>(s => s.State)
            .WithMany(s => s.Citys)
            .HasForeignKey(s => s.CurrentStateId);
        }
    
    }
}
