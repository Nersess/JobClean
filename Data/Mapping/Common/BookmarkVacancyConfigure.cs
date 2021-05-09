using Core.Domains.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mapping.Common
{
    public class BookmarkVacancyConfigure : IEntityTypeConfiguration<BookmarkVacancy>
    {
        public void Configure(EntityTypeBuilder<BookmarkVacancy> builder)
        {
            builder.ToTable("VacancyCategoryRef");

            builder.HasKey(vcr => vcr.Id);

            builder.HasOne(v => v.Vacancy)
                .WithMany(vc => vc.BookmarkVacancys).HasForeignKey(vc => vc.VacancyId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.User)
                .WithMany(u => u.BookmarkVacancys).HasForeignKey(vc => vc.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
