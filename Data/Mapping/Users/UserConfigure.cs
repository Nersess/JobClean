using Core.Domains.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mapping.Users
{
    public class UserConfigure : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);            
            builder.Property(u => u.Email).IsRequired().HasMaxLength(256);            
            builder.Property(u => u.UserName).HasMaxLength(256);

            builder.ToTable("Users");
        }
    }
}
