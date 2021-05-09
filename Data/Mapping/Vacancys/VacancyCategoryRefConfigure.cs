using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Domains.Vacancys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mapping.Vacancys
{
    public class VacancyCategoryRefConfigure : IEntityTypeConfiguration<VacancyCategoryRef>
    {
        public void Configure(EntityTypeBuilder<VacancyCategoryRef> builder)
        {
            builder.ToTable("VacancyCategoryRef");

            builder.HasKey(vcr => vcr.Id);

            builder.HasOne(v => v.Vacancy)
                .WithMany(vc => vc.VacancyCategoryRef).HasForeignKey(vc => vc.VacancyId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(vc => vc.VacancyCategory)
                .WithMany(v => v.VacancyCategoryRef).HasForeignKey(vc => vc.VacancyCategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
