using Core.Caching.Domains;
using Data.Interfaces;
using Data.Mapping.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Core.Domains.Users;

namespace Data
{
    //If our user will be custom user, we will inherit it from DBContext class
    public class JSDbContext : IdentityDbContext<User, Role, int>, IDbContext
    {
        public JSDbContext(DbContextOptions<JSDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<TEntity> SetEntity<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }
    }
}
    
