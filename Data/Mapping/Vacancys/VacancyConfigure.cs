using Core.Domains.Vacancys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mapping.Location.Vacancys
{
    public class VacancyConfigure : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.ToTable("Vacancy");

            builder.HasKey(v => v.Id);
            builder.Property(u => u.Title).HasMaxLength(50);
        }
    }
}
