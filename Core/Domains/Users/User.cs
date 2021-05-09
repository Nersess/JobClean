using Core.Caching.Domains;
using Core.Domains.Common;
using Core.Domains.Vacancys;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Domains.Users
{
    /// <summary>
    /// User class should be inherited from BaseEntity class and overwritten to have fully customized class
    /// In that case Repository and Dbcontext will work with User also
    /// </summary>
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<BookmarkVacancy> BookmarkVacancys { get; set; }
    }
    
}
