using Core.Domains.Vacancys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mapping.Vacancys
{
    public class VacancyCategoryConfigure : IEntityTypeConfiguration<VacancyCategory>
    {
        public void Configure(EntityTypeBuilder<VacancyCategory> builder)
        {
            builder.ToTable("VacancyCategory");

            builder.HasKey(vc => vc.Id);
            builder.Property(vc => vc.Name).HasMaxLength(50);
        }
    }
}
